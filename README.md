# containerized-microservice
Playground for experimenting with containerization of a microservice

## Overview
The project is for my experiments with Docker and other new(ish) technology. As of this writing, it's a service written in ASP.NET Core 2.0 with Serilog and Entity Framework. The database is PostgreSQL and the logs are saved in an Elasticsearch database. Kibana is used to browse the log.

This is bound to change as I play around with it and, knowing me, I might not get around to updating the readme file.

## Installing
To install, you need a computer with Docker, Docker Compose and Git. I use digitalocean.com's Ubuntu with Docker image when I need to test on a fresh machine.
As root, run

`git clone https://github.com/kodedylf/containerized-microservice.git` 

to copy the files. Then run

`docker-compose up`

to watch the magic happen.

Quite a lot happens when docker-compose brings up the service, including
- downloading all the needed images from Docker Hub and Elastic.
- running a container that builds the ASP.NET service. This is done inside a Microsoft made container that has the .NET Core SDK installed. Since it's done inside a container, the host machine doesn't need to have the SDK installed.
- creating a database. The service uses Entity Framework to create the tables it needs inside this database when it starts.

When the service has been built and is running, you can access the service on port 80 and Kibana on port 5601.
