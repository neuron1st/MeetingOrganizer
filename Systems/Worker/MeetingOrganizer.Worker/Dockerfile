#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Worker/MeetingOrganizer.Worker/MeetingOrganizer.Worker.csproj", "Systems/Worker/MeetingOrganizer.Worker/"]
COPY ["Data/MeetingOrganizer.Context/MeetingOrganizer.Context.csproj", "Data/MeetingOrganizer.Context/"]
COPY ["Shared/MeetingOrganizer.Settings/MeetingOrganizer.Settings.csproj", "Shared/MeetingOrganizer.Settings/"]
COPY ["Data/MeetingOrganizer.Context.Entities/MeetingOrganizer.Context.Entities.csproj", "Data/MeetingOrganizer.Context.Entities/"]
COPY ["Services/MeetingOrganizer.Services.EmailSender/MeetingOrganizer.Services.EmailSender.csproj", "Services/MeetingOrganizer.Services.EmailSender/"]
COPY ["Services/MeetingOrganizer.Services.Settings/MeetingOrganizer.Services.Settings.csproj", "Services/MeetingOrganizer.Services.Settings/"]
COPY ["Services/MeetingOrganizer.Services.RabbitMq/MeetingOrganizer.Services.RabbitMq.csproj", "Services/MeetingOrganizer.Services.RabbitMq/"]
COPY ["Shared/MeetingOrganizer.Common/MeetingOrganizer.Common.csproj", "Shared/MeetingOrganizer.Common/"]
COPY ["Shared/MeetingOrganizer.Consts/MeetingOrganizer.Consts.csproj", "Shared/MeetingOrganizer.Consts/"]
RUN dotnet restore "./Systems/Worker/MeetingOrganizer.Worker/./MeetingOrganizer.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/MeetingOrganizer.Worker"
RUN dotnet build "./MeetingOrganizer.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeetingOrganizer.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetingOrganizer.Worker.dll"]