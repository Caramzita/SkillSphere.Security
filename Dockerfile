FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN dotnet nuget add source http://host.docker.internal:5000/v3/index.json -n baget

COPY ["src/SkillSphere.Security.API/SkillSphere.Security.API.csproj", "src/SkillSphere.Security.API/"]
COPY ["src/SkillSphere.Security.DataAccess/SkillSphere.Security.DataAccess.csproj", "src/SkillSphere.Security.DataAccess/"]
COPY ["src/SkillSphere.Security.Contracts/SkillSphere.Security.Contracts.csproj", "src/SkillSphere.Security.Contracts/"]
COPY ["src/SkillSphere.Security.Core/SkillSphere.Security.Core.csproj", "src/SkillSphere.Security.Core/"]
COPY ["src/SkillSphere.Security.UseCases/SkillSphere.Security.UseCases.csproj", "src/SkillSphere.Security.UseCases/"]
RUN dotnet restore "./src/SkillSphere.Security.API/SkillSphere.Security.API.csproj"

COPY . .
WORKDIR "/src/src/SkillSphere.Security.API"
RUN dotnet build "./SkillSphere.Security.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SkillSphere.Security.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillSphere.Security.API.dll"]