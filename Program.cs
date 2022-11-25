using DotNetCoreDependencyInjectionLifetimes.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<ISingletonService, Session>();
services.AddScoped<IScopedService, Session>();
services.AddTransient<ITransientService, Session>();
services.AddTransient<SessionService, SessionService>();

// Add services to the container.
services.AddControllersWithViews();


var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();