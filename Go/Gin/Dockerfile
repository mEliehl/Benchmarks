FROM golang:latest

ADD ./Go/Gin /Gin
WORKDIR /Gin

RUN mkdir bin
ENV GOPATH /Gin
ENV PATH ${GOPATH}/bin:${PATH}

RUN go get github.com/gin-gonic/gin
RUN go get github.com/lib/pq

CMD go run main.go