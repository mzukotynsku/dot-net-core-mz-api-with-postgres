FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

WORKDIR /src
COPY *.sln .
COPY DotNetCoreMZ.API/*.csproj DotNetCoreMZ.API/
COPY DotNetCoreMZ.Contracts/*.csproj DotNetCoreMZ.Contracts/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/DotNetCoreMZ.API
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# ENTRYPOINT ["dotnet", "DotNetCoreMZ.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DotNetCoreMZ.API.dll
