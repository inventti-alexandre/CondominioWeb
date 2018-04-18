using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BuildingProject.DataAccess;
using BuildingProject.Model;
using BuildingProject.ModelView;

namespace BuildingProject.Controllers
{
    public class UserRolesController : Controller
    {
        private BuildingContext db = new BuildingContext();

        // GET: UserRoles
        public ActionResult Index(int? id)
        {
            if (DataUtil.Validation())
            {
                if (id == null)
                    id = 0;

                UserRoleView objUserRoleView = new UserRoleView();
                objUserRoleView.userRoleList = db.UserRole.Include(u => u.role).Include(u => u.user).Where(u => u.user.userID == id).ToList();
                objUserRoleView.roles = db.Database.SqlQuery<Role>("select r.roleid, r.name, CAST(ISNULL((select 1 from userRoles ur where ur.userID = @userID and ur.roleID = r.roleID),0) AS BIT) selected, r.active, r.createdate, r.createuser, r.updatedate, r.updateuser from roles r where r.active = 1 ", new SqlParameter("@userID", id)).ToList();
                objUserRoleView.user = db.User.Find(id);
                return View(objUserRoleView);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Index(UserRoleView objUserRoleView)
        {
            if (DataUtil.Validation())
            {
                UserRoleView objUserRoleView2 = new UserRoleView();
                try
                {
                    if (Request.Form["ActionForm"] == "Registrar")
                    {
                        var RemoveAll = db.UserRole.Where(x => x.userID == objUserRoleView.user.userID);
                        db.UserRole.RemoveRange(RemoveAll);
                        db.SaveChanges();

                        UserRole objUserRole = new UserRole();
                        foreach (var item in objUserRoleView.roles)
                        {
                            if (item.selected)
                            {
                                objUserRole.userID = objUserRoleView.user.userID;
                                objUserRole.roleID = item.roleID;
                                db.UserRole.Add(objUserRole);
                                db.SaveChanges();
                            }
                        }



                        objUserRoleView2.userRoleList = db.UserRole.Include(u => u.role).Include(u => u.user).Where(u => u.user.userID == objUserRoleView.user.userID).ToList();
                        objUserRoleView2.roles = db.Database.SqlQuery<Role>("select r.roleid, r.name, CAST(ISNULL((select 1 from userRoles ur where ur.userID = @userID and ur.roleID = r.roleID),0) AS BIT) selected, r.active, r.createdate, r.createuser, r.updatedate, r.updateuser from roles r where r.active = 1 ", new SqlParameter("@userID", objUserRoleView.user.userID)).ToList();
                        objUserRoleView2.user = db.User.Find(objUserRoleView.user.userID);

                        ModelState.Clear();
                    }

                }
                catch (Exception ex)
                {

                }
                return View(objUserRoleView2);
            }
            else
                return RedirectToAction("Login", "Home");
        }


        public ActionResult Delete(int id)
        {
            if (DataUtil.Validation())
            {
                var userRole = db.UserRole.Find(id);
                db.UserRole.Remove(userRole);
                db.SaveChanges();
                return RedirectToAction("Index", "UserRoles", new { @id = userRole.userID });
            }
            else
                return RedirectToAction("Login", "Home");
        }       
    }
}
