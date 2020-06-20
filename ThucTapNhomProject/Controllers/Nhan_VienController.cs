using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThucTapNhomProject.Models;

namespace TTCSDL_CAFE.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        private Data_Coffee db = new Data_Coffee();
        [HttpPost]
        public ActionResult Index(string searching)
        {

            var links = from l in db.NhanViens // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searching)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.HoTen.Contains(searching)); //lọc theo chuỗi tìm kiếm
            }

            return View(links); //trả về kết quả

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.NhanViens.ToList());
        }

        // GET: Admin/NhanViens1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanViens1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoTen,NgaySinh,SDT,ViTri,LuongCoBan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: Admin/NhanViens1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoTen,NgaySinh,SDT,ViTri,LuongCoBan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens1/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    NhanVien nhanVien = db.NhanViens.Find(id);
        //    if (nhanVien == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nhanVien);
        //}

        //// POST: Admin/NhanViens1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NhanVien nhanVien = db.NhanViens.Find(id);
        //    db.NhanViens.Remove(nhanVien);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        [HttpGet]
        public ActionResult DeleteNV(int manv)
        {
            NhanVien nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == manv);

            HoaDon HD = db.HoaDons.SingleOrDefault(n => n.MaNV == manv);
            List<ChiTietHoaDon> cthd = db.ChiTietHoaDons.Where(n => n.SoHD == HD.SoHD).ToList();

            PhieuNhap PN = db.PhieuNhaps.SingleOrDefault(n => n.MaNV == manv);
            List<ChiTietPhieuNhap> ctpn = db.ChiTietPhieuNhaps.Where(n => n.MaPhieuNhap == PN.MaPhieuNhap).ToList();
            if(nhanVien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhanViens.Remove(nhanVien); 
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
