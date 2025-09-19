FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["perla-metro-stations-service.csproj", "./"]
RUN dotnet restore "./perla-metro-stations-service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "perla-metro-stations-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "perla-metro-stations-service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DOTNET RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "perla-metro-stations-service.dll"]
