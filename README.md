# DotNetCoreCleanArchitecture
![Build Status](https://github.com/vikas0sharma/DotNetCoreCleanArchitecture/workflows/Docker%20Build%20Status/badge.svg)

Project to follow Domain Driven Design (DDD) and Comand and Query Responsibility Segregation (CQRS) and other patterns.

![Clean Architecture](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)
_Image by __Uncle Bob__ from [blog](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)_


![Microsoft](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/media/ddd-oriented-microservice/ddd-service-layer-dependencies.png)
_Image by __Microsoft__ from [documentation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)_


## The domain model layer
It is responsible for representing concepts of the business, information about the business situation, and business rules. 
This layer is the core of business software.
The domain model layer is where the business is expressed.

## The infrastructure layer
The infrastructure layer is how the data that is initially held in domain entities (in memory) is persisted in databases or another persistent store. 
An example is using Entity Framework Core code to implement the Repository pattern classes that use a DBContext to persist data in a relational database.
It can contain caching servive, email service, notification service, etc. 

## The application layer
Defines the jobs the software is supposed to do and directs the expressive domain objects to work out problems. 
The tasks this layer is responsible for are meaningful to the business or necessary for interaction with the application layers of other systems. 
This layer is kept thin. It does not contain business rules or knowledge, but only coordinates tasks and delegates work to collaborations of domain objects in the next layer down. 
It does not have state reflecting the business situation, but it can have state that reflects the progress of a task for the user or the program.

## API layer
This contains the API that will be used by client application.
