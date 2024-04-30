# Meeting Organizer

A website where you can organize collective meetings and parties

## Features

- Create, edit, view, delete meetings
- Comment on meetings, ability to like comments
- Ability to join a meeting, ability to like meetings
- Email notification to all meeting participants when the meeting date is changed

## Technology Stack

- Backend: .NET 8.0, ASP.NET Core, Entity Framework Core, IdentityServer4, RabbitMQ, Redis, MailKit
- Frontend: Blazor, MudBlazor
- Database: PostgreSQL
- Containerization: Docker, Docker Compose

## Getting Started

1. Clone the repository
	```
	git clone https://github.com/neuron1st/MeetingOrganizer.git
	```

2. Configure email sender in env.worker file:
	```
	EMAILSENDER__HOST=
	EMAILSENDER__PORT=465
	EMAILSENDER__USESSL=true
	EMAILSENDER__EMAIL=
	EMAILSENDER__SENDERNAME=
	EMAILSENDER__PASSWORD=
	```

3. Execute following commands in main directory:
	```
	docker-compose build
	```
	```
	docker-compose up
	```

4. Open in browser:
- Web client: [http://localhost:10002](http://localhost:10002)
- Swagger: [http://localhost:10000](http://localhost:10000)

