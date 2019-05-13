using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspMvc.Models;

namespace AspMvc.Controllers
{
    public class ManufacturersController : Controller
    {
        private IRepository db;

        public ManufacturersController()
        {
            db = null;
        }

        public ManufacturersController(IRepository context)
        {
            db = context;
        }

        // GET: Manufacturers
        [Authorize(Roles = "Admin, Moderator, User")]
        public ActionResult Index()
        {
            return View(db.Manufacturers.ToList());  
        }

        [Authorize(Roles = "Admin, Moderator, User")]
        public ActionResult Stats()
        {
            var query = (from m in db.Manufacturers
                         select new ManufacturerVM
                         {
                             Man = m.Name,
                             Rat = m.Rating,
                             TCount = db.Tools.Count(tool => tool.ManufacturerId == m.Id),
                             TRating = db.Tools.Where(tool => tool.ManufacturerId == m.Id).Select(tool => tool.Rating).DefaultIfEmpty(0).Average()
                         }
            );
            return View(query.ToList());
        }

        // GET: Manufacturers/Details/5
        [Authorize(Roles = "Admin, Moderator, User")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.FindManufacturerById((long)id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            ViewData["ToolCount"] = db.Tools.Count(t => t.ManufacturerId == id);

            if ((int)ViewData["ToolCount"] > 0)
                ViewData["ToolRating"] = db.Tools.Where(t => t.ManufacturerId == id).Select(t => t.Rating).Average();
            else
                ViewData["ToolRating"] = "Brak";
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create([Bind(Include = "Id,Name,CreationDate,Rating")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.FindManufacturerById((long)id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Manufacturer manufacturer = db.FindManufacturerById(id);
            db.Delete(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
