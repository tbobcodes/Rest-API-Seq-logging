using Microsoft.EntityFrameworkCore;
using Serilog;
using BookStoreApp.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreAppDBConnection");
builder.Services.AddDbContext<BookAPIContext>(options => options.UseSqlServer(connString));


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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
