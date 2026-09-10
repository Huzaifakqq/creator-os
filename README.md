# Creator OS

A multi-tenant SaaS platform where creators can build no-code websites, web apps, digital products, courses, memberships, and AI-powered tools — all in one place.

## Tech Stack

| Layer | Technology |
|-------|------------|
| Backend | ASP.NET Core 10 |
| Database | Microsoft SQL Server |
| Web Frontend | React 18 + TypeScript + Vite |
| Mobile | Flutter 3.x + Dart |
| AI | Google Gemini API (free tier) |
| Payments | Paddle / LemonSqueezy (when ready) |

## Project Structure

```
Creator OS/
├── backend/                    # ASP.NET Core solution
│   ├── CreatorOS.sln
│   ├── src/
│   │   ├── CreatorOS.Api/              # Web API entry point
│   │   ├── CreatorOS.Domain/           # Entities, value objects, events
│   │   ├── CreatorOS.Application/      # Business logic, services, DTOs
│   │   ├── CreatorOS.Infrastructure/   # Database, external services
│   │   └── CreatorOS.Shared/           # Cross-cutting concerns
│   └── tests/
│       ├── CreatorOS.UnitTests/
│       └── CreatorOS.IntegrationTests/
│
├── web/                        # React frontend
│   └── creator-os-web/         # Vite + React + TypeScript
│
├── mobile/                     # Flutter mobile app
│   └── creator_os_mobile/      # Flutter + Dart
│
├── docs/                       # Documentation
│   ├── ARCHITECTURE.md
│   ├── API.md
│   └── DATABASE.md
│
└── tools/                      # Scripts, utilities
```

## Getting Started

### Prerequisites

- [.NET 8+ SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org)
- [Flutter 3.x](https://flutter.dev)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Backend

```bash
cd backend
dotnet restore
dotnet run --project src/CreatorOS.Api
```

API available at: `https://localhost:5001` or `http://localhost:5000`

### Web Frontend

```bash
cd web/creator-os-web
npm install
npm run dev
```

Frontend available at: `http://localhost:3000`

### Mobile

```bash
cd mobile/creator_os_mobile
flutter pub get
flutter run
```

## Development

- Backend runs on: `localhost:5000`
- Frontend runs on: `localhost:3000`
- Database: SQL Server Express (local)

## License

Private - All rights reserved.
