# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY MyBookStore/*.csproj .
RUN dotnet restore

#COPY BookLibraryRESTfull/MyBookStore/* .
#COPY lol.txt .

# copy everything else and build app
COPY MyBookStore/. .
#RUN dotnet publish --use-current-runtime --self-contained false -o /app
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyBookStore.dll"]
