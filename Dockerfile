# Use the official ASP.NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DB_CONNECTION_STRING="Host=localhost;Port=5432;Username=postgres;Password=saadmin;Database=TransactionDB"



# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.sln .
#COPY ["BookTransactionServices.Api/BookTransactionServices.Api.csproj", "BookTransactionServices.Api/"]
COPY ["src/BookTransactionServices.Api/*.csproj", "src/BookTransactionServices.Api/"]
COPY ["src/BookTransactionServices.Model/*.csproj", "src/BookTransactionServices.Model/"]
COPY ["src/BookTransactionServices.Repository/*.csproj", "src/BookTransactionServices.Repository/"]
COPY ["src/Service/*.csproj", "src/Service/"]


#COPY BookTransactionServices.Api/*.csproj ./BookTransactionServices.Api/
RUN dotnet restore "src/BookTransactionServices.Api/BookTransactionServices.Api.csproj"

COPY . .
WORKDIR "src/BookTransactionServices.Api"
#RUN ["/bin/bash", "-c", "dotnet build "BookTransactionServices.Api.csproj -c Release -o /app/build"]
RUN dotnet build "BookTransactionServices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookTransactionServices.Api.csproj" -c Release -o /app/publish --no-restore

# Copy the build output to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entrypoint to the application
ENTRYPOINT ["dotnet", "BookTransactionServices.Api.dll"]
