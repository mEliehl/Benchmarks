package main

import (
	"database/sql"
	"fmt"
	"log"
	"os"
	"runtime"
	"sort"

	"github.com/gin-gonic/gin"
	_ "github.com/lib/pq"
)

const (
	fortuneSelect = "select id, message FROM fortune"
)

type Fortune struct {
	Id      uint16 `json:"id"`
	Message string `json:"message"`
}

type Fortunes []*Fortune

func (s Fortunes) Len() int      { return len(s) }
func (s Fortunes) Swap(i, j int) { s[i], s[j] = s[j], s[i] }

type ByMessage struct{ Fortunes }

func (s ByMessage) Less(i, j int) bool { return s.Fortunes[i].Message < s.Fortunes[j].Message }

var (
	// Database
	fortuneStatement *sql.Stmt
)

func plaintext(c *gin.Context) {
	c.String(200, "Hello, World!")
}

func fortunes(c *gin.Context) {
	rows, err := fortuneStatement.Query()
	if err != nil {
		c.AbortWithError(500, err)
		return
	}

	fortunes := make(Fortunes, 0, 16)
	for rows.Next() { //Fetch rows
		fortune := Fortune{}
		if err := rows.Scan(&fortune.Id, &fortune.Message); err != nil {
			c.AbortWithError(500, err)
			return
		}
		fortunes = append(fortunes, &fortune)
	}
	fortunes = append(fortunes, &Fortune{Message: "Additional fortune added at request time."})

	sort.Sort(ByMessage{fortunes})
	c.HTML(200, "fortune.html", fortunes)
}

func main() {
	gin.SetMode(gin.ReleaseMode)
	r := gin.New()
	serverHeader := []string{"Gin"}
	r.Use(func(c *gin.Context) {
		c.Writer.Header()["Server"] = serverHeader
	})

	r.LoadHTMLFiles("fortune.html")
	r.GET("/plaintext", plaintext)
	r.GET("/fortunes", fortunes)
	r.Run(":80")
}

func getEnv(key, fallback string) string {
	value, exists := os.LookupEnv(key)
	if !exists {
		value = fallback
	}
	return value
}

func init() {
	max := runtime.NumCPU()
	fmt.Printf("numcpu = %v", max)
	runtime.GOMAXPROCS(max)

	var (
		host     = getEnv("host", "localhost")
		port     = getEnv("port", "5432")
		user     = getEnv("user", "benchmark")
		password = getEnv("password", "benchmark")
		dbname   = getEnv("dbname", "benchmark")
	)
	connStr := fmt.Sprintf("host=%s port=%s user=%s "+
		"password=%s dbname=%s sslmode=disable",
		host, port, user, password, dbname)

	fmt.Printf("Database: %v", connStr)
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		log.Fatalf("Error opening database: %v", err)
	}

	fortuneStatement, err = db.Prepare(fortuneSelect)
	if err != nil {
		log.Fatalf("Error prepare : %v", err)
	}
}
