-yuapi-backend.sln
	-src
		-yuapi.Api
		-yuapi.Application
		-yuapi.Contracts
		-yuapi.Domain
		-yuapi.Infrastructure

	-yuapi.RPC.InvokeCountService (new created)

	-yuapi-interface
		-yuapi-interface.sln
		-yuapi-client-sdk
		-yuapi-interface
		-yuapi-interfce-console-app

	-yuapi-OcelotGateway (new created)

yuapi-interface web api (already have)
- url is http://localhost:8123
- NameController - GetUsernameByPost which can access by http://localhost:8123/api/name/user

(todo)
in yuapi-OcelotGateway web api empty net core 8.0 
- successfully redirect the UpstreamPathTemplate to DownstreamPathTemplate
- question: can I apply the log, access control white in Ocelot Gateway

1. request log
2. access control, white list
3. user verify (accessKey, secretKey)
4. check the request interface is exist
5. redirect the request to their request interface, eg: http://localhost:8090/api/name/user to http://localhost:8123/api/name/user
6. handle response by invoke invokeCountService


yuapi.Api
http://localhost:5218/

yuapi-interface
http://localhost:8123/

yuapi-OcelotGateway 
http://localhost:8090/
http://127.0.0.1:8090/api/Name?name=yupi


By organizing your code this way, you ensure that each layer has a specific responsibility, making your application easier to maintain, extend, and test. 

The controllers handle HTTP requests and responses, the application layer contains the business logic, the domain layer defines the core business entities and rules, and the infrastructure layer handles data persistence.