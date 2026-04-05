using AdventureWorksDominicana.Blazor.Components;
using AdventureWorksDominicana.Data.Context;
using AdventureWorksDominicana.Services;
using Blazored.Toast;
using Microsoft.EntityFrameworkCore;
using AdventureWorksDominicana.Data.Models;
using Microsoft.AspNetCore.Identity;
using AdventureWorksDominicana.Blazor.Components.Account;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContextFactory<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

builder.Services.AddDbContext<SecurityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));


builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

builder.Services.AddIdentityCore<AspNetUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<SecurityContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<AspNetUser>, IdentityNoOpEmailSender>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();


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
builder.Services.AddScoped<CultureService>();
builder.Services.AddScoped<LocationService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();