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
        public ActionResult Index(int id)
        {
            if (DataUtil.Validation())
            {
                var section = db.Section.Include(a => a.building);
                var result = section.ToList().Where(x => x.buildingID == id).ToList();
                string buildingName = "";
                if (result.Count > 0)
                    buildingName = result[0].building.name;                
                ViewBag.BuildingName = buildingName;
                ViewBag.BuildingID = id;
                return View(result);
            }
            else
                return RedirectToAction("Login", "Home");
        }
              
        // GET: Sections/Create
        public ActionResult Create(int id)
        {
            if (DataUtil.Validation())
            {
                Section section = new Section();
                section.buildingID = id;
                return PartialView(section);
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
                return PartialView(section);
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
                    return RedirectToAction("Index", new { id = section.buildingID });
                }
                ViewBag.buildingID = new SelectList(db.Building, "buildingID", "name", section.buildingID);
                return View(section);
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
