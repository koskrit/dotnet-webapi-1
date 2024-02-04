# ASP.NET Core CRUD API with IdentityServer4 Authentication

This project is an ASP.NET Core CRUD API that allows users to manage notes. It utilizes Entity Framework Core with a PostgreSQL database for data access. The API endpoints are secured using IdentityServer4, which authenticates users with JWTBearer tokens.

## Features

-  CRUD Operations: Users can Create, Read, Update, and Delete notes.
-  Entity Framework Core: The project follows a code-first approach for database management, utilizing PostgreSQL.
-  IdentityServer4 Authentication: API endpoints are secured using OAuth 2.0 and OpenID Connect provided by IdentityServer4.
-  JWTBearer Token Validation: Access tokens are validated to ensure secure API access.

## Installation

To run this project locally, follow these steps:

1. Clone the repository.
2. Install the required packages.
3. Configure the database connection in the `appsettings.json` file.
4. Build and run the migrations.
5. Build the project.
6. Run the project.

## Contribution

Contributions, suggestions, bug reports, and enhancements are welcome! Please feel free to open an issue or submit a pull request to contribute to this project.
