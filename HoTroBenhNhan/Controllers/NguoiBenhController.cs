using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;


namespace HoTroBenhNhan.Controllers
{
    public class NguoiBenhController : Controller
    {
        // GET: NguoiBenh
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();

        public ActionResult BaivietBN(int Id)
        {

            var objNguoiBenh = DuLieuHienTang.Thongtinbenhnhan.Where(n => n.Mabaiviet == Id).FirstOrDefault();
            return View(objNguoiBenh);
        }

        [HttpGet]
        public ActionResult dsNguoiBenh(int? page)
        {
            if (page == null) page = 1;
            var dsNguoiBenh = (from l in DuLieuHienTang.Thongtinbenhnhan
                               select l).OrderByDescending(x => x.Mabaiviet);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(dsNguoiBenh.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Taobaiviet()
        {
            if (Session["Matk"] != null && Session["Quyendangbai"] != null)
            {
                var benhList = DuLieuHienTang.Trinhtangbenh.ToList();
            ViewBag.Listbenh = new SelectList(benhList, "Mabenh", "Tenbenh");
            var tinhthanhphoList = DuLieuHienTang.TinhThanhPho.ToList();
            ViewBag.Listtinhthanhpho = new SelectList(tinhthanhphoList, "ID", "tenTinhThanhPho");
            var quanhuyenList = DuLieuHienTang.QuanHuyen.ToList();
            ViewBag.Listquanhuyen = new SelectList(quanhuyenList, "ID", "tenQuanHuyen");
            var xaphuongList = DuLieuHienTang.XaPhuong.ToList();
            ViewBag.Listxaphuong = new SelectList(xaphuongList, "ID", "tenXaPhuong");

            Thongtinbenhnhan nguoibenh = new Thongtinbenhnhan(); 
            return View(nguoibenh);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        [HttpPost]
        public ActionResult Taobaiviet(Thongtinbenhnhan benhnhan)
        {
            var benhList = DuLieuHienTang.Trinhtangbenh.ToList();
            ViewBag.Listbenh = new SelectList(benhList, "Mabenh", "Tenbenh");
            var tinhthanhphoList = DuLieuHienTang.TinhThanhPho.ToList();
            ViewBag.Listtinhthanhpho = new SelectList(tinhthanhphoList, "ID", "tenTinhThanhPho");
            var quanhuyenList = DuLieuHienTang.QuanHuyen.ToList();
            ViewBag.Listquanhuyen = new SelectList(quanhuyenList, "ID", "tenQuanHuyen");
            var xaphuongList = DuLieuHienTang.XaPhuong.ToList();
            ViewBag.Listxaphuong = new SelectList(xaphuongList, "ID", "tenXaPhuong");

            try
            {
                if (benhnhan.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(benhnhan.ImageUpload.FileName);
                    string extension = Path.GetExtension(benhnhan.ImageUpload.FileName);
                    fileName = fileName + extension;
                    benhnhan.Hinhanh = "/Content/images/" + fileName;
                    benhnhan.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
                }
                benhnhan.Ngaytao = DateTime.Now;
                benhnhan.Matk = (int)Session["Matk"];
                DuLieuHienTang.Thongtinbenhnhan.Add(benhnhan);
                DuLieuHienTang.SaveChanges();
                return RedirectToAction("dsNguoiBenh");
            }
            catch (Exception)
            {
                ViewBag.loi = "vui lòng nhập đầy đủ";
                return View();
            }
        }
    }
}