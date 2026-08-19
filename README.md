# ElectroPi Support Ticket Management System

## 0. Project Repo

https://github.com/AhmadHamdyAziz/ElectroPi

## 1. Project Overview

ElectroPi Support Ticket Management System is a role-based support ticketing application built with ASP.NET Core Web API and Angular.

The system supports three roles:

- **Admin**
- **Support Agent**
- **Customer**

The backend follows Clean Architecture principles, with business rules kept in the Domain layer and persistence concerns isolated in Infrastructure.

This README documents the implemented functionality, incomplete functionality, architectural decisions, assumptions, limitations, setup instructions, and seeded test accounts.

---

# 2. Requirements Implementation Status

## 2.1 User Roles

### Admin

| Requirement | Database | Backend | Frontend |
|---|---|---|---|
| Manage users | Yes | Yes | Partial |
| View all tickets | Yes | Yes | Yes |
| Assign tickets | Yes | Yes | Not implemented |
| Update priority | Yes | Not implemented | Not implemented |
| Update status | Yes | Not implemented | Not implemented |
| Access dashboard | N/A | Not implemented | Not implemented |

### User Management

The user-management page exists in the frontend, but the current user-list API request is not correctly configured, so users cannot currently be displayed through the UI.

The backend supports:

- User filtering by name.
- Pagination.

User creation is also implemented at the backend level, but the frontend currently points to an incorrect API URL.

The following functionality was intentionally omitted for task simplicity:

- User editing.
- User deletion.
- User activation/deactivation.
- Updating an existing user's role.

Roles can be assigned when creating a user, but changing an existing user's role is not supported.

---

### Support Agent

| Requirement | Database | Backend | Frontend |
|---|---|---|---|
| View assigned tickets | Supported by model | Not fully exposed/tested | Not implemented |
| Update ticket status | Business rules | API not implemented | Not implemented |
| Add comments | Yes | Yes | Yes |
| Log time | Yes | Not implemented | Not implemented |

Agent functionality has not been fully tested because the Agent workflow has not been completed through the API/UI.

The Domain layer contains the relevant ticket state/business rules, but the corresponding APIs have not all been exposed yet.

Time tracking has not been implemented.

---

### Customer

| Requirement | Database | Backend | Frontend |
|---|---|---|---|
| Create tickets | Yes | Yes | Yes |
| View own tickets | Yes | Yes | Yes |
| Add comments | Yes | Yes | Yes |
| Close resolved tickets | Domain rule | API not implemented | Not implemented |

Customer data isolation is implemented.

The system assumes that multiple Customer users may belong to the same Customer entity. A Customer user can therefore see tickets belonging to their own Customer, but cannot access tickets belonging to another Customer.

This isolation is enforced at the backend/API level and is not dependent solely on frontend filtering.

---

# 3. Authentication & Security

| Requirement | Status |
|---|---|
| JWT authentication | Implemented |
| Role-based authorization | Implemented |
| Protected API routes | Implemented |
| Protected Angular routes | Implemented |
| Angular HTTP interceptor for JWT | Implemented |
| Customer data isolation | Implemented |
| Protection against API ID manipulation | Implemented |
| Do not commit secrets or real credentials | **Not complied with** |

JWT authentication is implemented and working.

Role-based authorization is implemented using ASP.NET Core authorization attributes such as:

```csharp
[Authorize(Roles = "...")]
```

The Angular application also protects routes and controls access to pages/actions based on the user's role.

Frontend authorization is primarily used for UI access control; backend authorization remains the security boundary.

---

# 4. Ticket Management

The ticket model supports:

- Automatically generated ID.
- Title.
- Description.
- Status.
- Priority.
- Customer.
- Assigned Agent.
- Ticket activities.
- Comments.

## Ticket Status

The system supports exactly four statuses:

1. Open
2. In Progress
3. Resolved
4. Closed

Status transition rules are implemented in the Domain layer through the ticket state/business-rule implementation.

## Ticket Priority

The supported priorities are:

1. Low
2. Medium
3. High
4. Critical

There is no separate priority-transition validation because the requirements define the priority values but do not specify restrictions on how priority can change.

## Ticket Management Features

| Requirement | Database | Backend | Frontend |
|---|---|---|---|
| Auto-generated ID | Yes | Yes | Yes |
| Title | Yes | Yes | Yes |
| Description | Yes | Yes | Yes |
| Status | Yes | Yes | Yes |
| Priority | Yes | Yes | Yes |
| Pagination | Yes | Yes | Yes |
| Filtering | Yes | Yes | Yes |
| Searching | Yes | Yes | Yes |
| Sorting | Yes | Yes | Yes |
| Status transition validation | Yes | Yes | Yes |
| Priority transition validation | N/A | Not implemented | Not implemented |

---

# 5. Comments & Activity Timeline

Comments and ticket activities are persisted in the database.

The system records activities related to:

- Comments.
- Status changes.
- Priority changes.
- Agent assignment changes.

Comments are stored in a dedicated database table.

Domain events are implemented, dispatched, and logged to the database.

The activity API currently does not expose the actor/user information required to display the actor in the frontend timeline.

The frontend activity/timeline functionality is therefore not yet fully implemented.

| Requirement | Status |
|---|---|
| Persist comments | Implemented |
| Persist status changes | Implemented |
| Persist priority changes | Implemented |
| Persist agent changes | Implemented |
| Persist activity events | Implemented |
| Expose actor through API | Not implemented |
| Display complete activity timeline in UI | Not implemented |

---

# 6. Time Tracking

Time tracking has **not been implemented**.

The following requirements remain outstanding:

- Work date.
- Duration.
- Work description.
- Total time calculation per ticket.
- Work-log API.
- Work-log UI.

---

# 7. Dashboard

The dashboard has **not been implemented due to time limitations**.

The following requirements remain outstanding:

- Ticket counts.
- Open critical ticket count.
- Average resolution time.
- Agent workload.
- At least one chart.

---

# 8. Backend Architecture

The backend follows a Clean Architecture structure:

```text
ElectroPi.SupportTicket
│
├── ElectroPi.SupportTicket.Domain
├── ElectroPi.SupportTicket.Application
├── ElectroPi.SupportTicket.Infrastructure
└── ElectroPi.SupportTicket.Api
```

## Domain

The Domain layer contains the core business model and rules.

It includes, among other components:

- Entities.
- Aggregate roots.
- Entity base classes.
- Ticket aggregate.
- Customer.
- User.
- Ticket activities.
- Comments.
- Domain events.
- Ticket state management.
- Ticket state factory.
- Business rules.

The Domain layer contains ticket state transition rules rather than allowing arbitrary state changes.

## Application

The Application layer contains application use cases and contracts, including:

- Commands.
- Queries.
- Handlers.
- DTOs.
- Application interfaces.
- Application-level authorization logic.

EF Core entities are not exposed directly through API responses; DTOs are used at the application/API boundary.

## Infrastructure

The Infrastructure layer contains implementation details such as:

- Entity Framework Core.
- SQL Server persistence.
- `AppDbContext`.
- Entity configurations.
- Database migrations.
- Seed data.
- Domain event dispatching.
- Current-user implementation.
- Persistence services.

## API

The API layer contains:

- ASP.NET Core controllers.
- JWT authentication.
- Role-based authorization.
- Dependency injection configuration.
- API middleware.
- Swagger.
- Application startup/configuration.

---

# 9. Architectural Patterns

The implementation uses:

- Clean Architecture.
- Domain-Driven Design concepts.
- CQRS-style separation between commands and queries.
- Domain Events.
- Aggregate Roots.
- State Pattern / State Factory.
- Dependency Injection.
- DTO-based API contracts.

The solution deliberately does **not** use:

- Repository Pattern.
- Unit of Work Pattern.

Entity Framework Core's `DbContext` is used directly through the application's database abstraction.

---

# 10. Data Access

Entity Framework Core is used for both read and write operations.

SQL Server is the persistence provider.

The Application layer depends on the application's database abstraction rather than directly depending on Infrastructure implementations.

The system does not expose EF Core entities directly through the API.

---

# 11. Database Model

The database contains the core entities required by the application, including:

- Users.
- Roles.
- Customers.
- Tickets.
- Comments.
- Ticket activities.

Comments are persisted in their own table rather than being stored directly as ticket activities.

Domain events are dispatched and the resulting activities are persisted to the database.

The seed data creates users, roles, customers, and customer-user relationships, but does not create new sample tickets or other sample business data.

---

# 12. Authorization Model

The intended authorization model is:

| Operation | Admin | Support Agent | Customer |
|---|:---:|:---:|:---:|
| Manage users | ✓ | | |
| View all tickets | ✓ | | |
| Assign tickets | ✓ | | |
| Update ticket priority | Planned | | |
| Update ticket status | Planned | ✓* | |
| View assigned tickets | | ✓* | |
| Create tickets | | | ✓ |
| View own customer tickets | | | ✓ |
| Add comments | | ✓ | ✓ |
| Resolve tickets | | ✓* | |
| Close resolved tickets | | | ✓* |
| Log work | | ✓* | |

`*` indicates that the business/domain rules exist or the functionality is part of the intended design, but the corresponding API/UI implementation is incomplete.

---

# 13. Frontend Architecture

The frontend is implemented using Angular.

The application is organized into components and services for the major application features, including:

- Authentication.
- Tickets.
- Ticket creation.
- Ticket details.
- Comments.
- Activities.
- Ticket actions.
- User management.
- Dashboard-related areas.

The frontend uses:

- Angular Signals.
- TypeScript.
- Angular Material.
- HTTP Client.
- Forms.
- Route Guards.
- HTTP interceptors.
- Services.
- Role-based UI access.

Angular environment TypeScript files are used to configure environment-specific values such as the backend API URL.

### Lazy Loading

Lazy loading has not been explicitly verified as part of the current implementation and is therefore not claimed as an implemented requirement.

---

# 14. Error Handling, Validation & Logging

The backend includes:

- Dependency Injection.
- Input validation.
- Structured logging.
- DTO-based API contracts.

Centralized exception handling has **not** been implemented.

---

# 15. Setup Instructions

## Prerequisites

The following are required:

- .NET 8 SDK.
- Node.js.
- npm.
- Angular CLI.
- SQL Server.
- Entity Framework Core CLI tools.
- Visual Studio 2022 or another suitable IDE.

The application was developed/tested using:

- **.NET 8**
- **Angular 22**
- **Node.js 26**
- **SQL Server**

---

# 16. Backend Setup

Clone the repository and restore the backend dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Configure the SQL Server connection string in the API project's configuration.

Apply the database migrations:

```bash
dotnet ef database update --project ElectroPi.SupportTicket.Infrastructure --startup-project ElectroPi.SupportTicket.Api --context AppDbContext
```

Run the API:

```bash
dotnet run
```

---

# 17. Database Seeding

The database seeder creates:

- Roles.
- Users.
- Customers.
- Customer-user relationships.

It does not create new sample tickets or additional sample business data.

After the database has been created/migrated, the application's seeding process creates the required initial data according to the configured startup/seeding behavior.

---

# 18. Frontend Setup

Navigate to the Angular project and install dependencies:

```bash
npm install
```

Start the development server:

```bash
ng serve
```

The application will normally be available at:

```text
http://localhost:4200
```

The backend API URL is configured through the Angular environment TypeScript files.

Make sure the configured URL matches the ASP.NET Core API URL.

---

# 19. API Documentation

Swagger is available in the API project for exploring and testing the available endpoints.

When the API is running, open:

```text
https://localhost:<port>/swagger
```

The exact port depends on the ASP.NET Core launch configuration.

No separate Postman collection is currently provided.

No separate OpenAPI link is currently provided beyond the Swagger documentation exposed by the application.

---

# 20. Testing

Automated tests have **not been implemented due to time limitations**.

The following mandatory testing requirements therefore remain outstanding:

- Business-rule unit tests.
- Backend unit tests.
- API integration tests.
- Customer data-isolation tests.
- Frontend unit tests.

---

# 21. Seeded Test Accounts

The current seed data contains the following accounts:

| Email | Display Name | Role |
|---|---|---|
| `customer@demo.com` | Demo Customer | Customer |
| `Agent_B@ticketing.com` | — | SupportAgent |
| `Admin@ticketing.com` | — | Admin |
| `Agent_A@ticketing.com` | — | SupportAgent |
| `customer@test.com` | Test Customer | Customer |

The passwords are defined by the application's seeder.

Because credentials and secrets were committed to source control during development, the actual seeded passwords should be taken from the seeder/configuration rather than duplicated in this README.

---

# 22. Deliverables Status

| Deliverable | Status |
|---|---|
| Git repository | https://github.com/AhmadHamdyAziz/ElectroPi |
| Full source code | Available in the solution |
| Database migrations | Available |
| Seeded test accounts | Available |
| README.md | This document |
| Postman collection | Not provided |
| Swagger | Available |
| Separate OpenAPI link | Not provided |
| Screenshots | Not provided |
| Demonstration video | Not provided |

---

# 23. Limitations & Assumptions

## 23.1 Intentional Simplifications

The following functionality was intentionally omitted for task simplicity:

- User editing.
- User deletion.
- User activation/deactivation.
- Changing an existing user's role.

These were excluded intentionally rather than because of technical limitations.

---

## 23.2 Incomplete API/UI Functionality

Some functionality exists at the Domain, Infrastructure, or API level but has not yet been exposed through the complete API/UI flow.

Examples include:

- Ticket assignment UI.
- Ticket status update API.
- Ticket closing API.
- Agent ticket workflow.
- Complete activity timeline UI.
- Actor information in activity responses.
- User-management UI integration.

---

## 23.3 Customer Isolation Assumption

The system assumes that a Customer represents the customer entity/organization.

Multiple Customer users can belong to the same Customer.

Customer ticket visibility is therefore based on the authenticated user's Customer ID.

A Customer user cannot access tickets belonging to another Customer through API manipulation.

---

## 23.4 Priority Transitions

No priority transition rules were introduced because the requirements define priority values but do not specify restrictions on changing priority.

---

## 23.5 Ticket Closing

The Domain model contains the rule that a ticket can only be closed after it has been resolved.

The corresponding API and frontend functionality have not yet been implemented.

---

## 23.6 Dashboard

The dashboard was not implemented due to time limitations.

---

## 23.7 Time Tracking

Time tracking was not implemented.

---

## 23.8 Testing

Automated testing was not implemented due to time limitations.

---

## 23.9 Centralized Exception Handling

Centralized exception handling was not implemented.

---

# 24. Repository Security Note

The assessment explicitly states:

> **Do NOT commit secrets or real credentials.**

This requirement was **not followed in the current implementation**.

Configuration values, credentials, and secrets were committed to source control during development, and no external secret-management mechanism was configured.

This was done for development/testing simplicity and should be addressed before using the application in a production environment.

### Production Recommendations

Before production deployment:

- Remove secrets and credentials from source control.
- Rotate any credentials or secrets that have already been committed.
- Move sensitive configuration to environment variables, .NET User Secrets, or a dedicated secret-management solution.
- Ensure connection strings, JWT signing keys, passwords, and other sensitive values are not stored in the repository.
- Review the complete Git history because removing a secret from the latest commit does not remove it from previous Git commits.

> **Important:** Any credential that has been committed to a repository should be considered compromised, even if the corresponding value is later removed from the latest version of the source code.

---

# 25. Bonus Features

None of the optional bonus features were implemented.

| Bonus Feature | Status |
|---|---|
| Refresh Token Rotation | Not implemented |
| Docker Compose | Not implemented |
| SignalR | Not implemented |
| Optimistic Concurrency | Not implemented |
| CI Pipeline | Not implemented |
| Caching | Not implemented |
| Rate Limiting | Not implemented |

---

# 26. Known Frontend Issues

## User List

The user-management page exists, but its API request is currently pointing to an incorrect endpoint, so the user list cannot currently be loaded through the UI.

The backend filtering and pagination functionality is available.

## Create User

The create-user UI exists, but the HTTP request currently points to an incorrect API URL.

## Ticket Assignment

The ticket assignment API exists, but the functionality is not currently exposed through the Angular UI.

These issues represent incomplete frontend/backend integration rather than an absence of the corresponding backend/domain functionality.

---

# 27. Current Implementation Summary

The current implementation provides the core foundation of the support ticket system:

- JWT authentication.
- Role-based authorization.
- Customer data isolation.
- Ticket creation.
- Ticket viewing.
- Ticket status model and transition rules.
- Ticket priority.
- Comments.
- Ticket activities.
- Activity persistence.
- Ticket assignment API.
- User management backend functionality.
- Pagination and filtering.
- EF Core persistence.
- Database migrations.
- Seed data.
- Angular frontend.
- Protected frontend routes.
- Role-aware frontend behavior.
- Swagger API documentation.
- Domain events and database activity logging.

The main incomplete areas are:

- Dashboard.
- Time tracking.
- Automated testing.
- Complete Agent workflow.
- Customer ticket closing API/UI.
- Complete ticket action UI.
- Complete activity timeline UI.
- Complete user-management UI integration.
- Centralized exception handling.
- Repository security/secret management.

The implementation prioritizes the core architecture, business rules, persistence, authentication, authorization, customer data isolation, and ticketing functionality while leaving several secondary features incomplete due to the available implementation time.

---

# 28. Assessment Compliance Summary

| Assessment Requirement | Status |
|---|---|
| JWT authentication | Implemented |
| Role-based authorization | Implemented |
| Protected routes | Implemented |
| Customer data isolation | Implemented |
| Ticket management | Partially implemented |
| Comments | Implemented |
| Activity timeline | Partially implemented |
| Time tracking | Not implemented |
| Dashboard | Not implemented |
| Clean architecture | Implemented |
| Dependency Injection | Implemented |
| DTOs | Implemented |
| Centralized exception handling | Not implemented |
| Input validation | Implemented |
| Structured logging | Implemented |
| EF migrations | Implemented |
| Seed data | Implemented |
| Organized Angular components/services | Implemented |
| Lazy loading | Not verified |
| Route guards | Implemented |
| HTTP interceptor | Implemented |
| Backend unit tests | Not implemented |
| Integration tests | Not implemented |
| Data isolation tests | Not implemented |
| Frontend unit tests | Not implemented |
| Git repository | Not committed yet |
| Postman collection | Not provided |
| Swagger | Available |
| Screenshots/video | Not provided |
| Secrets excluded from source control | **Not complied with** |
| Bonus features | Not implemented |