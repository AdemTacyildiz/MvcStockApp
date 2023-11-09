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
    public class CustomerController : Controller
    {
        StockDbEntities db = new StockDbEntities();
        // GET: Customer
        public ActionResult Index(string p)
        {
            var values = from d in db.TblCustomers select d;
            if(!string.IsNullOrEmpty(p))
            {
                values = values.Where(m=> m.CustomerName.Contains(p));
            }

            //var values = db.TblCustomers.ToList();
            //var values = db.TblCustomers.ToList().ToPagedList(1,9);
            return View(values.ToList());
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(TblCustomers c1)
        {
            if (!ModelState.IsValid)
            { 
                return View("AddCustomer");
            }

            db.TblCustomers.Add(c1);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.TblCustomers.Find(id);
            db.TblCustomers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringCustomer(int id)
        {
            var customer = db.TblCustomers.Find(id);
            return View("BringCustomer", customer);
        }
        public ActionResult UpdateCustomer(TblCustomers p1)
        {
            var customer = db.TblCustomers.Find(p1.CustomerID);
            customer.CustomerID = p1.CustomerID;
            customer.CustomerName = p1.CustomerName;
            customer.CustomerSurName = p1.CustomerSurName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}