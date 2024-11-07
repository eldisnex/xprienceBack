using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Xprience.Models;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

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
        ViewBag.logged = false;
        return View();
    }

    [Route("/Login")]
    [Route("/Home/Login")]
    public IActionResult Login()
    {
        ViewBag.logged = false;
        return View();
    }

    public IActionResult GetLogin(string nameMail, string password)
    {
        ViewBag.User = BD.LogIn(nameMail, password);
        if (ViewBag.User == null)
        {
            ViewBag.Error = "incorrect username or password";
            return RedirectToAction("Login");
        }
        else
        {
            return RedirectToAction("indexLogged");
        }
    }

    [Route("/SignUp")]
    [Route("/Home/SignUp")]
    public IActionResult SignUp()
    {
        // Response.Cookies.Delete("UserId");
        // Response.Cookies.Append("UserId", "12345");
        // Checkear si existe
        // string usuario = Request.Cookies["UserId"];

        // Console.WriteLine(usuario ?? "No hay usuario definido");
        return View();
    }

    [HttpPost]
    public IActionResult GetSignup(string name, string mail, string password, string verifyPassword)
    {
        if (password == verifyPassword)
        {
            string hashedPassword = Functions.HashString(password);
            // Si esta todo bien
            Response.Cookies.Append("UserId", "12345");

            // if(!BD.UserExist(name, mail, password)){
            //     ViewBag.User = BD.SingUp(name, mail, password);
            //     return RedirectToAction("indexLogged");
            // }
            // else{
            //     ViewBag.Error = "invalid password";
            //     return RedirectToAction("Signup");
            // }

        }
        else
        {
            ViewBag.Error = "invalid password";
        }
        return RedirectToAction("Signup");
    }

    public IActionResult indexLogged()
    {
        return View();
    }

    public IActionResult Inspiration()
    {
        ViewBag.Plans = BD.ListPlan();
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

    public IActionResult Create()
    {
        Api a = new Api();
        a.getImage("51d9202a498e44076324c5fc");
        ViewBag.logged = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> HandleCreate(string latitude, string longitude, string query)
    {
        Api api = new Api();
        var r = await api.makeRequest(latitude, longitude, query);
        return Json(r);
    }

    [HttpPost]
    public async Task<IActionResult> HandleImage(string id)
    {
        Api api = new Api();
        var r = await api.getImage(id);
        return Json(r);
    }

    public IActionResult CreatePlanGastronomy()
    {
        ViewBag.ButtonEnabled = true;
        int i = 0;
        if (i == 1)
        {
            ViewBag.ButtonEnabled = false;
        }
        i++;
        return View();
    }

    public IActionResult CreatePlanEntrtainment()
    {
        ViewBag.ButtonEnabled = true;
        int i = 0;
        if (i == 1)
        {
            ViewBag.ButtonEnabled = false;
        }
        i++;
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
