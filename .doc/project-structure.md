[Back to README](../README.md)

## Project Structure

The project should be structured as follows:

```
backend/
├── src/
│   ├── Ambev.DeveloperEvaluation.API/
│   │   ├── Controllers/
│   │   ├── Configurations/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── Ambev.DeveloperEvaluation.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Mappings/
│   ├── Ambev.DeveloperEvaluation.Domain/
│   │   ├── Entities/
│   │   │   └── User.cs
│   │   ├── Interfaces/
│   │   └── Services/
│   ├── Ambev.DeveloperEvaluation.Infrastructure/
│   │   ├── Context/
│   │   ├── Repositories/
│   │   └── Migrations/
│   └── Ambev.DeveloperEvaluation.Tests/
│       ├── Integration/
│       └── Unit/
├── Dockerfile
├── docker-compose.yml
└── docker-compose.override.yml
```
