echo "====================" >> log.txt
echo "CSHARP - MVC" >> log.txt
echo "Inicio do teste PLAINTEXT" >> log.txt
echo "16 conexões" >> log.txt
wrk -t 8 -c 16 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "-----------" >> log.txt
echo "32 conexões" >> log.txt
wrk -t 8 -c 32 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "-----------" >> log.txt
echo "64 conexões" >> log.txt
wrk -t 8 -c 64 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "------------" >> log.txt
echo "128 conexões" >> log.txt
wrk -t 8 -c 128 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "------------" >> log.txt
echo "256 conexões" >> log.txt
wrk -t 8 -c 256 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "------------" >> log.txt
echo "512 conexões" >> log.txt
wrk -t 8 -c 512 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "------------" >> log.txt
echo "1024 conexões" >> log.txt
wrk -t 8 -c 1024 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "------------" >> log.txt
echo "2048 conexões" >> log.txt
wrk -t 8 -c 2048 -d 10s http://localhost:5000/mvc/plaintext >> log.txt
echo "====================" >> log.txt
echo "GO - GIN" >> log.txt
echo "Inicio do teste PLAINTEXT" >> log.txt
echo "16 conexões" >> log.txt
wrk -t 8 -c 16 -d 10s http://localhost:7000/plaintext >> log.txt
echo "-----------" >> log.txt
echo "32 conexões" >> log.txt
wrk -t 8 -c 32 -d 10s http://localhost:7000/plaintext >> log.txt
echo "-----------" >> log.txt
echo "64 conexões" >> log.txt
wrk -t 8 -c 64 -d 10s http://localhost:7000/plaintext >> log.txt
echo "------------" >> log.txt
echo "128 conexões" >> log.txt
wrk -t 8 -c 128 -d 10s http://localhost:7000/plaintext >> log.txt
echo "------------" >> log.txt
echo "256 conexões" >> log.txt
wrk -t 8 -c 256 -d 10s http://localhost:7000/plaintext >> log.txt
echo "------------" >> log.txt
echo "512 conexões" >> log.txt
wrk -t 8 -c 512 -d 10s http://localhost:7000/plaintext >> log.txt
echo "------------" >> log.txt
echo "1024 conexões" >> log.txt
wrk -t 8 -c 1024 -d 10s http://localhost:7000/plaintext >> log.txt
echo "------------" >> log.txt
echo "2048 conexões" >> log.txt
wrk -t 8 -c 2048 -d 10s http://localhost:7000/plaintext >> log.txt
echo "====================" >> log.txt
echo "JAVASCRIPT - EXPRESS" >> log.txt
echo "Inicio do teste PLAINTEXT" >> log.txt
echo "16 conexões" >> log.txt
wrk -t 8 -c 16 -d 10s http://localhost:9000/plaintext >> log.txt
echo "-----------" >> log.txt
echo "32 conexões" >> log.txt
wrk -t 8 -c 32 -d 10s http://localhost:9000/plaintext >> log.txt
echo "-----------" >> log.txt
echo "64 conexões" >> log.txt
wrk -t 8 -c 64 -d 10s http://localhost:9000/plaintext >> log.txt
echo "------------" >> log.txt
echo "128 conexões" >> log.txt
wrk -t 8 -c 128 -d 10s http://localhost:9000/plaintext >> log.txt
echo "------------" >> log.txt
echo "256 conexões" >> log.txt
wrk -t 8 -c 256 -d 10s http://localhost:9000/plaintext >> log.txt
echo "------------" >> log.txt
echo "512 conexões" >> log.txt
wrk -t 8 -c 512 -d 10s http://localhost:9000/plaintext >> log.txt
echo "------------" >> log.txt
echo "1024 conexões" >> log.txt
wrk -t 8 -c 1024 -d 10s http://localhost:9000/plaintext >> log.txt
echo "------------" >> log.txt
echo "2048 conexões" >> log.txt
wrk -t 8 -c 2048 -d 10s http://localhost:9000/plaintext >> log.txt
echo "fim"