using BookStoreApp.Blazor.ServerUI.Services.Base;
using BookStoreApp.Blazor.ServerUI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.ServerUI.Services.Authentication;
using BookStoreApp.Blazor.ServerUI.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<WeatherForecastService>();

// NSwag configuration
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7045"));


//Authentication
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
    p.GetRequiredService<ApiAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Serve Swagger and ReDoc UI
    app.UseSwagger();
    app.UseReDoc();

    // Serve NSwag documents
    app.UseOpenApi(); // Same as app.UseSwagger()
    app.UseReDoc(); // Serve ReDoc UI
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
