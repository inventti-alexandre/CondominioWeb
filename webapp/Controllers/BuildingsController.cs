using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;

namespace SmartAdminMvc.Controllers
{
    public class BuildingsController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: Buildings
        public ActionResult Index()
        {
            if (DataUtil.Validation())
                return View(db.Building.ToList());
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Building building = db.Building.Find(id);
                if (building == null)
                {
                    return HttpNotFound();
                }
                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            if (DataUtil.Validation())
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Buildings/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "buildingID,name,address,addressReference,country,state,city,district,apartmentQuantity,active,createDate,createUser,updateDate,updateUser")] Building building)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Building.Add(building);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Building building = db.Building.Find(id);
                if (building == null)
                {
                    return HttpNotFound();
                }
                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Buildings/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "buildingID,name,address,addressReference,country,state,city,district,apartmentQuantity,active,createDate,createUser,updateDate,updateUser")] Building building)
        {
            if (DataUtil.Validation())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(building).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Building building = db.Building.Find(id);
                if (building == null)
                {
                    return HttpNotFound();
                }
                return View(building);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (DataUtil.Validation())
            {
                Building building = db.Building.Find(id);
                db.Building.Remove(building);
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
