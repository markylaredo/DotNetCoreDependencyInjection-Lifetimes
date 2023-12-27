using System.Diagnostics;
using DotNetCoreDependencyInjectionLifetimes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreDependencyInjectionLifetimes.Models;
using DotNetCoreDependencyInjectionLifetimes.Services;

namespace DotNetCoreDependencyInjectionLifetimes.Controllers;

public sealed class HomeController : Controller
{
    private readonly IScopedService _serviceScoped;
    private readonly ITransientService _transientService;
    private readonly ISingletonService _singletonService;
    private readonly SessionService _sessionService;
    private readonly UptimeService _uptimeService;

    public HomeController(IScopedService serviceScoped,
        ITransientService transientService,
        ISingletonService singletonService,
        SessionService sessionService, UptimeService uptimeService)
    {
        _serviceScoped = serviceScoped;
        _transientService = transientService;
        _singletonService = singletonService;
        _sessionService = sessionService;
        _uptimeService = uptimeService;
    }

    public IActionResult Index()
    {
        ViewBag.Singleton = _singletonService;
        ViewBag.Transient = _transientService;
        ViewBag.Scope = _serviceScoped;
        ViewBag.SessionService = _sessionService;
        var uptime = _uptimeService.GetUptime();
        ViewBag.UpTime = uptime;
        var model = new LifeTimeService();
        model.Singleton = _singletonService.GetSessionId();
        model.Transient = _transientService.GetSessionId();
        model.Scope = _serviceScoped.GetSessionId();
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public sealed class LifeTimeService
{
    public Guid Singleton { get; set; }
    public Guid Transient { get; set; }
    public Guid Scope { get; set; }
}