using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class SalesController : Controller
    {
        StockDbEntities db = new StockDbEntities();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewSales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewSales(TblSales t)
        {
            db.TblSales.Add(t);
            db.SaveChanges();
            return View("Index");
        }
    }
}