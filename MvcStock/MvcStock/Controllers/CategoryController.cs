using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        StockDbEntities db = new StockDbEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var values = db.TblCategories.ToList();
            var values = db.TblCategories.ToList().ToPagedList(sayfa, 4);
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(TblCategories p1)
        {
            if(!ModelState.IsValid)
            {
                return View("AddCategory");
            }
            db.TblCategories.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = db.TblCategories.Find(id);
            db.TblCategories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult BringCategory(int id)
        {
            var category = db.TblCategories.Find(id);
            return View("BringCategory",category);
        }
        public ActionResult UpdateCategory(TblCategories p1)
        {
            var category = db.TblCategories.Find(p1.CategoryID);
            category.CategoryName = p1.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}