using Microsoft.AspNetCore.Mvc;

namespace PierreAuthId.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}