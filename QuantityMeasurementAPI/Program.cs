using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;

using BusinessLayer.Services;
using RepositoryLayer.Interface;
using RepositoryLayer.Repository;
using QuantityMeasurementAPI.Middleware;

// IMPORTANT: FORCE REPOSITORY DB CONTEXT
using RepoDbContext = RepositoryLayer.Context.AppDbContext;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// =====================
// SERILOG
// =====================
builder.Host.UseSerilog();

// =====================
// CONTROLLERS
// =====================
builder.Services.AddControllers();

// =====================
// SWAGGER
// =====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Quantity Measurement API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token like: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =====================
// CORS
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// =====================
// DATABASE
// =====================
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["ConnectionStrings__DefaultConnection"]
    ?? builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration["DefaultConnection"];

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new Exception("Database connection string not found.");
}

Console.WriteLine($"DB CONNECTION STRING: {connectionString}");

builder.Services.AddDbContext<RepoDbContext>(options =>
    options.UseNpgsql(connectionString));

// // FORCE SAME CONTEXT FOR DI
// builder.Services.AddScoped<IAppDbContext>(provider =>
//     provider.GetRequiredService<RepoDbContext>());

// =====================
// REPOSITORY + SERVICES
// =====================
builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

// =====================
// VALIDATION
// =====================
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// =====================
// JWT
// =====================
var key = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(key))
{
    throw new Exception("JWT key missing.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// =====================
// DB INIT
// =====================
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<RepoDbContext>();
//     db.Database.Migrate();
// }

// =====================
// RAILWAY PORT
// =====================
var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://0.0.0.0:{port}");
}

// =====================
// MIDDLEWARE
// =====================
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();