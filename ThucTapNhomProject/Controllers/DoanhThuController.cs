using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucTapNhomProject.Models;

namespace ThucTapNhomProject.Controllers
{
    public class DoanhThuController : Controller
    {
        // GET: DoanhThu
        Data_Coffee db = new Data_Coffee();
        public ActionResult Index()
        {
            var model = db.HoaDons.SqlQuery("select * from HoaDon");
            return View(model);
        }

        [HttpPost]
        public ActionResult ThongKeDoanhThu(DateTime? ngay1, DateTime? ngay2)
        {

            List<HoaDon> model = new List<HoaDon>();
            if (ngay1.ToString() == "" && ngay2.ToString() == "")
            {
                model = db.HoaDons.ToList();
            }
            else
            {
                model = db.HoaDons.Where(x => x.NgayTao >= ngay1 && x.NgayTao <= ngay2).ToList();
            }
            return View("Index", model);
        }
    }
}