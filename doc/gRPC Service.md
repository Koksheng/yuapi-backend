# gRPC Service Implementation (Server)

In your yuapi.RPC.InvokeCountService project, you implement the gRPC service. This project includes the invoke_count.proto file and is configured to generate the server-side code:

```
<ItemGroup>
  <Protobuf Include="Protos\invoke_count.proto" GrpcServices="Server" />
</ItemGroup>
```

## Using gRPC Service (Client)
Any other project that needs to call the InvokeCountService must include the invoke_count.proto file and configure it to generate the client-side code. This is typically done by including the following in the consuming project's .csproj file:

```
<ItemGroup>
  <Protobuf Include="Protos\invoke_count.proto" GrpcServices="Client" />
</ItemGroup>
```

### Example Scenario

1. gRPC Service Implementation (yuapi.RPC.InvokeCountService):
	- Implements the InvokeCountService.
	- Configured to generate server-side code from invoke_count.proto.
```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.19.1" />
    <PackageReference Include="Grpc.AspNetCore" Version="2.37.0" />
    <PackageReference Include="Grpc.Tools" Version="2.37.0" PrivateAssets="All" />
  </ItemGroup>

  <ItemGroup>
    <Protobuf Include="Protos\invoke_count.proto" GrpcServices="Server" />
  </ItemGroup>

</Project>
```

2. Consuming the gRPC Service (yuapi-OcelotGateway or any other API that needs to call the InvokeCountService):
    - Includes the invoke_count.proto file.
    - Configured to generate client-side code from invoke_count.proto.
```
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.19.1" />
    <PackageReference Include="Grpc.Net.Client" Version="2.37.0" />
    <PackageReference Include="Grpc.Tools" Version="2.37.0" PrivateAssets="All" />
  </ItemGroup>

  <ItemGroup>
    <Protobuf Include="Protos\invoke_count.proto" GrpcServices="Client" />
  </ItemGroup>

</Project>

```

### Practical Steps for Adding Client-Side gRPC

1. Copy invoke_count.proto to the Consuming Project:
    - Make sure the invoke_count.proto file is copied to the Protos folder of the consuming project (e.g., yuapi-OcelotGateway).
2. Configure the .csproj File:
    - Update the .csproj file of the consuming project to include the Protos folder and set GrpcServices to Client.
3. Rebuild the Project:
    - After these changes, rebuild the consuming project to generate the client code.

### Example Usage in Consuming Project
Here is an example of how you might use the generated gRPC client in your consuming project (e.g., yuapi-OcelotGateway):

```
using Grpc.Net.Client;
using yuapi.RPC.InvokeCountService;

namespace yuapi_OcelotGateway.Services
{
    public class InvokeCountServiceClient
    {
        private readonly InvokeCount.InvokeCountClient _client;

        public InvokeCountServiceClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new InvokeCount.InvokeCountClient(channel);
        }

        public async Task<UpdateCountReply> UpdateCountAsync(int interfaceInfoId, int userId)
        {
            var request = new UpdateCountRequest
            {
                InterfaceInfoId = interfaceInfoId,
                UserId = userId
            };
            return await _client.UpdateCountAsync(request);
        }
    }
}

```

In this example:

- The InvokeCountServiceClient class wraps the generated gRPC client code.
- It provides a method (UpdateCountAsync) to call the UpdateCount RPC method on the gRPC service.

By following these steps, you ensure that your projects are correctly set up to either provide or consume gRPC services, maintaining clear separation and modularity.