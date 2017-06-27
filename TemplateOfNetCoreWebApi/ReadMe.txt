1. How to migrations with EF Core
troubleshooting can refer to :http://www.cnblogs.com/soldout/p/EFCoreCodeFirstMigrationErrorHandle.html
A. DAL
   a. add models,DBContext class(here is SchoolContext)

   b.package manager console -- install dependency for EF
   Install-Package Microsoft.EntityFrameworkCore -Version 1.1.2
   Install-Package Microsoft.EntityFrameworkCore.SqlServer  (offer sql server database provider)
   Install-Package Microsoft.EntityFrameworkCore.Design -Version 1.1.2

   Solution 1: using CLI operate Migrations - CLI(Command-line interface tool)
   c. edit DAL.csproj so that project install "Microsoft.EntityFrameworkCore.Tools.DotNet"
   <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />

   d.create DB (because there are not DB provider in DAL project,so using API project to do)
   dotnet ef migrations add InitialCreate -s ..\TemplateOfNetCoreWebApi

   e. update DB
   dotnet ef database update -s ..\TemplateOfNetCoreWebApi

   f. remove the last migrations
   dotnet ef migrations remove -s ..\TemplateOfNetCoreWebApi

   Solution 2: using Package Manager Console(PMC)
   c. install Microsoft.EntityFrameworkCore.Tools
   Install-Package Microsoft.EntityFrameworkCore.Tools
   after execute above command, it will be added an reference in DAL.csproj as below:
   <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="1.1.1" />

   d. create DB
   Add-Migration InitialCreate

   e. update DB
   Update-Database -s 

   f. remove the last migration
   Remove-Migration -s 

B. API project -- TemplateOfNetCoreWebApi
   a. reference DAL project

   b.package manager console -- install dependency for EF
   Install-Package Microsoft.EntityFrameworkCore.SqlServer

   c. appsetting.json add connection string

   d. startup.cs add db provider into ConfigureServices
   services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

   e. [Optional] create DB and initail data of DB by using DbInitializer class in startup.cs
   DbInitializer.Initialize(context);

2. Repository and Unit of work pattern
   add genericRepository.cs and UnitOfWork.cs into DAL project to make sure using same DBContext in Controller or service
   (actually, if we use DBContext by DI,the DBContext will be same in controller)
   refer to :
   https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
   https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/advanced

3. Add BLL project into Solution which includes all of services and DTOs(Data Transfer Object)

4. Add Unit Test Project
   using Moq and xUnit as the TDD(Test Drive Development) framework
   Moq refer to:https://github.com/moq/moq4
   xUnit: 
   office site:https://xunit.github.io/docs/getting-started-dotnet-core.html
   Introduction:https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
   other:http://www.cnblogs.com/gaochundong/p/mstest_nunit_xunit_comparisons.html

   a. install xUnit package 
      1. edit testproject.proj to add reference for xunit, then restore
	  2. using Nuget to install
   b. install Moq
      using Nuget: Install-Package Moq
   c. reference BLL project into Test Project.
   d. create IUnitOfWork and IRepository so that We can mock them for test service
      refer to:https://stackoverflow.com/questions/21847306/how-to-mock-repository-unit-of-work
   e. related documents for introduce test concept
      https://www.gaui.is/how-to-mock-the-datacontext-linq/
	  https://www.gaui.is/isolating-your-bll-from-your-dal-and-unit-testing-it/
   f. Demo or example
      https://github.com/bpun/UnitTesingfor.NETCorewithXUnitAndMOQ

