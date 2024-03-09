#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/MeetingOrganizer.Api/MeetingOrganizer.Api.csproj", "Systems/Api/MeetingOrganizer.Api/"]
COPY ["Data/MeetingOrganizer.Context.MigrationsPostgreSQL/MeetingOrganizer.Context.MigrationsPostgreSQL.csproj", "Data/MeetingOrganizer.Context.MigrationsPostgreSQL/"]
COPY ["Data/MeetingOrganizer.Context.Entities/MeetingOrganizer.Context.Entities.csproj", "Data/MeetingOrganizer.Context.Entities/"]
COPY ["Data/MeetingOrganizer.Context/MeetingOrganizer.Context.csproj", "Data/MeetingOrganizer.Context/"]
COPY ["Shared/MeetingOrganizer.Settings/MeetingOrganizer.Settings.csproj", "Shared/MeetingOrganizer.Settings/"]
COPY ["Services/MeetingOrganizer.Services.Settings/MeetingOrganizer.Services.Settings.csproj", "Services/MeetingOrganizer.Services.Settings/"]
COPY ["Shared/MeetingOrganizer.Common/MeetingOrganizer.Common.csproj", "Shared/MeetingOrganizer.Common/"]
RUN dotnet restore "./Systems/Api/MeetingOrganizer.Api/./MeetingOrganizer.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/MeetingOrganizer.Api"
RUN dotnet build "./MeetingOrganizer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeetingOrganizer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetingOrganizer.Api.dll"]