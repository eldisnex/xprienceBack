using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Xprience.Models;
using Microsoft.Extensions.Logging;

namespace Xprience.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // ---- Directorios ----
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login(string nameMail)
    {
        return View();
    }

    public IaActionResult GetLogin(string nameMail, string password)
    {
        ViewBag.User = BD.LogIn(nameMail, password);
        if(ViewBag.User == null)
        {
            ViewBag.Error = "incorrect username or password";
            return RedirecToAction ("Login");
        }
        else
        {
            return RedirecToAction ("indexLogged");
        }
    }
    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult GetSignup(string name, string mail, string password, string verifyPassword)
    {
        if(password == verifyPassword)
        {
            ViewBag.User = BD.SingUp(name, mail, password);
            return RedirecToAction ("indexLogged");
        }
        else
        {
            ViewBag.Error = "invalid password";
            return RedirecToAction ("Signup");
        }

    }

    public IActionResult indexLogged()
    {
        return View();
    }

    public IActionResult Inspiration()
    {
        return View();
    }

    public IActionResult Explore()
    {
        return View();
    }

    public IActionResult Calendar()
    {
        return View();
    }

    public IActionResult CreatePlan()
    {
        return View();
    }

    public IActionResult ReceivePlan()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
