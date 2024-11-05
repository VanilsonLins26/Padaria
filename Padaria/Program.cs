using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Padaria.Models.Enums;
using Padaria.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Padaria.Services;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PadariaContext>(options =>
    options.UseMySql(builder
    .Configuration
    .GetConnectionString("PadariaContext"),
    ServerVersion.AutoDetect(builder
    .Configuration
    .GetConnectionString("PadariaContext"))));



var supportedCultures = new[]
{
    new CultureInfo("pt-BR"),
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProdutoContaService>();

var app = builder.Build();

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Venda}/{action=Index}/{id?}");

app.Run();
