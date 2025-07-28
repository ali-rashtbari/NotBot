# 🧠 NotBot - Captcha Generator API for ASP.NET Core

A minimal yet secure open-source **Captcha generator service** built with ASP.NET Core and SixLabors.ImageSharp.
NotBot is designed to prevent bots from abusing your API endpoints using simple token-based image CAPTCHAs.

## 🚀 Features

* ⚡ Generate captcha images with random secure codes.
* 🧾 Token-based verification with client fingerprinting.
* 🧠 Customizable options like code length, expiry, and secret key.
* 🛠 Easily pluggable into any ASP.NET Core project.
* 🌐 Works cross-platform (Linux, Windows).

---

## 📸 Example

**GET** `/notbot/build`
Returns a JPEG captcha image and a `token` in the response headers.

**GET** `/notbot/verify?code=ABC123&token=...`
Verifies the code and token.

---

## 🔧 Configuration

Use the following options in your `appsettings.json` to configure NotBot:

```json
{
  "NotBotOptions": {
    "CharactersCount": 6,
    "CaptchaCodeExpirationSeconds": 120,
    "SecretKey": "YOUR_SECRET_KEY"
  }
}
```

**🔡️ Make sure `SecretKey` is a strong, random string. It's used to sign and validate tokens.**

---

## 🛠️ Usage

### 1. Install the Package (If Available via NuGet)

```bash
dotnet add package NotBot
```

Or clone the repo and reference it directly in your solution.

### 2. Register the service in `Program.cs` or `Startup.cs`:

```csharp
builder.Services.AddNotBot(options =>
{
    options.CharactersCount = 6;
    options.CaptchaCodeExpirationSeconds = 120;
    options.SecretKey = "your-strong-secret-key";
});
```

### 3. Use the Captcha Endpoints

You can use the built-in endpoints via the included controller:

```csharp
[ApiController]
[Route("notbot")]
public class NotBotEndpoints : ControllerBase
{
    ...
}
```

Or inject `INotBotService` and implement your own logic.

---

## 🧪 Sample Client Request

```http
GET /notbot/build
```

* 📤 Response Header: `token: xxxxxxxx`
* 📸 Response Body: JPEG captcha image

```http
GET /notbot/verify?code=ABC123&token=xxxxxxxx
```

* ✅ Returns `true` or `false`

---

## 🔐 How It Works

1. `BuildCaptcha`:

   * Generates a secure random code (non-ambiguous characters only)
   * Signs a token using HMAC SHA256 with:

     * code
     * expiry
     * fingerprint
   * Generates an image from the code

2. `VerifyCaptcha`:

   * Validates the token signature
   * Confirms fingerprint and expiry
   * Compares user input with the original code

---

## 🖥️ Linux Compatibility

The captcha engine uses **Liberation Sans** font automatically when running on Linux OS.

---

## 📦 Dependencies

* [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp)
* [SixLaborsCaptcha](https://github.com/SixLabors/SixLaborsCaptcha)
* Built-in .NET:

  * `HMACSHA256`
  * `RandomNumberGenerator`

---

## 🧑‍💻 Contributing

Pull requests are welcome.
If you find a bug or have ideas for improvements, feel free to open an issue.

---

## 📄 License

This project is licensed under the MIT License.

---

## 💬 Feedback

If you find this project useful or have suggestions, feel free to star ⭐ the repo and reach out!

---

Made with ❤️ by [Ali (AAA)](https://github.com/ali-rashtbari)
