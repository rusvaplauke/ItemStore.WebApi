**2023.12.18**

Create an webapi for Item Store.
Requirements:
DONE 0. Item has Id, Name, Price.

DONE 1. It has to be a Restful CRUD

DONE 2. Follow Controller, Service, Repository (connect to Postgre). (DbUp not required).

DONE 3. Use Dependency Injection.

DONE 4. Implement additional action "Buy", where quantity is required. If quantity is more than 10, apply 10% discount.
      If it is more than 20 apply 20% discount.

**2023.12.19**

Additional requirements:
DONE 1. Replace Your Dapper repository with EF Core InMemory Repository.

DONE 2. Your service should  (maybe?) be interchangable with both Dapper and EF Core InMemory.

DONE 3. Update your application with DTO/Entity concepts.

Advanced:
DONE 1. Connect EF Core to Postgre. (Investigate Migrations)
2. try to  use AutoMapper to map entities to dtos.

**2023.12.20**
   Update existing ItemStore:
   
DONE Employ ExceptionHandlingMiddleware.

Convert the code to Async.

(Advanced) Use Ef Core migrations to create new database and create schema.

**2023.12.21**

Update TodoStore:

Cover Services with Unit tests. Use Moq and Fluent Assertions. Mark Arrange-Act-Assert

Please create Github repository and push your code to Github.

Advanced:

     1. Research Autofixture.
     
     2. Try creating Integration Tests.
