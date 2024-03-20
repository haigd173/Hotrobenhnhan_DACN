using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class BaiVietController : Controller
    {


        // GET: Admin/BaiViet
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();

        public ActionResult danhSachBaiViet()
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                var dsBaiViet= (from l in DuLieuHienTang.baiViet
                               select l).OrderByDescending(x => x.ngayTao);
                return View(dsBaiViet);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }
        // GET: Admin/tinTuc
        [HttpGet]
        public ActionResult Taobaiviet()
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                var Loaibaivietlist = DuLieuHienTang.LoaiBlog.ToList();
                ViewBag.Listblog = new SelectList(Loaibaivietlist, "idLoai", "TenLoai");
                baiViet listBaiViet = new baiViet();
            return View(listBaiViet);
            }
            return Redirect("~/NguoiDung/Khongduoctruycap");
        }

        [HttpPost]
        public ActionResult Taobaiviet(baiViet tintuc)
        {
            if (tintuc.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(tintuc.ImageUpload.FileName);
                string extension = Path.GetExtension(tintuc.ImageUpload.FileName);
                fileName = fileName + extension;
                tintuc.hinhAnhBaiViet = "/Content/images/" + fileName;
                tintuc.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
            }
            tintuc.ngayTao = DateTime.Now;
            DuLieuHienTang.baiViet.Add(tintuc);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("danhSachBaiViet");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                baiViet link = DuLieuHienTang.baiViet.Find(id);
                if (link == null)
                {
                    return HttpNotFound();
                }
                    return View(link);
                }
                else
                {
                    return Redirect("~/NguoiDung/Khongduoctruycap");
                }
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            baiViet baiviet = DuLieuHienTang.baiViet.Find(id);
            DuLieuHienTang.baiViet.Remove(baiviet);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("danhSachBaiViet");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {


                var baiviet = DuLieuHienTang.baiViet.Where(n => n.idbaiViet == id).FirstOrDefault();
            return View(baiviet);
}
            else
{
    return Redirect("~/NguoiDung/Khongduoctruycap");
}
        }
        public ActionResult Edit(baiViet baiviet)
        {
                
            DuLieuHienTang.Entry(baiviet).State = EntityState.Modified;
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("danhSachBaiViet");
        }
    }
}