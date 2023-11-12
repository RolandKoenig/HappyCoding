docker build -f HappyCoding.WebApiWithSqliteDb/Dockerfile -t web-api-with-sqlite-db .
docker run -p 5100:80 --name my-web-api-with-sqlite-db -d web-api-with-sqlite-db