## Yuapi API
An API platform for users to register, share, and track their APIs.

.NET Core + React fullstack project

**Frontend**: https://github.com/Koksheng/yuapi-frontend

**Backend**: https://github.com/Koksheng/yuapi-backend

## Project Description

Yuapi API is a full-stack project built with a .NET Core backend and a React frontend. 

Administrators can access and publish interfaces, as well as perform statistics and analysis on the calls of each interface. Users can register and log in to activate interface call permissions, browse interfaces, debug online, and use the client SDK to easily call interfaces in code.

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
![image](https://github.com/user-attachments/assets/2f87cf95-c901-4f8e-80bb-6a7c0de0f4bc)

## Interface Info
![image](https://github.com/user-attachments/assets/1caccab2-d177-469e-a8a2-28fc1afd1448)

### Interface Info Free Trial
![image](https://github.com/user-attachments/assets/af1c8931-c0f2-4379-8629-268a71f4c975)

### Interface Online Invoke
**GetUsernameByPost**

![image](https://github.com/user-attachments/assets/27b143ef-a693-4fb8-af78-4c84a665ef38)

**GetRandomWallpaper**

![image](https://github.com/user-attachments/assets/89f55979-cd91-4902-afda-85b61e51cd81)

### NuGet Package to Invoke Interface
1. Install `yuapi-client-sdk`
   ![image](https://github.com/user-attachments/assets/21abcbd9-a37b-4576-94f0-91091bba9c6c)

2. With a few lines of code, you can invoke the interface:
   ```
	 using yuapi_client_sdkyuapi_client_sdk.Client;

	 var httpClient = new HttpClient { BaseAddress = new Uri("http://127.0.0.1:8090") };
	 var yuApiClient = new YuApiClient(httpClient);
	 
	 yuApiClient.SetAccessKey("9d8ebf4636e2dec32117b9d72e1a3d70");
	 yuApiClient.SetSecretKey("296791011ea2292bc7efc73deeaddc45");
	 var result = await yuApiClient.InvokeAsync("GetUsernameByPost", "{\"username\":\"exampleUser\"}");
   ```


## Interface Management
![image](https://github.com/user-attachments/assets/74527d4b-e996-4ade-ac82-1d918dac417c)

## Interface Analysis
![image](https://github.com/user-attachments/assets/80f1ca5b-2bff-4654-864a-a3830c0ae8fa)

## User Management
![image](https://github.com/user-attachments/assets/f2f2ec19-a33a-4682-b8b6-1841c27a4710)

## Personal Info
![image](https://github.com/user-attachments/assets/a1d33ef6-3092-4267-a98b-fc848114d1a3)




## Project Structure:
![Untitled Diagram drawio](https://github.com/user-attachments/assets/3c9f309c-7c72-46b4-9885-d907e2616edb)




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
