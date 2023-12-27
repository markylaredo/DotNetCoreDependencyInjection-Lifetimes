using DotNetCoreDependencyInjectionLifetimes.Interfaces;
using DotNetCoreDependencyInjectionLifetimes.Services;
using Quartz;
using Quartz.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddQuartz(q =>
{

    // Create a "key" for the job
    var jobKey = new JobKey("HelloWorldJob");

    // Register the job with the DI container
    q.UseSimpleTypeLoader();
    q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));
                
    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey) // link to the HelloWorldJob
        .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
        .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

});

// ASP.NET Core hosting
services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

services.AddSingleton<ISingletonService, Session>();
services.AddScoped<IScopedService, Session>();
services.AddTransient<ITransientService, Session>();
services.AddTransient<SessionService, SessionService>();

// Add services to the container.
services.AddSingleton<UptimeService>();
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



[DisallowConcurrentExecution]
public class HelloWorldJob : IJob
{
    private readonly ILogger<HelloWorldJob> _logger;
    public HelloWorldJob(ILogger<HelloWorldJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Hello world!");
        return Task.CompletedTask;
    }
}