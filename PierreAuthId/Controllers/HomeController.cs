using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using PierreAuthId.Models;

namespace PierreAuthId.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierreAuthIdContext _db;
    public HomeController(PierreAuthIdContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.TreatList = _db.Treats.ToList();
      ViewBag.FlavorList = _db.Flavors.ToList();
      return View();
    }
  }
}