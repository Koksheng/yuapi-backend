# Yu Api Project - Backend


[Frontend at here](https://github.com/Koksheng/yuapi-frontend)

## JWT Notes

[Video](https://www.youtube.com/watch?v=7ILCRfPmQxQ&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=9)

1. When the user successfully logs in, generate a JWT token and pass it to the frontend.
   
   `yuapi.Application` -> `UserService.cs` -> `UserLogin()`
   
   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/8da851d9-e68d-493f-8c6c-2348efe0946f)

   `yuapi.Infrastructure` -> `JwtTokenGenerator.cs` -> `GenerateToken()`

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/08d40bab-2a2a-46b1-9015-ebd8879e37fe)
   
   `yuapi.Infrastructure` -> `DependencyInjection.cs` -> `AddAuth()` -> Token Validation

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/04c78701-5b55-44e4-a2f2-b9fcd636dd6a)


   `yuapi.Api` -> `Program.cs`

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/f6674d65-50e9-48f1-abcb-1f61cd12a9be)

   `yuapi.Api` -> `Controller.cs` -> add `[Authorize]`

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/680dff46-fc9a-4480-bac3-5a7185ed1f95)


3. Frontend, store the token in localStorage.

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/a5cf2285-33eb-4d47-ad3f-ca1322382b5b)

   `globalRequest.ts` -> `request.interceptors (请求拦截器)` -> Add the token to the request interceptor to include the token in the request header.

   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/c397cb30-b4ec-48dd-b378-0d248317b432)


4. Backend, verify the token for each request.
   ![image](https://github.com/Koksheng/yuapi-backend/assets/33799735/76965d82-636e-48fa-baeb-a97abe3d2822)
