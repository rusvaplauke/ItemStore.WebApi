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
DONE 2. try to  use AutoMapper to map entities to dtos.

**2023.12.20**
DONE Update existing ItemStore:
   
DONE Employ ExceptionHandlingMiddleware.

DONE Convert the code to Async.

DONE (Advanced) Use Ef Core migrations to create new database and create schema.

**2023.12.21**

Update TodoStore:

DONE Cover Services with Unit tests. Use Moq and Fluent Assertions. Mark Arrange-Act-Assert

DONE Please create Github repository and push your code to Github.

DONE 1. Research Autofixture.
     
ADVANCED 2. Try creating Integration Tests.
**2023.12.22**
1. DONE? Use Fluent Assertions.
2. IN PROGRESS / IMPROVE **Add Mock Verify to your tests.**
3. DONE Use Autofixture for data generation. Autofixture.xunit2 has AutoData attribute.
4. Add new User Controller which performs GET, GET By Id, and Create to https://jsonplaceholder.typicode.com/users
5. Cover GetById with a unit tests.
   
Optional, advanced: Have basic data caching for the system (Fetch data and save into database)
