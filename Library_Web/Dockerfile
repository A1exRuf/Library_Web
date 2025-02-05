FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Library_Web/Library_Web.csproj", "Library_Web/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["UseCases/UseCases.csproj", "UseCases/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
RUN dotnet restore "./Library_Web/Library_Web.csproj"
COPY . .
WORKDIR "/src/Library_Web"
RUN dotnet build "./Library_Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Library_Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library_Web.dll"]