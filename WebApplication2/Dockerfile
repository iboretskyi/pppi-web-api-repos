# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY ["AnimeWebAPI.csproj", "./"]
RUN dotnet restore "AnimeWebAPI.csproj"
COPY . .
RUN dotnet publish "AnimeWebAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish .

# Install ClosedXML library
RUN apt-get update \
    && apt-get install -y libgdiplus \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

EXPOSE 8080
ENTRYPOINT ["dotnet", "AnimeWebAPI.dll"]
