FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /glint/app
RUN apt-get update && apt-get install -y --no-install-recommends ffmpeg=7:6.1.1* && rm -rf /var/lib/apt/lists/*
RUN groupadd -g 1000 appuser && \
    useradd -u 1000 -g appuser -s /bin/bash -m appuser && \
    chown -R appuser:appuser /glint/app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /glint/src
COPY ["Glint.MediaWorker.csproj", "./"]
RUN dotnet restore "Glint.MediaWorker.csproj"
COPY . .
RUN dotnet publish "Glint.MediaWorker.csproj" -c Release -o /glint/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS development
WORKDIR /glint/src/Glint.MediaWorker
RUN apt-get update && apt-get install -y --no-install-recommends ffmpeg=7:6.1.1* && rm -rf /var/lib/apt/lists/*
# No COPY needed — source files come from volume mount.
ENV DOTNET_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"]

FROM base AS final
COPY --from=build /glint/publish .
ENV DOTNET_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "Glint.MediaWorker.dll"]
