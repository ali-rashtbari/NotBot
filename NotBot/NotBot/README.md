باشه، این رو به صورت یک فایل **README.md** کامل برات می‌نویسم تا مستقیم ذخیره کنی و توی پروژه بذاری.  

---

**README.md**  
```markdown
# NotBot

**NotBot** is a lightweight and secure CAPTCHA generation and verification library for ASP.NET Core.  
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

## Installation

```bash
dotnet add package NotBot
```

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
```csharp
[HttpGet("build")]
public IActionResult Build([FromServices] INotBotService notBotService)
{
    var result = notBotService.BuildCaptcha();
    Response.Headers["Captcha-Token"] = result.Token;
    return File(result.ImageArray, "image/jpeg");
}
```

---

### 4. Verify a CAPTCHA
```csharp
[HttpPost("verify")]
public IActionResult Verify(
    [FromServices] INotBotService notBotService,
    [FromBody] VerifyCaptchaDto dto)
{
    bool isValid = notBotService.VerifyCaptcha(dto);
    return Ok(new { valid = isValid });
}
```

---

## Best Practices

- Always use a **strong, random SecretKey** for signing.
- Serve the CAPTCHA image over HTTPS.
- Never expose the generated code to the client; only send the signed token.

---

## License

MIT License. See the `LICENSE.txt` file for more details.
```

---