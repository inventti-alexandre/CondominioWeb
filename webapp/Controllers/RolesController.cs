using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace BuildingProject.Controllers
{
    public class RolesController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Roles
        public ActionResult Index()
        {
            if (DataUtil.Validation())
                return View(db.Role.ToList().OrderBy(x => x.name));
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            if (DataUtil.Validation())
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleID,name,active,createDate,createUser,updateDate,updateUser,selected")] Role role)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Role.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleID,name,active,createDate,createUser,updateDate,updateUser,selected")] Role role)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (DataUtil.Validation())
            {
                Role role = db.Role.Find(id);
                db.Role.Remove(role);
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
