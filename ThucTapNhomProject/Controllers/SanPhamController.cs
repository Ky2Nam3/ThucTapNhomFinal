using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.Entity;
using ThucTapNhomProject.Models;

namespace TTCSDL_CAFE.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        Data_Coffee db = new Data_Coffee();
        // GET: Admin/SanPham
        //[HttpPost]
        public ActionResult Index()
        {
            var mode = db.Mons.SqlQuery("select * from Mon");
            return View(mode);
        }
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View(db.Mons.Where(n => n.TrangThai == false).OrderBy(n => n.MaLoaiMon));
        //}
        [HttpGet]
        public ActionResult TaoMoi()
        {
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMons.OrderBy(n => n.MaLoaiMon), "MaLoaiMon", "TenLoaiMon");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Mon sp, HttpPostedFileBase HinhAnh)
        {
            //load list  mã sản phẩm 
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMons.OrderBy(n => n.MaLoaiMon), "MaLoaiMon", "TenLoaiMon");

            // kiểm tra hình ảnh đã tồn tại hay chưa 
            if (HinhAnh.ContentLength > 0)
            {
                //lấy tên hình ảnh 
                var filename = Path.GetFileName(HinhAnh.FileName);
                //lấy hình ảnh chuyển đến thư mục hình ảnh 
                var path = Path.Combine(Server.MapPath("~/Content/images/menu1"), filename);
                //nếu thư mục đã có hình ảnh thì xuất ra thông báo 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình Đã Tồn Tại ";
                    return View();
                }
                else
                {
                    //lấy  hình ảnh đưa ra thư mục Hình Ảnh Sản Phẩm 
                    HinhAnh.SaveAs(path);
                    sp.HinhAnh = filename;
                }
            }
            db.Mons.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id 
            //nếu id không tương ứng 
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Mon sp = db.Mons.SingleOrDefault(n => n.MaMon == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load  dropdow list mã loại mónFirstOrDefault
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMons.OrderBy(n => n.MaLoaiMon), "MaLoaiMon", "TenLoaiMon");
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Mon model, HttpPostedFileBase ShowImage)
        {
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMons.OrderBy(n => n.MaLoaiMon), "MaLoaiMon", "TenLoaiMon", model.MaLoaiMon);
            //var item = db.PRODUCTs.Find(model.ProductID);
            //if (ShowImage != null)
            //{
            //    var fileName = Path.GetFileName(ShowImage.FileName);
            //    var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP/"), fileName);
            //    ShowImage.SaveAs(path);

            //    item.ShowImage = ShowImage.FileName;

            //}


            // if (ModelState.IsValid)
            // {

            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            // }
            // return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id 
            //nếu id không tương ứng 
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Mon sp = db.Mons.FirstOrDefault(n => n.MaMon == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            //load  dropdow list mã loại món 
            ViewBag.MaLoaiMon = new SelectList(db.LoaiMons.ToList().OrderBy(n => n.MaLoaiMon), "MaLoaiMon", "TenLoaiMon", sp.MaLoaiMon);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Mon model = db.Mons.FirstOrDefault(n => n.MaMon == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Mons.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}