using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class BenhVienController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        // GET: Admin/BenhVien
        public ActionResult danhSachBenhVien()
        {
            var dsBenhVien = DuLieuHienTang.Benhvien.ToList();
            return View(dsBenhVien);
        }

        public ActionResult xoaBenhVien(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benhvien benhVien = DuLieuHienTang.Benhvien.Find(id);
            if (benhVien == null)
            {
                return HttpNotFound();
            }
            return View(benhVien);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Benhvien benhVien = DuLieuHienTang.Benhvien.Find(id);
            DuLieuHienTang.Benhvien.Remove(benhVien);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("danhSachKhieuNai");
        }
        [HttpGet]
        public ActionResult Create()
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                Benhvien benhVien = new Benhvien();
                return View(benhVien);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        [HttpPost]
        public ActionResult Create(Benhvien benhVien)
        {

            try
            {
               
                    if (benhVien.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(benhVien.ImageUpload.FileName);
                        string extension = Path.GetExtension(benhVien.ImageUpload.FileName);
                        fileName = fileName + extension;
                        benhVien.Anh = "/Content/images/" + fileName;
                        benhVien.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
                    }
                    DuLieuHienTang.Benhvien.Add(benhVien);
                    DuLieuHienTang.SaveChanges();
                    return RedirectToAction("danhSachBenhVien");
                
               
            }
            catch (Exception)
            {
                return View();
            }

        }
    }
}