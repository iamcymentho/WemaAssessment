using CustomerOnboarding.Services;
using CustomerOnboarding.Services.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WemaBank.CustomerOnboarding", Version = "v1" });

    }
);
builder.AddConfiguration();
builder.Services.AddMemoryCache();
builder.Services.AddDataDependencies(builder.Configuration);
builder.Services.AddServiceDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers to the container
builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("corsapp");

// Map controllers to the request pipeline
app.MapControllers();

app.Run();
