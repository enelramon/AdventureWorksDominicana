using AdventureWorksDominicana.Blazor.Components;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Services;
using Blazored.Toast;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ShipMethodService>();
builder.Services.AddScoped<CountryRegionsService>();

builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ShiftService>();
builder.Services.AddScoped<EmployeeService>(); 
    builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddBlazorBootstrap();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
