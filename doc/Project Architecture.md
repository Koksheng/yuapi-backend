## Project Overview and Architecture

The project consists of several interconnected services and components that work together to handle API requests, process them, and track their usage. Here’s a detailed breakdown of the workflow and the components involved:

### Components

**1. UserInterfaceInfoController (yuapi.Api)**
- **URL: `http://localhost:5218/`**
- **Role**: Handles incoming requests to invokeInterfaceInfo and forwards them to the appropriate command handler.

**2. InvokeInterfaceInfoCommandHandler (yuapi.Application)**
- **URL: `http://localhost:5218/`**
- **Role**: Processes the command by referencing the `YuApiClient SDK (yuapi-client-sdk)` to trigger an external API call.

**3. YuApiClient SDK (yuapi-client-sdk)**
- **Purpose**: Makes HTTP client requests to external services.
- **Configured Base Address**: `http://127.0.0.1:8090`

**4. Ocelot API Gateway (yuapi-OcelotGateway)**
- **URL: `http://localhost:8090/`**
- **Role**: Acts as an API gateway, processing requests through various middleware components before forwarding them to the final destination.
- **Middleware Pipeline**:
	1. **RequestLoggingMiddleware**: Logs the incoming requests.
	2. **AccessControlMiddleware**: Checks interface information via `InterfaceInfoService` using gRPC at `yuapi.RPC.ServiceCenter (http://localhost:5266)`.
	3. **UserVerificationMiddleware**: Verifies user information via `GetUserInfo` service using gRPC at `yuapi.RPC.ServiceCenter (http://localhost:5266)`.
	4. **ResponseHandlingMiddleware**:
		- Executes the API logic by calling `GetUsernameByPost (http://localhost:8123/)`.
		- Updates the invocation count via `InvokeCountService` using gRPC at `yuapi.RPC.ServiceCenter (http://localhost:5266)`.


### Workflow
1. **UserInterfaceInfoController** receives a request to `invokeInterfaceInfo`.
2. The request is forwarded to the `InvokeInterfaceInfoCommandHandler`.
3. The **InvokeInterfaceInfoCommandHandler** uses the **YuApiClient SDK** to send a `PostAsync` request.
4. The request is routed to the **Ocelot API Gateway** at `http://127.0.0.1:8090`.
5. **Ocelot API Gateway** processes the request through its middleware pipeline:
	- **RequestLoggingMiddleware** logs the request.
	- **AccessControlMiddleware** verifies the interface information via gRPC.
	- **UserVerificationMiddleware** verifies the user information via gRPC.
	- **ResponseHandlingMiddleware**:
		- Executes the API logic by calling `GetUsernameByPost (http://localhost:8123/)`.
		- Updates the invocation count via gRPC.
6. The final response is returned back through the same route.


### Diagram

```
[Client Request] --> [UserInterfaceInfoController (yuapi.Api)]
                         |
                         v
         [InvokeInterfaceInfoCommandHandler (yuapi.Application)]
                         |
                         v
                 [YuApiClient SDK]
                         |
                         v
[Ocelot API Gateway (yuapi-OcelotGateway)]
                         |
      -------------------------------------------------
     |                 |                  |             |
     v                 v                  v             v
[RequestLogging] [AccessControl] [UserVerification] [ResponseHandling]
                         |
                         v
       [GetUsernameByPost (http://localhost:8123/)]
                         |
                         v
       [InvokeCountService (yuapi.RPC.ServiceCenter)]
                         |
                         v
                    [Final Response]
```