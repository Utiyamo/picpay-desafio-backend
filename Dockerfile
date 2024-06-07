# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DC.PicpaySim.API/DC.PicpaySim.API.csproj", "DC.PicpaySim.API/"]
RUN dotnet restore "DC.PicpaySim.API/DC.PicpaySim.API.csproj"
COPY . .
WORKDIR "/src/DC.PicpaySim.API"
RUN dotnet build "DC.PicpaySim.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DC.PicpaySim.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DC.PicpaySim.API.dll"]