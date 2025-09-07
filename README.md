## NotBot

NotBot is a lightweight and secure CAPTCHA generation and verification library for ASP.NET Core.  
It uses digital signatures (HMAC-SHA256) and client fingerprinting (IP + User-Agent) to ensure the CAPTCHA cannot be reused or tampered with.

---

## Features

- Generate image-based CAPTCHAs with customizable character length
- Validate CAPTCHAs with secure HMAC signatures
- Bind CAPTCHA to a specific client using fingerprinting (IP + User-Agent)
- Expiration time for each CAPTCHA
- Built-in middlewares for extracting CAPTCHA tokens and client fingerprints
- Works on both Linux and Windows

---

## Requirements

- Default fonts are: Arial, Verdana, Times New Roman.
- If you want to run your application on Linux, make sure Liberation Sans is installed first.

---

## Usage

### 1. Register the Service
```csharp
using NotBot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNotBot(options =>
{
    options.CharactersCount = 6; // Number of CAPTCHA characters
    options.CaptchaCodeExpirationSeconds = 120; // Expiration time in seconds
    options.SecretKey = "A_Strong_Key_For_HMAC"; // Secret key for signing
});
```

---

### 2. Add Middlewares
```csharp
var app = builder.Build();

app.UseClientSignatureExtractor();
app.UseCaptchaTokenExtractor();

app.MapControllers();

app.Run();
```

---

### 3. Generate a CAPTCHA
Call the `BuildCaptcha` method from your implementation of `INotBotService` to generate the CAPTCHA image and token.

You can expose it through your own API endpoint, or integrate it into an existing endpoint.  
For example, you might create an endpoint like `/captcha/build` that returns the image along with the token in the response headers.

---

### 4. Verify a CAPTCHA
```csharp
public class SampleService(INotBotService notBotService)

    public async Task<ResultData> DoSomething(RequestData request, CancellationToken cancellationToken = default)
    {

        ...
        ...
        ...

        if (!RequestScope.CaptchaToken.HasValue())
        {
            throw new CaptchaTokenIsRequiredException();
        }

        var isValid = notBotService.VerifyCaptcha(new VerifyCaptchaDto(request.Captcha, NotBotRequestScope.CaptchaToken));
        if (!isValid)
        {
            throw new InvalidCaptchaException();
        }

        ...
        ...
        ...
    }
}
```

---

## Best Practices

- Always use a **strong, random SecretKey** for signing.
- Serve the CAPTCHA image over HTTPS.
- Never expose the generated code to the client; only send the signed token.
