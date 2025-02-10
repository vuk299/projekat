## Swagger url 
http://localhost:8080/swagger/index.html

## build Docker image 
cd ./projekat_API

docker build -t \<image-name\> .

## run Docker container 
docker run -p 8080:8080 --name \<container-name\> \<image-name\>
