FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /glint/app
RUN addgroup -g 1000 appuser && \
    adduser -u 1000 -G appuser -s /bin/sh -D appuser && \
    chown -R appuser:appuser /glint/app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /glint/src
COPY ["Glint.HttpApi.csproj", "./"]
RUN dotnet restore "Glint.HttpApi.csproj"
COPY . .
RUN dotnet publish "Glint.HttpApi.csproj" -c Release -o /glint/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS development
WORKDIR /glint/src/Glint.HttpApi
# No COPY needed — source files come from volume mount.
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "run", "--no-build", "--no-restore", "--no-launch-profile"]

FROM base AS final
COPY --from=build /glint/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Glint.HttpApi.dll"]