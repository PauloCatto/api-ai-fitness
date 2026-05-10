using System.Text;
using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//
// =========================
// CONFIGURAÇÕES
// =========================
//

// FRONTEND URL
// Local: http://localhost:4200
// Produção: URL da Vercel depois
var frontendUrl =
    builder.Configuration["FRONTEND_URL"]
    ?? "http://localhost:4200";

//
// =========================
// CORS
// =========================
//

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(
                frontendUrl,
                "http://localhost:4200"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//
// =========================
// CONTROLLERS + OPENAPI
// =========================
//

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//
// =========================
// DATABASE (POSTGRES / NEON)
// =========================
//

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//
// =========================
// JWT
// =========================
//

// Variáveis JWT
var jwtKey =
    builder.Configuration["Jwt:Key"]
    ?? throw new Exception("JWT KEY não configurada.");

var jwtIssuer =
    builder.Configuration["Jwt:Issuer"]
    ?? throw new Exception("JWT ISSUER não configurado.");

var jwtAudience =
    builder.Configuration["Jwt:Audience"]
    ?? throw new Exception("JWT AUDIENCE não configurado.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)
                    )
            };
    });

builder.Services.AddAuthorization();

//
// =========================
// SERVICES
// =========================
//

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

//
// =========================
// OPEN API / SWAGGER
// =========================
//

// Deixa disponível tanto local quanto produção
app.MapOpenApi();

//
// =========================
// PIPELINE
// =========================
//

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();