#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/MeetingOrganizer.Api/MeetingOrganizer.Api.csproj", "Systems/Api/MeetingOrganizer.Api/"]
COPY ["Data/MeetingOrganizer.Context.MigrationsPostgreSQL/MeetingOrganizer.Context.MigrationsPostgreSQL.csproj", "Data/MeetingOrganizer.Context.MigrationsPostgreSQL/"]
COPY ["Data/MeetingOrganizer.Context.Entities/MeetingOrganizer.Context.Entities.csproj", "Data/MeetingOrganizer.Context.Entities/"]
COPY ["Data/MeetingOrganizer.Context/MeetingOrganizer.Context.csproj", "Data/MeetingOrganizer.Context/"]
COPY ["Shared/MeetingOrganizer.Settings/MeetingOrganizer.Settings.csproj", "Shared/MeetingOrganizer.Settings/"]
COPY ["Data/MeetingOrganizer.Context.Seeder/MeetingOrganizer.Context.Seeder.csproj", "Data/MeetingOrganizer.Context.Seeder/"]
COPY ["Services/MeetingOrganizer.Services.UserAccount/MeetingOrganizer.Services.UserAccount.csproj", "Services/MeetingOrganizer.Services.UserAccount/"]
COPY ["Shared/MeetingOrganizer.Common/MeetingOrganizer.Common.csproj", "Shared/MeetingOrganizer.Common/"]
COPY ["Services/MeetingOrganizer.Services.Actions/MeetingOrganizer.Services.Actions.csproj", "Services/MeetingOrganizer.Services.Actions/"]
COPY ["Shared/MeetingOrganizer.Consts/MeetingOrganizer.Consts.csproj", "Shared/MeetingOrganizer.Consts/"]
COPY ["Services/MeetingOrganizer.Services.EmailSender/MeetingOrganizer.Services.EmailSender.csproj", "Services/MeetingOrganizer.Services.EmailSender/"]
COPY ["Services/MeetingOrganizer.Services.Settings/MeetingOrganizer.Services.Settings.csproj", "Services/MeetingOrganizer.Services.Settings/"]
COPY ["Services/MeetingOrganizer.Services.RabbitMq/MeetingOrganizer.Services.RabbitMq.csproj", "Services/MeetingOrganizer.Services.RabbitMq/"]
COPY ["Services/MeetingOrganizer.Services.Cache/MeetingOrganizer.Services.Cache.csproj", "Services/MeetingOrganizer.Services.Cache/"]
COPY ["Services/MeetingOrganizer.Services.CommentLikes/MeetingOrganizer.Services.CommentLikes.csproj", "Services/MeetingOrganizer.Services.CommentLikes/"]
COPY ["Services/MeetingOrganizer.Services.Comments/MeetingOrganizer.Services.Comments.csproj", "Services/MeetingOrganizer.Services.Comments/"]
COPY ["Services/MeetingOrganizer.Services.Participants/MeetingOrganizer.Services.Participants.csproj", "Services/MeetingOrganizer.Services.Participants/"]
COPY ["Services/MeetingOrganizer.Services.Logger/MeetingOrganizer.Services.Logger.csproj", "Services/MeetingOrganizer.Services.Logger/"]
COPY ["Services/MeetingOrganizer.Services.MeetingLikes/MeetingOrganizer.Services.MeetingLikes.csproj", "Services/MeetingOrganizer.Services.MeetingLikes/"]
COPY ["Services/MeetingOrganizer.Services.Meetings/MeetingOrganizer.Services.Meetings.csproj", "Services/MeetingOrganizer.Services.Meetings/"]
COPY ["Services/MeetingOrganizer.Services.Photos/MeetingOrganizer.Services.Photos.csproj", "Services/MeetingOrganizer.Services.Photos/"]
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