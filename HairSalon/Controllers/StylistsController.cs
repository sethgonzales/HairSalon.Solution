using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db; 

    public ClientsController(HairSalonContext db) 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Clients";
      List<Item> model = _db.Clients.Include(item => item.Category).ToList(); 
      return View(model);
    }

    public ActionResult Create() 
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");  
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item) 
    {
      ViewBag.PageTitle = "Create New Clients";
      if (item.CategoryId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Clients.Add(item);  
      _db.SaveChanges();
      return RedirectToAction("Index"); 
    }

    public ActionResult Details(int id)
    {
      ViewBag.PageTitle = "Item Details";
      Item thisItem = _db.Clients.Include(item => item.Category).FirstOrDefault(item => item.ItemId == id); 
      return View(thisItem);
    }

    public ActionResult Edit(int id) 
    {
      ViewBag.PageTitle = "Edit Item Details";
      Item thisItem = _db.Clients.FirstOrDefault(item => item.ItemId == id); 
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item) 
    {
      _db.Clients.Update(item); 
      return RedirectToAction("Index"); 
    }

    public ActionResult Delete(int id)
    {
      ViewBag.PageTitle = "Delete Clients";
      Item thisItem = _db.Clients.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")] /
    public ActionResult DeleteConfirmed(int id) 
    {
      Item thisItem = _db.Clients.FirstOrDefault(item => item.ItemId == id); 
      _db.Clients.Remove(thisItem); 
      _db.SaveChanges(); 
      return RedirectToAction("Index"); 
    }
  }
}



