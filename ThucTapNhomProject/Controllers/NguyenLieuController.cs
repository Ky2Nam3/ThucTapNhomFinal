using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucTapNhomProject.Models;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace ThucTapNhomProject.Controllers
{
    public class NguyenLieuController : Controller
    {
        // GET: NguyenLieu
        Data_Coffee db = new Data_Coffee();

        // GET: Admin/NguyenLieux
        [HttpPost]
        public ActionResult Index(string searchingg)
        {

            var links = from l in db.NguyenLieux // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchingg)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenNL.Contains(searchingg)); //lọc theo chuỗi tìm kiếm
            }

            return View(links); //trả về kết quả

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.NguyenLieux.ToList());
        }
        // GET: Admin/NguyenLieux1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            if (nguyenLieu == null)
            {
                return HttpNotFound();
            }
            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieux1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NguyenLieux1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNL,TenNL,DVT,NhaCungCap")] NguyenLieu nguyenLieu)
        {
            if (ModelState.IsValid)
            {
                db.NguyenLieux.Add(nguyenLieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieux1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            if (nguyenLieu == null)
            {
                return HttpNotFound();
            }
            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieux1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNL,TenNL,DVT,NhaCungCap")] NguyenLieu nguyenLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguyenLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguyenLieu);
        }

        // GET: Admin/NguyenLieux1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            if (nguyenLieu == null)
            {
                return HttpNotFound();
            }
            return View(nguyenLieu);
        }

        // POST: Admin/NguyenLieux1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NguyenLieu nguyenLieu = db.NguyenLieux.Find(id);
            db.NguyenLieux.Remove(nguyenLieu);
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

        [HttpPost]
        public ActionResult Search(string TenNL)
        {
            List<NguyenLieu> model = new List<NguyenLieu>();
            if (TenNL != "")
            {
                model = db.NguyenLieux.Where(x => x.TenNL == TenNL).ToList();
            }
            else
            {
                model = db.NguyenLieux.Where(x => x.TenNL != null).ToList();
            }
            return View("Index", model);
        }
    }
}