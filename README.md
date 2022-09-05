# SleekBoard
:pencil: An experimental todo list API, powered by .NET.

## Prerequisite
Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download).

## Getting started
Generate a development cert with:
```
dotnet dev-certs https --trust
```
Run the following command in **src** directory:
```
dotnet run
```
You should be able to browse the Swagger UI, and explore the endpoints by using:
```
https://localhost:port/swagger
```
## Architecture overview
The application was built with a simple 2-layer architecture, Controller-Repository. A service layer between the controller and the repository was forfeited to streamline the design.

### Data access

EntityFramework Core (EF Core) is the chosen ORM. An in-memory database was used to optimise development speed. The access to EF Core's DbContext was further 
encapsulated using the repository pattern. This ensures that switching/removing ORM in the future is easier, and you do not need to mock the DbContext for unit testing.

### Entity relationships
There are two domain entities, Board and BoardItem. You can think of Board as a todo list, and BoardItem as an item in a todo list. Board has a one-many relationship with BoardItem.

### Auditable entity
The base entity has the following properties: CreationTime, ModificationTime, and DeletionTime. The values will be populated automatically without your intervention. You can override the behaviour in the `UpdateEntityValues` method of `SleekBoardContext.cs`.

### Soft deletion
By default, you will not be actually deleting an entity. Deleted entities are marked using the `isDeleted` property. There is a global filter that automatically filters the deleted entities during the database query. Similar to the mechanism of auditable entity, you can override the behaviour in `SleekBoardContext.cs`.

## API overview
### Filtering
You can filter the retrieval of boards or board items by using query strings. 

The following **Boards** filters are supported on **GET ​/api​/boards**​:
```
Name (string)
Description (string)
```
Example:

```
curl -X 'GET' 'https://localhost:port/api/boards?Name=Study&Description=Study'
```
The following **BoardItems** filters are supported on **GET ​/api​/boards​/{boardId}​/items**:
```
Name (string)
Description (string)
MinDueDate (YYYY-MM-DDThh:mm:ss.sssZ)
MaxDueDate (YYYY-MM-DDThh:mm:ss.sssZ)
Status (0 = Pending, 1 = Completed)
```
Example:

```
curl -X 'GET' 'https://localhost:port/api/boards/3fa85f64-5717-4562-b3fc-2c963f66afa6/items?Name=Study&Status=0&MinDueDate=2022-09-04T14:23:41.955Z'
```
### Sorting
The API comes with a flexible sorting system that is powered by [Dynamic LINQ](https://dynamic-linq.net/basic-simple-query#ordering-results). You can sort result with the Sorting query strings. The values can be "Name", "Name desc", "Name asc, CreationTime desc" etc.

Sorting by Name:
```
curl -X 'GET' 'https://localhost:port/api/boards?Sorting=Name'
```

Sorting by Name in descending order:
```
curl -X 'GET' 'https://localhost:port/api/boards?Sorting=Name%20desc'
```

Sorting by Name in ascending order and then CreationTime in descending order:
```
curl -X 'GET' 'https://localhost:port/api/boards?Sorting=Name%20asc%2C%20CreationTime%20desc'
```
> **Known issue**
> To be production-ready, it is recommended to limit the properties that can be sorted. Due to time constraints, a proper guardrail was not developed. This means that a user can sort entity properties that are not exposed in DTO. You will also encounter an Internal Server Error 500 when you are using an arbitrary sorting value. I am sorry about that :sweat:.
> 

## Testing
The project comes with a few sample unit tests, powered by xUnit. Run the following command in **test** directory:
```
dotnet test
```

