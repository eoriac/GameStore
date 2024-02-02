# Setup solution

dotnet new sln -o GameStore

dotnet new webapi --use-controllers -o GameStore.API

dotnet sln add .\GameStore.API\GameStore.API.csproj


# Setup mongo with Docker

https://hub.docker.com/_/mongo

docker pull mongo

docker run -d --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=db_admin -e MONGO_INITDB_ROOT_PASSWORD=mongodb_1234 mongo

docker exec -it mongodb bash

mongosh --username db_admin --password mongodb_1234


# BSON spec
https://bsonspec.org/

# Mongo Objects

// Not acceptable
_id : [1, 2, 3]

// Acceptable
_id : string, int, objects, ...

// Default
_id = ObjectId()

# Mongo Driver C#
https://www.nuget.org/packages/MongoDB.Driver
https://www.mongodb.com/docs/drivers/csharp/current/
