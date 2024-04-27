#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Systems/Web/MeetingOrganizer.Web/MeetingOrganizer.Web.csproj", "Systems/Web/MeetingOrganizer.Web/MeetingOrganizer.Web.csproj"]
RUN dotnet restore "Systems/Web/MeetingOrganizer.Web/MeetingOrganizer.Web.csproj"
COPY . .
WORKDIR "/src/Systems/Web/MeetingOrganizer.Web"
RUN dotnet build "./MeetingOrganizer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./MeetingOrganizer.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Web/MeetingOrganizer.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 8080