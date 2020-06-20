using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucTapNhomProject.Models;

namespace ThucTapNhomProject.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        Data_Coffee db = new Data_Coffee();
        public ActionResult Index()
        {
            var model = db.HoaDons.Where(x => x.MaBan != 0);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ThemHD(HoaDon model)
        {
            db.HoaDons.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ThemHoaDon(int id)
        {
            var model = db.HoaDons.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ThemHoaDon(ChiTietHoaDon model)
        {
            db.ChiTietHoaDons.Add(model);

            Mon a = db.Mons.Find(model.MaMon);
            HoaDon hd = db.HoaDons.Find(model.SoHD);
            Double? Tien = a.DonGia * model.SoLuong;
            double? TongtienHD = hd.TongTien + Tien; 

            hd.SoHD = model.SoHD;
            hd.TongTien = Convert.ToDouble(TongtienHD);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            HoaDon hd = db.HoaDons.SingleOrDefault(n => n.SoHD == id);
            List<ChiTietHoaDon> cthd = db.ChiTietHoaDons.Where(n => n.SoHD == id).ToList();
            if (hd == null)
            {
                Response.StatusCode = 404; 
                return null;
            }
            db.HoaDons.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}