FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["/src/Api/Api.csproj", "Api/"]
COPY ["/src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["/src/Shared/Shared.csproj", "Shared/"]
COPY ["/src/Application/Application.csproj", "Application/"]

WORKDIR "/src/Api"
RUN dotnet restore "Api.csproj"

COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

COPY . .
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]
