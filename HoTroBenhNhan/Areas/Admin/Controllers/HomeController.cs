using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {

            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                return View();
             }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }
        }

        public ActionResult danhSachKhieuNai()
        {
            var dsKhieuNai = DuLieuHienTang.Khieunai.ToList();
            return View(dsKhieuNai);
        }

        public ActionResult xoaKhieuNai(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Khieunai dsKhieuNai = DuLieuHienTang.Khieunai.Find(id);
            if (dsKhieuNai == null)
            {
                return HttpNotFound();
            }
            return View(dsKhieuNai);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Khieunai dsKhieuNai = DuLieuHienTang.Khieunai.Find(id);
            DuLieuHienTang.Khieunai.Remove(dsKhieuNai);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("danhSachKhieuNai");
        }

        public ActionResult xemKhieuNai(int Id)
        {
            var khieuNai = DuLieuHienTang.Khieunai.Where(n => n.MaKN == Id).FirstOrDefault();
            return View(khieuNai);
        }


    }
}