using HoTroBenhNhan.Data;
using HoTroBenhNhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;

namespace HoTroBenhNhan.Controllers
{
    public class NguoiHienController : Controller
    {
        // GET: NguoiHien
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        public ActionResult ChiTietNguoiHien(int Id)
        {
            var objNguoiHien = DuLieuHienTang.Thongtinhientang.Where(n => n.MaHT == Id).FirstOrDefault();
            return View(objNguoiHien);
        }


        [HttpGet]
        public ActionResult dsNguoiBenh(int? page)
        {
            if (page == null) page = 1;
            var dsNguoiBenh = (from l in DuLieuHienTang.Thongtinhientang
                               select l).OrderByDescending(x => x.MaHT);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(dsNguoiBenh.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Taobaiviet()
        {
            if (Session["Matk"] != null && Session["Quyendangbai"] != null)
            {
                var benhvienList = DuLieuHienTang.Benhvien.ToList();
            ViewBag.Listbenhvien = new SelectList(benhvienList, "mabenhvien", "tenbenhvien");
            var benhtinhList = DuLieuHienTang.Trinhtangbenh.ToList();
            ViewBag.Listhientang = new SelectList(benhtinhList, "Mabenh", "Tenbenh");

            Thongtinhientang nguoihien = new Thongtinhientang();
            return View(nguoihien);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }

        [HttpPost]
        
        public ActionResult Taobaiviet(Thongtinhientang hientang)
        {
            var benhvienList = DuLieuHienTang.Benhvien.ToList();
            ViewBag.Listbenhvien = new SelectList(benhvienList, "mabenhvien", "tenbenhvien");
            var benhtinhList = DuLieuHienTang.Trinhtangbenh.ToList();
            ViewBag.Listhientang = new SelectList(benhtinhList, "Mabenh", "Tenbenh");
            //try
            //{
                //if (benhnhan.ImageUpload != null)
                //{
                //    string fileName = Path.GetFileNameWithoutExtension(benhnhan.ImageUpload.FileName);
                //    string extension = Path.GetExtension(benhnhan.ImageUpload.FileName);
                //    fileName = fileName + extension;
                //    benhnhan.Hinhanh = "/Content/images/" + fileName;
                //    benhnhan.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
                //}
                hientang.Ngaytaobaiviet = DateTime.Now;
                //hientang.Matk =DuLieuHienTang.Taikhoan.FirstOrDefault().Matk;
                hientang.Matk = (int)Session["Matk"];
                //benhnhan.Matk = Convert.ToDouble(Session["Matk"].ToString()).ParseInt;
                DuLieuHienTang.Thongtinhientang.Add(hientang);
                DuLieuHienTang.SaveChanges();
                return RedirectToAction("dsNguoiBenh");
            //}
            //catch (Exception)
            //{
            //    ViewBag.loitrong = "vui lòng nhập đầy đủ thông tin";
            //    return View();
            //}
        }

        //[HttpGet]
        //public ActionResult Edit(int? id)
        //{
        //    var benhvienList = DuLieuHienTang.Benhvien.ToList();
        //    ViewBag.Listbenhvien = new SelectList(benhvienList, "mabenhvien", "tenbenhvien");
        //    var benhtinhList = DuLieuHienTang.Trinhtangbenh.ToList();
        //    ViewBag.Listhientang = new SelectList(benhtinhList, "Mabenh", "Tenbenh");
        //    var nguoihien = DuLieuHienTang.Thongtinhientang.Where(n => n.MaHT == id).FirstOrDefault();
        //    return View(nguoihien);
            
        //}
        //[HttpPost]

        //public ActionResult Edit(Thongtinhientang hientang)
        //{
        //    var benhvienList = DuLieuHienTang.Benhvien.ToList();
        //    ViewBag.Listbenhvien = new SelectList(benhvienList, "Mabenh", "tenbenh");
        //    var benhtinhList = DuLieuHienTang.Trinhtangbenh.ToList();
        //    ViewBag.Listhientang = new SelectList(benhtinhList, "Mabenh", "Tenbenh");
        //    try
        //    {
        //        //if (benhnhan.ImageUpload != null)
        //        //{
        //        //    string fileName = Path.GetFileNameWithoutExtension(benhnhan.ImageUpload.FileName);
        //        //    string extension = Path.GetExtension(benhnhan.ImageUpload.FileName);
        //        //    fileName = fileName + extension;
        //        //    benhnhan.Hinhanh = "/Content/images/" + fileName;
        //        //    benhnhan.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
        //        //}
        //        hientang.Ngaytaobaiviet = DateTime.Now;
        //        //hientang.Matk =DuLieuHienTang.Taikhoan.FirstOrDefault().Matk;
        //        hientang.Matk = (int)Session["Matk"];
        //        //benhnhan.Matk = Convert.ToDouble(Session["Matk"].ToString()).ParseInt;
        //        DuLieuHienTang.Entry(hientang).State = EntityState.Modified;
        //        DuLieuHienTang.SaveChanges();
        //        return RedirectToAction("dsNguoiBenh");
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.loitrong = "vui lòng nhập đầy đủ thông tin";
        //        return View();
        //    }
        //}
    }
}