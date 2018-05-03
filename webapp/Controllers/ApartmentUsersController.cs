using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;
using BuildingProject.ModelView;

namespace SmartAdminMvc.Controllers
{
    public class ApartmentUsersController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: ApartmentUsers
        public ActionResult Index(int id)
        {
            if (DataUtil.Validation())
            {
                var apartmentUser = db.ApartmentUser.Include(a => a.apartment).Include(a => a.user);
                ViewBag.ApartmentId = id;
                return View(apartmentUser.ToList().Where(x=>x.apartmentID == id));
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: ApartmentUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentUser apartmentUser = db.ApartmentUser.Find(id);
            if (apartmentUser == null)
            {
                return HttpNotFound();
            }
            return View(apartmentUser);
        }

        // GET: ApartmentUsers/Create
        public ActionResult Create(int id)
        {
            ApartmentUser apartmentUser = new ApartmentUser();
            apartmentUser.apartmentID = id;
            return View(apartmentUser);
        }

        // POST: ApartmentUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apartmentUserID,apartmentID,userID")] ApartmentUser apartmentUser)
        {
            if (ModelState.IsValid)
            {
                db.ApartmentUser.Add(apartmentUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.apartmentID = new SelectList(db.Apartment, "apartmentID", "name", apartmentUser.apartmentID);
            ViewBag.userID = new SelectList(db.User, "userID", "name", apartmentUser.userID);
            return View(apartmentUser);
        }

        // GET: ApartmentUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentUser apartmentUser = db.ApartmentUser.Find(id);
            if (apartmentUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.apartmentID = new SelectList(db.Apartment, "apartmentID", "name", apartmentUser.apartmentID);
            ViewBag.userID = new SelectList(db.User, "userID", "name", apartmentUser.userID);
            return View(apartmentUser);
        }

        // POST: ApartmentUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "apartmentUserID,apartmentID,userID")] ApartmentUser apartmentUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmentUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.apartmentID = new SelectList(db.Apartment, "apartmentID", "name", apartmentUser.apartmentID);
            ViewBag.userID = new SelectList(db.User, "userID", "name", apartmentUser.userID);
            return View(apartmentUser);
        }

        // GET: ApartmentUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmentUser apartmentUser = db.ApartmentUser.Find(id);
            if (apartmentUser == null)
            {
                return HttpNotFound();
            }
            return View(apartmentUser);
        }

        // POST: ApartmentUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApartmentUser apartmentUser = db.ApartmentUser.Find(id);
            db.ApartmentUser.Remove(apartmentUser);
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
