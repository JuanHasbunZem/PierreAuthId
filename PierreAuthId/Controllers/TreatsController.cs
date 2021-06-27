using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PierreAuthId.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace PierreAuthId.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierreAuthIdContext _db;

    public TreatsController(PierreAuthIdContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treats> model = _db.Treats.ToList();
      return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treats Treats)
    {
      _db.Treats.Add(Treats);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreats = _db.Treats
          .Include(Treats => Treats.JoinEntities)
          .ThenInclude(join => join.Flavors)
          .FirstOrDefault(Treats => Treats.TreatsId == id);
      return View(thisTreats);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisTreats = _db.Treats.FirstOrDefault(Treats => Treats.TreatsId == id);
      ViewBag.FlavorsId = new SelectList(_db.Flavors, "FlavorsId", "Type");
      return View(thisTreats);
    }

    [HttpPost]
    public ActionResult Edit(Treats Treats)
    {
      _db.Entry(Treats).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisTreats = _db.Treats.FirstOrDefault(Treats => Treats.TreatsId == id);
      return View(thisTreats);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreats = _db.Treats.FirstOrDefault(Treats => Treats.TreatsId == id);
      _db.Treats.Remove(thisTreats);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}