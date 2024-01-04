FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MultiLevelArchitectureProject.WEB/MultiLevelArchitectureProject.WEB.csproj", "MultiLevelArchitectureProject.WEB/"]
COPY ["MultiLevelArchitectureProject.BLL/MultiLevelArchitectureProject.BLL.csproj", "MultiLevelArchitectureProject.BLL/"]
COPY ["MultiLevelArchitectureProject.DAL/MultiLevelArchitectureProject.DAL.csproj", "MultiLevelArchitectureProject.DAL/"]
RUN dotnet restore "./MultiLevelArchitectureProject.WEB/./MultiLevelArchitectureProject.WEB.csproj"
COPY . .
WORKDIR "/src/MultiLevelArchitectureProject.WEB"
RUN dotnet build "./MultiLevelArchitectureProject.WEB.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultiLevelArchitectureProject.WEB.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiLevelArchitectureProject.WEB.dll"]