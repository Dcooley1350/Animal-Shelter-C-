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
    public ActionResult Index(string orderBy, string gender)
    {
      List<Animal> model = new List<Animal> {};
      if (orderBy == "Name" && gender == "Male")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "male").OrderBy(x => x.Name).ToList();
      }
      else if (orderBy == "Type" && gender == "Male")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "male").OrderBy(x => x.Type).ToList();
      }
      else if (orderBy == "Breed" && gender == "Male")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "male").OrderBy(x => x.Breed).ToList();
      }
      if (orderBy == "Name" && gender == "Female")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "female").OrderBy(x => x.Name).ToList();
      }
      else if (orderBy == "Type" && gender == "Female")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "female").OrderBy(x => x.Type).ToList();
      }
      else if (orderBy == "Breed" && gender == "Female")
      {
        model = _db.Animals.Where(x => x.Gender.ToLower() == "female").OrderBy(x => x.Breed).ToList();
      }
      if (orderBy == "Name" && gender == "Both")
      {
        model = _db.Animals.OrderBy(x => x.Name).ToList();
      }
      else if (orderBy == "Type" && gender == "Both")
      {
        model = _db.Animals.OrderBy(x => x.Type).ToList();
      }
      else if (orderBy == "Breed" && gender == "Both")
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

    [HttpPost("/Animals/Details/{AnimalId}"), ActionName("Delete")]
    public ActionResult Destroy(int AnimalId)
    {
      var thisAnimal =_db.Animals.FirstOrDefault(animals => animals.AnimalId == AnimalId);
      
      _db.Animals.Remove(thisAnimal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}