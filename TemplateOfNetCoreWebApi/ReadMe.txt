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
      http://www.punbharat.com.np/unit-testing-for-net-core-mvc-with-xunit-and-moq/
      https://github.com/bpun/UnitTesingfor.NETCorewithXUnitAndMOQ
   g. run test 
      Test -> Run -> All Tests (or Ctrl R + A) in visual studio

   Note: [TODO] add specflow test project for solution

5.Security
  A.Enable SSL for .net core web api
    refer to:https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl
	we can enable ssl on three level (globlly,controller and action), globlly is the best practice.
	a. enable ssl in configureService (using RequireHttpsAttribute in globlly, [RequireHttps] to controllers )
	b. redirect all http request to https
	   need to install Microsoft.AspNetCore.Rewrite by Nuget, command as below
	   Install-Package Microsoft.AspNetCore.Rewrite -Version 1.0.2
	c. set up iis express for ssl/https on the project

  B.Enable Cors (Cross Origin Requests)
    refer to: https://docs.microsoft.com/en-us/aspnet/core/security/cors
	a. install package for Cors by nuget
	   Install-Package Microsoft.AspNetCore.Cors
	b. in startup.cs ConfigureServices method setting up Cors
	   Services.AddCors()
	   Meanwhile, we can config cors globally using CorsAuthorizationFilterFactory class or [EnableCors] for controller or action
	   the precedence order is: Action-level > controller level > globally level
	c. in startup.cs Congigure method enable Cors
	   app.UseCors()
	d. disable cors for controller or action
	   we can use [DisableCors] attribute to disable cors

	Note: How cors work 
	precondition: 
	Browser security prevents a web page from making AJAX requests to another domain. 
	This restriction is called the same-origin policy, but sometimes you might want to let other sites make cross-origin requests to your web API.

	a.If a browser supports CORS, it sets these headers automatically for cross-origin requests; you don’t need to do anything special in your JavaScript code.
	The "Origin" header gives the domain of the site that is making the request: like below
GET http://myservice.azurewebsites.net/api/test HTTP/1.1
Referer: http://myclient.azurewebsites.net/
Accept: */*
Accept-Language: en-US
Origin: http://myclient.azurewebsites.net <<--
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)
Host: myservice.azurewebsites.net
    
	b.If the server allows the request, it sets the Access-Control-Allow-Origin header. 
	The value of this header either matches the Origin header, or is the wildcard value "*", meaning that any origin is allowed.:
HTTP/1.1 200 OK
Cache-Control: no-cache
Pragma: no-cache
Content-Type: text/plain; charset=utf-8
Access-Control-Allow-Origin: http://myclient.azurewebsites.net <<--
Date: Wed, 20 May 2015 06:27:30 GMT
Content-Length: 12
Test message
    
	If the response does not include the Access-Control-Allow-Origin header, the AJAX request fails.

	C.Preventing Cross-Site Scripting(XSS)
	  mainly using Encoding way to prevent XSS, especially for input or output in front end.
	  at the same time, validation is also best practice for XSS, such as an number input should add 0-9 validation in case other characters inputed.

	D.Preventing Cross-Site Request Forgery(CSRF)
	  refer to:https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery
	  using [ValidateAntiForgeryToken] attribute for controller or action








    

