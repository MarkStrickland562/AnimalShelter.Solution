using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    [HttpGet("/Animals")]
    public ActionResult Index2()
    {
      List<Animal> allAnimals = Animal.GetAll("default");
      return View("index", allAnimals);
    }

    [HttpGet("/Animals/{sortField}")]
    public ActionResult Index(string sortField)
    {
      List<Animal> allAnimals = Animal.GetAll(sortField);
      return View(allAnimals);
    }
  }
}
