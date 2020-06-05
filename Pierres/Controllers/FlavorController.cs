using Microsoft.AspNetCore.Mvc;
using Pierres.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
//identity \/
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

namespace Pierres.Controllers
{
  // [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierresContext _db;
    // private readonly UserManager<ApplicationUser> _userManager; //identity

    public FlavorsController(PierresContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.Include(flavors => flavors.Treat).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Type");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
          .Include(flavor => flavor.Treats)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    
   public ActionResult Search( string type)
    {
      var thisFlavor = _db.Flavors

          .Include(flavor => flavor.Treats)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(falvor => flavor.Type == type);
      return View(thisFlavor);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Type");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Type");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}