using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class BenhtinhController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        // GET: Admin/QLBVbenhnhan

        public ActionResult Index()
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {

                var benhtinh = DuLieuHienTang.Trinhtangbenh.ToList();
            return View(benhtinh);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            
            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
            Trinhtangbenh benhtinh = new Trinhtangbenh();
            return View(benhtinh);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        [HttpPost]
        public ActionResult Create(Trinhtangbenh ojbtrinhtrangbenh)
        {

            try
            {
                if(ojbtrinhtrangbenh.Tenbenh != null) 
                {
                    DuLieuHienTang.Trinhtangbenh.Add(ojbtrinhtrangbenh);
                    DuLieuHienTang.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.khongbotrong = "Tên bệnh không được bỏ trống ";
                    return View();
                }              
            }
            catch (Exception)
            {
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
            var benhtinh = DuLieuHienTang.Trinhtangbenh.Where(n => n.Mabenh == id).FirstOrDefault();
            return View(benhtinh);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        [HttpPost]
        public ActionResult Delete(Trinhtangbenh ttbenh)
        {
            var benhtinh = DuLieuHienTang.Trinhtangbenh.Where(n => n.Mabenh == ttbenh.Mabenh).FirstOrDefault();
            DuLieuHienTang.Trinhtangbenh.Remove(benhtinh);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {


           var benhtinh = DuLieuHienTang.Trinhtangbenh.Where(n => n.Mabenh == id).FirstOrDefault();
            return View(benhtinh);
        }
        public ActionResult Edit(Trinhtangbenh benhtinh)
        {
            
            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
            DuLieuHienTang.Entry(benhtinh).State = EntityState.Modified;
            DuLieuHienTang.SaveChanges();
            return RedirectToAction ("Index");
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        
    }
}