# Book Library RESTfull service in ASP.NET Core

Ability to GET and POST books with inforamtion about them

# Running the project with Docker

docker build --tag library -f Dockerfile .

# Info while developing
### Adding integration tests
Right click solution, add MSTests.  
Go into Package Manager console, choose/specify testproject:
> $ install-package microsoft.aspnetcore.testhost
Rightclick "Dependencies", "Add Reference", choose main project
