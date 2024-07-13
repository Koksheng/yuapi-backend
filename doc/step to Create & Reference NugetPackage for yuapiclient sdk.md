https://www.youtube.com/watch?v=jU33w5xxYH0&list=LL&index=3&t=915s

1. Create an account on https://www.nuget.org/.
2. Create NuGet Gallery API Keys.
3. In the yuapi-client-sdk library, fill in the NuGet Package Details by right-clicking the Library > Properties.
   
   ![image](https://github.com/user-attachments/assets/e091f053-ff7e-48d6-9a24-d0f3b9dbf991)

4. Pack a Release version.
   
   ![image](https://github.com/user-attachments/assets/f969afc6-fb35-49b3-8406-9aaa091a9c57)

5. It will generate a `.nupkg` file.
   
   ![image](https://github.com/user-attachments/assets/aa6d8775-fc42-49cd-88cb-34ac39bcf52d)

6. Execute the command below:
   ```
   dotnet nuget push YourPackageName.nupkg -k YourApiKey -s https://api.nuget.org/v3/index.json
   ```
   
   ![image](https://github.com/user-attachments/assets/5d581eed-348b-4ede-bcd1-4eca985849de)



7. Replace the blur part with your actual API Key below:
    
   ![image](https://github.com/user-attachments/assets/d39c536f-30c9-4726-9937-e058326117b2)

8. You can find the published package on NuGet.org.
    
    ![image](https://github.com/user-attachments/assets/740c9dfa-88fd-4fc0-a07a-70861a3afe61)

# Steps to Reference yuapi-client-sdk #
1. Create a console app.
2. In the NuGet manager, search for `yuapi-client-sdk` and install it.

   ![image](https://github.com/user-attachments/assets/8fa6d48c-ba7c-4241-b2fc-c0b2cb2b2a0c)

3. Code in `Program.cs`
   ```
    using yuapi_client_sdkyuapi_client_sdk.Client;

    var httpClient = new HttpClient { BaseAddress = new Uri("http://127.0.0.1:8090") };
    var yuApiClient = new YuApiClient(httpClient);
    
    yuApiClient.SetAccessKey("9d8ebf4636e2dec32117b9d72e1a3d70");
    yuApiClient.SetSecretKey("296791011ea2292bc7efc73deeaddc45");
    
    try
    {
        var result = await yuApiClient.InvokeAsync("GetUsernameByPost", "{\"username\":\"exampleUser\"}");
        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.WriteLine("Hello, World!");
   ```

   ![image](https://github.com/user-attachments/assets/0614cc0c-953b-40a4-b480-3fe9cfd7da8e)

4. Make sure both projects are running, then can call each other.
   ![image](https://github.com/user-attachments/assets/c256b1dc-bb7e-4f6a-9ef6-30201e0450b4)

5. End Result.
   ![image](https://github.com/user-attachments/assets/d6354ae2-1afc-4e24-8b5b-6670a9f9054c)
