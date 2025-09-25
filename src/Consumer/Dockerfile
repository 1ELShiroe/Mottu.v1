FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["/src/Consumer/Consumer.csproj", "Consumer/"]
COPY ["/src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["/src/Shared/Shared.csproj", "Shared/"]
COPY ["/src/Application/Application.csproj", "Application/"]

WORKDIR "/src/Consumer"
RUN dotnet restore "Consumer.csproj"

COPY . .
WORKDIR "/src/Consumer"
RUN dotnet build "Consumer.csproj" -c Release -o /app/build

COPY . .
RUN dotnet publish "Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Consumer.dll"]
