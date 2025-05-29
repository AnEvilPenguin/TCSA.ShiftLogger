# Shifts Logger

A console based C# application used to track shifts at a hypothetical company. Developed using SQL Server,
Spectre.Console, EF Core, and ASP .Net Core. Implemented using CRUD and MVC methodologies.

# How to use

This project requires the use of Microsoft SQL Server. There shouldn't be any requirement to use a specific type or
edition of SQL Server, however this was developed using the Linux container image:

``` powershell
docker pull mcr.microsoft.com/mssql/server
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

Which will then run the container using the default port, the developer edition, and (at the time of writing) the 2022
version of the software.

Once the project is built you'll need to update the configuration in the `appsettings.json` file to use your SQL
Server. During development the sa account was used. However, it should be possible to create a lower privilege account
as long as it can create a database and tables. You could also pre-create the `Shifts` database and use an account
that can only create/use tables.

Once the application is able to connect to the database all interaction is provided via a menu.

# Requirements

- [X] Record a workers shifts
  - I've taken this to mean that this is a full shift not an in-progress shift
- [X] Two applications Web API and UI
- [X] All validation in UI app
- [X] API controllers should be lean, logic should be in separate service
- [X] Uses SQL Server not SQLite
- [X] Code first Entity Framework
- [X] Try-catch blocks around API calls in UI

## Stretch Goals

No stretch goals for this project.

# Challenges

Generally this was pretty straightforward. The only real challenges were where plans did not survive
contact with reality. Mostly this was around how the HTTP client interacted with the API.  
Additionally my focus stared to wane about half-way through the project. I've come up with a side project
idea that I want to explore. Though funnily enough a lot of the things learned in this project will be 
relevant. This README for example is far leaner than I normally would want.  
But hopefully something much more interesting to look at in the near future, once I've finished another 
training course I think will complement it nicely.

# Lessons Learned

- Learned a lot more about how ASP.net core and EF interact
- Learned about some of the oddities around HttpClient and JSON encoding

# Areas to Improve

- Most of the project to be honest
  - Lots of corners cut where I want to move on
- Otherwise, I think it mostly comes down to my planning not matching reality
  - This will just be better now that I have more experience

