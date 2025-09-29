using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Dapper;
using Microsoft.OpenApi.Models;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var connctionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterShopDependency(connctionString);
CommonBootstrapper.Init(builder.Services);

builder.Services.AddTransient<IFileService, FileService>();

SqlMapper.AddTypeHandler(new TomanTypeHandler());


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Sample ASP.NET Core Web API with Swagger"
    });

    // (اختیاری) پشتیبانی از JWT در Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "توکن را به صورت: Bearer {token} وارد کنید"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{ Reference = new OpenApiReference
                { Type = ReferenceType.SecurityScheme, Id = "Bearer" }}, new string[]{}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
