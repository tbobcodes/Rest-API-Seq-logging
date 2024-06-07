using Microsoft.EntityFrameworkCore;
using Serilog;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreDbAppConnection");
builder.Services.AddDbContext<BookAPIContext>(options => options.UseSqlServer(connString));

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookAPIContext>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
    lc.WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day);
    lc.WriteTo.Seq("http://localhost:5341");

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin());
});

//auto mapper config
builder.Services.AddAutoMapper(typeof(mapperConfig));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Corrected
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])) // Corrected
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var identity = context.Principal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                // Log the claims for debugging
                Console.WriteLine("Claims Identity:");
                foreach (var claim in identity.Claims)
                {
                    Console.WriteLine($"- {claim.Type}: {claim.Value}");
                }

                // Remove the existing "Name" claim
                var nameClaim = identity.FindFirst(ClaimTypes.Name);
                if (nameClaim != null)
                {
                    identity.RemoveClaim(nameClaim);
                }

                // Map the "sub" claim to the "Name" claim type
                var subClaim = identity.FindFirst("sub");
                if (subClaim != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, subClaim.Value));
                }
            }
        }
    };
});

//NSwagg configuration
builder.Services.AddOpenApiDocument(document =>
{
    document.DocumentName = "a";
    document.Title = "First API Documentation";
    // Additional configuration if needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Serve Swagger and ReDoc UI
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc();

    // Serve NSwag documents
    app.UseOpenApi(); // Same as app.UseSwagger()
    app.UseReDoc(); // Serve ReDoc UI
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
