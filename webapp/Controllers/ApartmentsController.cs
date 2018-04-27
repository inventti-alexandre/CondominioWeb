using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace SmartAdminMvc.Controllers
{
    public class ApartmentsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Apartments
        public ActionResult Index()
        {
            if (DataUtil.Validation())
            {
                var apartment = db.Apartment.Include(a => a.section);
                return View(apartment.ToList());
            }
            else
                return RedirectToAction("Login", "Home");
        }

       // GET: Apartments/Create
        public ActionResult Create()
        {
            if (DataUtil.Validation())
            {
                ViewBag.sectionID = new SelectList(db.Section, "sectionID", "name");
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Apartments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apartmentID,sectionID,name,active,createDate,createUser,updateDate,updateUser")] Apartment apartment)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Apartment.Add(apartment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.sectionID = new SelectList(db.Section, "sectionID", "name", apartment.sectionID);
                return View(apartment);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Apartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Apartment apartment = db.Apartment.Find(id);
                if (apartment == null)
                {
                    return HttpNotFound();
                }
                ViewBag.sectionID = new SelectList(db.Section, "sectionID", "name", apartment.sectionID);
                return View(apartment);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Apartments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "apartmentID,sectionID,name,active,createDate,createUser,updateDate,updateUser")] Apartment apartment)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(apartment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.sectionID = new SelectList(db.Section, "sectionID", "name", apartment.sectionID);
                return View(apartment);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Apartment apartment = db.Apartment.Find(id);
                if (apartment == null)
                {
                    return HttpNotFound();
                }
                return View(apartment);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (DataUtil.Validation())
            {
                Apartment apartment = db.Apartment.Find(id);
                db.Apartment.Remove(apartment);
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
