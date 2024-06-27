## Yuapi API
An API platform for users to register, share, and track their APIs.

.NET Core + React fullstack project

**Frontend**: https://github.com/Koksheng/yuapi-frontend

**Backend**: https://github.com/Koksheng/yuapi-backend

## Project Description

Yuapi API is a full-stack project built with a .NET Core backend and a React frontend. It allows users to register their APIs, make them available for others to invoke, and track usage statistics. This platform provides a centralized solution for API management, ensuring easy sharing and detailed analytics.

## Tech Stack

### Frontend
- Development framework: React, Umi
- Scaffolding: Ant Design Pro
- Component library: Ant Design, Ant Design Components
- Plugin: OpenAPI frontend code generation

### Backend
- Framework: .NET Core 
- Libraries: 
	- MediatR (for CQRS and Mediator pattern)
	- FluentValidation (for validation)
	- Entity Framework Core (for data access)
	- AutoMapper (for object-to-object mapping)
	- Ocelot (for API Gateway)
- Database: Microsoft SQL Server
- Middleware: Custom middleware for user verification, access control, request logging, and response handling
- RPC Services: gRPC (for inter-service communication)

## Login
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/60e008cd-741f-4d3f-a829-81742938b3dd)

## Register
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/f2c72a89-f43b-469c-a87b-8167af57bb63)

## Main Page
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/4efdd33c-a008-4ebd-8524-662c30ede58a)

## Interface Info
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/80aec7ef-2d6d-41d8-8646-cd1c343c3e5b)

## Interface Management
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/63eb95a7-d9d7-40a6-9c85-dfd67425dca1)

## Interface Analysis
![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/c8187962-49f3-4e6d-9165-038ac0eb19c8)




## Project Structure:

### yuapi-backend.sln
**Solution File**: This is the main solution file that includes all the backend projects.

### yuapi.Api (Start)
**Function**: This project likely serves as the main API application.

**Features**:
Exposes different endpoints for clients.
Handles incoming HTTP requests and processes them.
Includes controllers, such as the UserController, InterfaceInfoController, UserInterfaceInfoController.

### yuapi.Application
**Function**: This project contains the application logic and services.

**Features**:
Implements business rules.
Provides application services that interact with the domain and infrastructure layers.
Contains interfaces and implementations for various operations needed by the **yuapi.Api**.

### yuapi.Contracts
**Function**: This project defines the data transfer objects (DTOs) and contracts used for communication.

**Features**:
Defines request and response models for API endpoints.
Ensures consistency in the data exchanged between the client and server.

### yuapi.Domain
**Function**: This project contains the domain models and business logic.

**Features**:
Defines the core entities and value objects.
Implements domain services and business rules.
Contains domain events and other domain-related logic.

### yuapi.Infrastructure
**Function**: This project handles the infrastructure concerns like data access and external service integrations.

**Features**:
Implements repositories and data context for database interactions.
Contains configurations and setups for external services.
Handles data persistence and retrieval operations.

### yuapi.RPC.ServiceCenter (Start)
**Function**: This project hosts multiple gRPC services.

**Features**: Provides inter-service communication via gRPC, allowing other services to call these methods.

`InvokeCountService`: Manages the invocation count of various API methods.

`InterfaceInfoService`: Retrieves interface information based on path and method.

`UserInfoService`: Retrieves user information based on access keys.



### yuapi-interface
**Function**: This is a separate solution focused on exposing API endpoints and providing a client SDK.

**Features**:

`yuapi-interface.sln`: Solution file for the interface project.

`yuapi-client-sdk (YuApiClient)`: A client SDK that allows external applications to interact with the yuapi-interface APIs.

`yuapi-interface` **(Start)**: Contains controllers like NameController for handling requests such as GetNameByGet, GetNameByPost, and GetUsernameByPost.

`yuapi-interface-console-app`: A console application that references the YuApiClient to interact with the yuapi-interface controllers for testing and demonstration purposes.

### yuapi-OcelotGateway (Start)
**Function**: This project serves as an API gateway using Ocelot.

**Features**: Uses middleware components like `AccessControlMiddleware`, `RequestLoggingMiddleware`, `UserVerificationMiddleware`, and `ResponseHandlingMiddleware` to implement the features.

**Access Control**: Manages access control to the APIs.

**User Verification**: Verifies users before allowing them to access certain endpoints.

**Request Redirect**: Redirects requests to appropriate backend services.

**Response Handling**: Handles and modifies responses before sending them back to the client.

**Request Logging**: Logs incoming requests for auditing and debugging purposes.
