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

// Services
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ShipMethodService>();
builder.Services.AddScoped<CountryRegionsService>();
builder.Services.AddScoped<CreditCardService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ShiftService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ContactTypeService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<PhoneNumberTypeService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<SalesTerritoryService>();
builder.Services.AddScoped<VendorService>();
builder.Services.AddScoped<SalesOrderHeaderService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<SalesPersonService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<ShipMethodService>();
builder.Services.AddScoped<CurrencyRateService>();
builder.Services.AddScoped<SpecialOfferProductService>();
builder.Services.AddScoped<ProductDescriptionService>();
builder.Services.AddScoped<ShoppingCartItemService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductModelService>();
builder.Services.AddScoped<UnitMeasureService>();
builder.Services.AddScoped<ProductSubcategoryService>();
builder.Services.AddScoped<StateProvinceService>();

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
