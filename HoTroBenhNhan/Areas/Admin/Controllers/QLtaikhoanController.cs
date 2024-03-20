using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class QLtaikhoanController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();

        // GET: Admin/QLtaikhoan  
        public ActionResult Index(string sortOrder)
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {


                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.NameDescSort = sortOrder == "name_desc" ? "name_desc" : "name_desc";
                var taikhoan = DuLieuHienTang.Taikhoan.OrderByDescending(m => m.Matk);
                taikhoan = (IOrderedQueryable<Taikhoan>)DuLieuHienTang.Taikhoan.OrderByDescending(m => m.Matk);
                //var objtaikhoan = DuLieuHienTang.Taikhoan.ToList();
                return View(taikhoan);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }


        }
        [HttpGet]
        public ActionResult ChinhQuyen(int id)
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {

                List<SelectListItem> Quandangbai = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Bỏ quyền đăng bài ",Value=""},
        new SelectListItem {
            Text = "Có thể đăng bài", Value = "Có Thể	"}
            };
                ViewBag.dangbai = Quandangbai;

                List<SelectListItem> Quantrivien = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Bỏ quyền quản trị viên ",Value=""},
        new SelectListItem {
            Text = "Quyền quản trị viên", Value = "Là Quản trị viên	"}
            };
                ViewBag.Quantrivien = Quantrivien;
                var tttaikhoan = DuLieuHienTang.Taikhoan.Where(n => n.Matk == id).FirstOrDefault();
                return View(tttaikhoan);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        [HttpPost]
        public ActionResult ChinhQuyen(Taikhoan ttaikhoan)
        {
            List<SelectListItem> Quantrivien = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Bỏ quyền quản trị viên ",Value=""},
        new SelectListItem {
            Text = "Quyền quản trị viên", Value = "Là Quản trị viên	"}
            };
            ViewBag.Quantrivien = Quantrivien;
            List<SelectListItem> Quandangbai = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Bỏ quyền đăng bài ",Value=""},   
        new SelectListItem {
            Text = "Có thể đăng bài", Value = "Có Thể	"}
            };
            ViewBag.dangbai = Quandangbai;
            //var ttttaikhoan = DuLieuHienTang.Taikhoan.SingleOrDefault(x => x.Matk == ttaikhoan.Matk);
            try
            {
                //ttttaikhoan.TenDN = ttaikhoan.TenDN;
                //    ttttaikhoan.Matkhau = ttaikhoan.Matkhau;
                //ttttaikhoan.Anhdaidien = ttaikhoan.Anhdaidien;
                //ttttaikhoan.Cccdtruoc = ttaikhoan.Cccdtruoc;
                //ttttaikhoan.Cccdsau = ttaikhoan.Cccdsau;
                //ttttaikhoan.Sdt = ttaikhoan.Sdt;
                //ttaikhoan.Quyendangbai = ttaikhoan.Quyendangbai;
                DuLieuHienTang.Entry(ttaikhoan).State = EntityState.Modified;
                DuLieuHienTang.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }
        public ActionResult xoaTaiKhoan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taikhoan taiKhoan = DuLieuHienTang.Taikhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Taikhoan taiKhoan = DuLieuHienTang.Taikhoan.Find(id);
            DuLieuHienTang.Taikhoan.Remove(taiKhoan);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult CapQuyen(int id)
        //{
        //    List<SelectListItem> Quantrivien = new List<SelectListItem>() {
        //new SelectListItem {
        //    Text = "Quyền quản trị viên", Value = "Quản trị viên"},
        //new SelectListItem {
        //    Text = "Bỏ quyền quản trị viên ",Value=""}
        //    };
        //    ViewBag.Quantrivien = Quantrivien;
        //    var tttaikhoan = DuLieuHienTang.Taikhoan.Where(n => n.Matk == id).FirstOrDefault();
        //    return View(tttaikhoan);
        //}
        //[HttpPost]
        //public ActionResult CapQuyen(int id, Taikhoan taikhoan)
        //{
        //    var tttaikhoan = DuLieuHienTang.Taikhoan.SingleOrDefault(x => x.Matk == taikhoan.Matk);

        //    List<SelectListItem> Quantrivien = new List<SelectListItem>() {
        //    new SelectListItem {
        //        Text = "Quyền quản trị viên", Value = "Quản trị viên"},
        //    new SelectListItem {
        //        Text = "Bỏ quyền quản trị viên ",Value=""}
        //        };
        //    ViewBag.Quantrivien = Quantrivien;

        //    var tttaikhoan = DuLieuHienTang.Taikhoan.SingleOrDefault(x => x.Matk == taikhoan.Matk);
        //    try
        //    {
        //        tttaikhoan.TenDN = taikhoan.TenDN;
        //        tttaikhoan.Matkhau = taikhoan.Matkhau;
        //        tttaikhoan.Anhdaidien = taikhoan.Anhdaidien;
        //        tttaikhoan.Cccdtruoc = taikhoan.Cccdtruoc;
        //        tttaikhoan.Cccdsau = taikhoan.Cccdsau;
        //        tttaikhoan.Quyendangbai = taikhoan.Quyendangbai;
        //        DuLieuHienTang.Entry(taikhoan).State = EntityState.Modified;
        //        DuLieuHienTang.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return View(taikhoan);
        //    }

        //}


    }
}
 