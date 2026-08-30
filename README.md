# JobTrack Professional — Full Stack

A full-stack job and internship tracker built with **React + Vite**, **ASP.NET Core 8**, **MySQL**, **JWT authentication**, and **Entity Framework Core**.

## Features
- Register and login
- BCrypt password hashing
- JWT authentication
- User-isolated data (each user sees only their own applications)
- Full CRUD for applications
- Dashboard, analytics, pipeline and calendar
- Search and status filtering
- Interview dates, priority and notes
- Dark/light mode
- Responsive interface
- Swagger API documentation

## Project structure
- `frontend/` React + Vite
- `backend/` ASP.NET Core Web API
- `database/` MySQL SQL script

---
## 1. Start MySQL

### Option A — MySQL Workbench
Create/import the database using `database/jobtrack_mysql.sql`.

Update `backend/appsettings.json`:
```json
"DefaultConnection":"server=localhost;port=3306;database=jobtrack;user=root;password=YOUR_MYSQL_PASSWORD"
```

### Option B — Docker
From the project root:
```bash
docker compose up -d
```
Then use password `root` in the backend connection string.

---
## 2. Start backend
Requirements: .NET 8 SDK.

```bash
cd backend
dotnet restore
dotnet run
```

Backend API:
- http://localhost:5167/api/health
- Swagger: http://localhost:5167/swagger

The backend automatically creates missing tables using EF Core `EnsureCreated()`.

---
## 3. Start frontend
Open a second terminal:
```bash
cd frontend
npm install
npm run dev
```
Open the Vite URL shown in the terminal (usually `http://localhost:5173`).

## Important startup order
1. Start MySQL
2. Start backend
3. Start frontend
4. Create an account
5. Login and start adding applications

## API endpoints
- POST `/api/auth/register`
- POST `/api/auth/login`
- GET `/api/applications`
- GET `/api/applications/{id}`
- POST `/api/applications`
- PUT `/api/applications/{id}`
- DELETE `/api/applications/{id}`

Protected application endpoints require a Bearer JWT token.
