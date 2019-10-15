using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;

    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Animal> model = _db.Animals.ToList();
      return View(model);
    }

    [HttpPost]
    public ActionResult Index(string orderBy)
    {
      List<Animal> model = new List<Animal> {};
      if (orderBy == "Name")
      {
        model = _db.Animals.OrderBy(x => x.Name).ToList();
      }
      else if (orderBy == "Type")
      {
        model = _db.Animals.OrderBy(x => x.Type).ToList();
      }
      else if (orderBy == "Breed")
      {
        model = _db.Animals.OrderBy(x => x.Breed).ToList();
      }
      else
      {
        model = _db.Animals.ToList();
      }
      return View(model);
    }

    public ActionResult New()
    {
        return View();
    }

    [HttpPost("/animals/New")]
    public ActionResult Create(Animal animal)
    {
        _db.Animals.Add(animal);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        Animal thisAnimal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
        return View(thisAnimal);
    }
  }
}