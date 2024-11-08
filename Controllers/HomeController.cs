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
            ViewBag["Error"] = "incorrect username or password";
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
        // Console.WriteLine("Hola");
        // Response.Cookies.Delete("UserId");
        // Response.Cookies.Append("UserId", "12345");
        // Checkear si existe
        // string usuario = Request.Cookies["UserId"];

        // Console.WriteLine(usuario ?? "No hay usuario definido");
        if (TempData.ContainsKey("Error"))
            ViewData["Error"] = TempData["Error"].ToString();
        return View();
    }

    [HttpPost]
    public IActionResult GetSignup(string name, string mail, string password, string verifyPassword)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(verifyPassword))
        {
            Console.WriteLine("Hola");
            TempData["Error"] = "No se pasaron todos los parametros";
            return RedirectToAction("SignUp");
        }
        if (password == verifyPassword)
        {
            string hashedPassword = Functions.HashString(password);
            // Si esta todo bien
            Response.Cookies.Append("UserId", "12345");

            if (!BD.UserExist(name, mail, hashedPassword))
            {
                ViewBag.User = BD.SignUp(name, mail, hashedPassword);
                return RedirectToAction("indexLogged");
            }
            TempData["Error"] = "Username with that name or email already exists.";
            return RedirectToAction("SignUp");
        }
        TempData["Error"] = "Passwors do not match.";
        return RedirectToAction("SignUp");

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
