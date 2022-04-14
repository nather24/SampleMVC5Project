using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;
namespace EFDbFirstApproachExample.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string search="")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            List<Product> products = db.Products.Where(temp=>temp.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
            //SqlParameter[] sqlParameters = new SqlParameter[]
            //{
            //    new SqlParameter("@BrandID", 2)
            //    //you can add more parameters here
            //};
            //List<Product> products = db.Database.SqlQuery<Product>("exec getProductsByBrandID @BrandID", sqlParameters).ToList();

            return View(products);
        }
        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product products = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(products);
        }
        public ActionResult Create()
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.ct = db.Categories.ToList();
            ViewBag.br = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index", "Products");
        }
        public ActionResult Edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(tmp => tmp.ProductID == id).FirstOrDefault();
            ViewBag.ct = db.Categories.ToList();
            ViewBag.br = db.Brands.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(tmp => tmp.ProductID == p.ProductID).FirstOrDefault();
            product.ProductName = p.ProductName;
            product.Price = p.Price;
            product.DateOfPurchase = p.DateOfPurchase;
            product.AvailabilityStatus = p.AvailabilityStatus;
            product.CategoryID = p.CategoryID;
            product.BrandID = p.BrandID;
            product.Active = p.Active;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(tmp => tmp.ProductID == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(tmp => tmp.ProductID == p.ProductID).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }


    
}