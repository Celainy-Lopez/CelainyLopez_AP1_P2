using BlazorBootstrap;
using CelainyLopez_AP1_P2.Components;
using CelainyLopez_AP1_P2.DAL;
using CelainyLopez_AP1_P2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Inyeccion del contexto
var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Context>(c => c.UseSqlServer("Name=SqlConStr"));
builder.Services.AddScoped<CombosService>();
builder.Services.AddScoped<ArticulosService>();
builder.Services.AddSingleton<ToastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
