using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Snow_simulation.Model;
using Snow_simulation.Model.Physic;
using SnowInBrowser.Models;

namespace SnowInBrowser.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public List<SnowFlake> GetSnow()
    {
        Converter json = new Converter();
        return json.Flakes;
    }
}