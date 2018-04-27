using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.Model;
using BuildingProject.DataAccess;
using System;

namespace SmartAdminMvc.Controllers
{
    public class SectionsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Sections
        public ActionResult Index()
        {
            if (DataUtil.Validation())
            {
                var section = db.Section.Include(a => a.building);
                return View(section.ToList());
            }
            else
                return RedirectToAction("Login", "Home");
        }
              
        // GET: Sections/Create
        public ActionResult Create()
        {
            if (DataUtil.Validation())
            {
                ViewBag.buildingID = new SelectList(db.Building, "buildingID", "name");
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Sections/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sectionID,buildingID,name,active,createDate,createUser,updateDate,updateUser")] Section section)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Section.Add(section);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.buildingID = new SelectList(db.Building, "buildingID", "name", section.buildingID);
                return View(section);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = db.Section.Find(id);
                if (section == null)
                {
                    return HttpNotFound();
                }
                ViewBag.buildingID = new SelectList(db.Building, "buildingID", "name", section.buildingID);
                return View(section);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Sections/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sectionID,buildingID,name,active,createDate,createUser,updateDate,updateUser")] Section section)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(section).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.buildingID = new SelectList(db.Building, "buildingID", "name", section.buildingID);
                return View(section);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = db.Section.Find(id);
                if (section == null)
                {
                    return HttpNotFound();
                }
                return View(section);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (DataUtil.Validation())
            {
                Section section = db.Section.Find(id);
                db.Section.Remove(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Login", "Home");
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
