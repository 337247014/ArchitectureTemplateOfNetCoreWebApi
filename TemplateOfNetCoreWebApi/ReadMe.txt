1. DAL
   a. add models,DBContext

   b.package manager console -- install dependency for EF
   Install-Package Microsoft.EntityFrameworkCore -Version 1.1.2
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Design -Version 1.1.2

   c. DAL.csproj add
   <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />

   d.create DB (because there are not DB provider in DAL project,so using API project to do)
   dotnet ef migrations add InitialCreate -s ..\TemplateOfNetCoreWebApi

2. API project -- TemplateOfNetCoreWebApi
   a. reference DAL project

   b.package manager console -- install dependency for EF
   Install-Package Microsoft.EntityFrameworkCore.SqlServer

   c. appsetting.json add connection string

   d. startup.cd add db provider into ConfigureServices
   services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

