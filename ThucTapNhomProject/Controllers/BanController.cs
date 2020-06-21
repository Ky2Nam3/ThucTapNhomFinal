using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucTapNhomProject.Models;

namespace ThucTapNhomProject.Controllers
{
    public class BanController : Controller
    {
        // GET: Ban
        Data_Coffee db = new Data_Coffee();
        // GET: Admin/Ban
        public ActionResult Ban()
        {
            var mode = db.Bans.SqlQuery("select * from Ban");
            return View(mode);
        }

        public ActionResult DatBan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DatBan(Ban model)
        {
            db.Bans.Add(model);
            db.SaveChanges();
            return RedirectToAction("Ban");
        }

        public ActionResult DoiTrangThai(int id)
        {
            var model = db.Bans.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DoiTrangThai(Ban model)
        {
            var obj = db.Bans.Find(model.MaBan);
            obj.MaBan = model.MaBan;
            obj.TrangThai = model.TrangThai;
            db.SaveChanges();
            return RedirectToAction("Ban", model);
        }
        public ActionResult Delete(int id)
        {
            var model = db.Bans.Find(id);
            db.Bans.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Ban");
        }
    }
}
