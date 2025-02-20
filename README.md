# IwMetrics - Portfolio Insights

A .NET Core Web API that allows users to manage investment portfolios and share insights with others.


## Features
 ## User authentication (JWT-based)  
 ## Role-based authorization (User, Manager, Admin)  
 ## Portfolio and Asset Management  
 ## Secure API endpoints with MediatR and CQRS  
 ## Clean Architecture and .NET Core Web API  


## Tech Stack
- .NET Core 6
- Entity Framework Core
- MediatR
- AutoMapper
- JWT Authentication
- SQL Server
- Clean Architecture with CQRS

## Installation & Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/MutteshwarGowda/PortfolioInsights-API.git

2. Navigate to project directory

   cd InvestmentMetrics-Portfolio

3. Apply Migrations (Since migrations are not included in the repo)

   dotnet ef migrations add InitialCreate

   # Update Database
     dotnet ef database update

4. Run the application


## API Endpoints
- POST /api/auth/register - Register a new user
- POST /api/auth/login - Login and receive JWT token
- GET /api/portfolio - Get all portfolios (requires authentication)