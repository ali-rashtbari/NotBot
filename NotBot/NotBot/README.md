# 🚀 NotBot – Version 1.0.4

## ✨ New Features

### 🔤 1. Custom Allowed Characters for CAPTCHA Generation

Version **1.0.4** introduces support for customizing the set of characters used when generating CAPTCHA codes.

You can now define your own character set via `NotBotOptions`:

```csharp
builder.Services.AddNotBot(options =>
{
    options.AllowedCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
});

You are free to use any combination of characters, such as:

Numbers only: 0123456789

Letters only: ABCDEFGHIJKLMNOPQRSTUVWXYZ

Mixed custom sets

Unicode characters (as long as the selected font supports them)

This feature provides full flexibility and allows you to tailor the CAPTCHA format to your project's specific requirements.

2. Combined Middleware Extension (UseNotBot)

In this version, the two middlewares:

ClientSignatureExtractor

CaptchaTokenExtractor

have been merged into a single extension method for easier configuration.

Instead of registering both middlewares individually, you can now simply use:

app.UseNotBot();

This simplifies the setup and ensures both middlewares are always applied in the correct order.


Summary

Version 1.0.4 delivers:

Customizable CAPTCHA character sets

A unified middleware extension for cleaner and more intuitive configuration