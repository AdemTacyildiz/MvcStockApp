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
    public class ProductController : Controller
    {
        StockDbEntities db = new StockDbEntities();
        // GET: Product
        public ActionResult Index(int page = 1)
        {
            var values = db.TblProducts.ToList().ToPagedList(page, 9);
            return View(values);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> values = (from i in db.TblCategories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(TblProducts p1)
        {
            var category = db.TblCategories.Where(m => m.CategoryID == p1.TblCategories.CategoryID).FirstOrDefault();
            p1.TblCategories = category;
            db.TblProducts.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index") ;
        }
        public ActionResult DeleteProduct(int id)
        {
            var product = db.TblProducts.Find(id);
            db.TblProducts.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult BringProduct(int id)
        {
            var product = db.TblProducts.Find(id);
            List<SelectListItem> values = (from i in db.TblCategories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;
            return View("BringProduct",product);
        }
        public ActionResult UpdateProduct(TblProducts p1)
        {
            var product = db.TblProducts.Find(p1.ProductID);
            product.ProductName = p1.ProductName;
            product.ProductBrand = p1.ProductBrand;
            product.ProductPrice = p1.ProductPrice;
            product.ProductStock = p1.ProductStock;
            //product.ProductCategory = p1.ProductCategory;
            var ctgry=db.TblCategories.Where(c => c.CategoryID == p1.TblCategories.CategoryID).FirstOrDefault();
            product.ProductCategory = ctgry.CategoryID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}