## yuapi-backend.sln
**Solution File**: This is the main solution file that includes all the backend projects.

## yuapi.Api (Start)
**Function**: This project likely serves as the main API application.

**Features**:
Exposes different endpoints for clients.
Handles incoming HTTP requests and processes them.
Includes controllers, such as the UserController, InterfaceInfoController, UserInterfaceInfoController.

## yuapi.Application
**Function**: This project contains the application logic and services.

**Features**:
Implements business rules.
Provides application services that interact with the domain and infrastructure layers.
Contains interfaces and implementations for various operations needed by the **yuapi.Api**.

## yuapi.Contracts
**Function**: This project defines the data transfer objects (DTOs) and contracts used for communication.

**Features**:
Defines request and response models for API endpoints.
Ensures consistency in the data exchanged between the client and server.

## yuapi.Domain
**Function**: This project contains the domain models and business logic.

**Features**:
Defines the core entities and value objects.
Implements domain services and business rules.
Contains domain events and other domain-related logic.

## yuapi.Infrastructure
**Function**: This project handles the infrastructure concerns like data access and external service integrations.

**Features**:
Implements repositories and data context for database interactions.
Contains configurations and setups for external services.
Handles data persistence and retrieval operations.

## yuapi.RPC.ServiceCenter (Start)
**Function**: This project hosts multiple gRPC services.

**Features**: Provides inter-service communication via gRPC, allowing other services to call these methods.

`InvokeCountService`: Manages the invocation count of various API methods.

`InterfaceInfoService`: Retrieves interface information based on path and method.

`UserInfoService`: Retrieves user information based on access keys.



## yuapi-interface
**Function**: This is a separate solution focused on exposing API endpoints and providing a client SDK.

**Features**:

`yuapi-interface.sln`: Solution file for the interface project.

`yuapi-client-sdk (YuApiClient)`: A client SDK that allows external applications to interact with the yuapi-interface APIs.

`yuapi-interface` **(Start)**: Contains controllers like NameController for handling requests such as GetNameByGet, GetNameByPost, and GetUsernameByPost.

`yuapi-interface-console-app`: A console application that references the YuApiClient to interact with the yuapi-interface controllers for testing and demonstration purposes.

## yuapi-OcelotGateway (Start)
**Function**: This project serves as an API gateway using Ocelot.

**Features**: Uses middleware components like `AccessControlMiddleware`, `RequestLoggingMiddleware`, `UserVerificationMiddleware`, and `ResponseHandlingMiddleware` to implement the features.

**Access Control**: Manages access control to the APIs.

**User Verification**: Verifies users before allowing them to access certain endpoints.

**Request Redirect**: Redirects requests to appropriate backend services.

**Response Handling**: Handles and modifies responses before sending them back to the client.

**Request Logging**: Logs incoming requests for auditing and debugging purposes.
