using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierreAuthId.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierreAuthId.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierreAuthIdContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierreAuthIdContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userFlavors);
    }

    public ActionResult Create()
    {
      ViewBag.TreatsId = new SelectList(_db.Treats, "TreatsId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavors Flavors, int TreatsId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Flavors.User = currentUser;
      _db.Flavors.Add(Flavors);
      _db.SaveChanges();
      if (TreatsId != 0)
      {
          _db.TreatsFlavors.Add(new TreatsFlavors() { TreatsId = TreatsId, FlavorsId = Flavors.FlavorsId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFlavors = _db.Flavors
          .Include(Flavors => Flavors.JoinEntities)
          .ThenInclude(join => join.Treats)
          .FirstOrDefault(Flavors => Flavors.FlavorsId == id);
      return View(thisFlavors);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavors = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorsId == id);
      ViewBag.TreatsId = new SelectList(_db.Treats, "TreatsId", "Name");
      return View(thisFlavors);
    }

    [HttpPost]
    public ActionResult Edit(Flavors Flavors, int TreatsId)
    {
      if (TreatsId != 0)
      {
        _db.TreatsFlavors.Add(new TreatsFlavors() { TreatsId = TreatsId, FlavorsId = Flavors.FlavorsId });
      }
      _db.Entry(Flavors).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      var thisFlavors = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorsId == id);
      ViewBag.TreatsId = new SelectList(_db.Treats, "TreatsId", "Name");
      return View(thisFlavors);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavors flavors, int treatsId)
    {
      if (treatsId != 0)
      {
      _db.TreatsFlavors.Add(new TreatsFlavors() { TreatsId = treatsId, FlavorsId = flavors.FlavorsId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlavors = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorsId == id);
      return View(thisFlavors);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavors = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorsId == id);
      _db.Flavors.Remove(thisFlavors);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTreats(int joinId)
    {
      var joinEntry = _db.TreatsFlavors.FirstOrDefault(entry => entry.TreatsFlavorsId == joinId);
      _db.TreatsFlavors.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}