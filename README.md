This was an ongoing homework project during Adform's .Net Academy. 

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
4. DONE Add new User Controller which performs GET, GET By Id, and Create to https://jsonplaceholder.typicode.com/users
5. IN PROGRESS / IMPROVE **Cover GetById with a unit tests**.
   
Optional, advanced: Have basic data caching for the system (Fetch data and save into database)

**2023.12.27**

0. DONE Finish previous tasks (tests, external api)
   
1. DONE Apply Adapter pattern on JsonPlaceholder (error handling, etc)
   
2. DONE Beautify code (exceptions, etc)

ADVANCED

DONE 0. Think about introducing 'Shop' {name, address} to which 'Item' belongs. -->
1. 
DONE (1) CRUD to Shop (galbut panaudot inherit?)

DONE (2) Assign 'Item' to a shop. _Assumption: item only belongs to 1 shop_

(3) 'User' should be able to buy an item _Assumption: multiple users can buy the same item. Bought item is not removed from shop. Assumption: quantity is indefinite. If item/user is deleted, the list of purchases doesn't change (for audit purposes)_

3. Apply Clean architecture principles.
