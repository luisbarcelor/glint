FROM mcr.microsoft.com/dotnet/sdk:10.0 AS development
WORKDIR /src

COPY ["Glint.HttpApi.csproj", "./"]

RUN dotnet restore "Glint.HttpApi.csproj"

COPY . .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "run", "--no-launch-profile"]


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Glint.HttpApi.csproj", "./"]

RUN dotnet restore "Glint.HttpApi.csproj"

COPY . .

RUN dotnet publish "Glint.HttpApi.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS production
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

RUN addgroup -g 1000 appuser && \
    adduser -u 1000 -G appuser -s /bin/sh -D appuser && \
    chown -R appuser:appuser /app

USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "Glint.HttpApi.dll"]