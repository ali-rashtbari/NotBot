using NotBot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // برای Swagger نیاز هست
builder.Services.AddSwaggerGen();           // اضافه کردن سرویس Swagger

builder.Services.AddNotBot(options =>
{
    options.CharactersCount = 6;
    options.SecretKey = "087mr45)*fp!kxQruk?><=9yNLXId$#w";
    options.CaptchaCodeExpirationSeconds = 120;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
