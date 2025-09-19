using Gateway.Controllers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<RequestResponseLoggingFilter>();
});
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Serilog ve Seq entegrasyonu
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
    
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // Tüm istekleri logla
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapReverseProxy();

app.Run();
