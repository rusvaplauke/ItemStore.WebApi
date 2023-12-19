2023.12.18 
Create an webapi for Item Store.
Requirements:
0. Item has Id, Name, Price.
1. It has to be a Restful CRUD
2. Follow Controller, Service, Repository (connect to Postgre). (DbUp not required).
3. Use Dependency Injection.
4. Implement additional action "Buy", where quantity is required. If quantity is more than 10, apply 10% discount.
If it is more than 20 apply 20% discount.

2023.12.19
Additional requirements:
1. Replace Your Dapper repository with EF Core InMemory Repository.
2. Your service should  (maybe?) be interchangable with both Dapper and EF Core InMemory.
3. Update your application with DTO/Entity concepts.
Advanced:
1. Connect EF Core to Postgre. (Investigate Migrations)
2. try to  use AutoMapper to map entities to dtos.
