using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projekt_dotnet.Models;
using projekt_dotnet4.Models;

namespace projekt_dotnet4.Controllers
{
    public class ManufacturersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string[] CanRead = { "User", "Moderator", "Admin" };
        private string[] CanCreate = { "Moderator", "Admin" };
        private string[] CanUpdate = { "Moderator", "Admin" };
        private string[] CanDelete = { "Admin" };

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturers.ToList());
        }

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
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreationDate,Rating")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreationDate,Rating")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
