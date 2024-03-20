using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class QLBVbenhnhanController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        // GET: Admin/QLBVbenhnhan

        public ActionResult Index(string sortOrder)
        {
            if (Session["Matk"] != null && Session["Quyenadmin"] != null)
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            
            //var thongtinbenhnhan = DuLieuHienTang.Thongtinbenhnhan.ToList();
            var thongtinbenhnhan = DuLieuHienTang.Thongtinbenhnhan.OrderByDescending(m => m.Mabaiviet);
            thongtinbenhnhan = (IOrderedQueryable<Thongtinbenhnhan>)DuLieuHienTang.Thongtinbenhnhan.OrderByDescending(m => m.Ngaytao);
            return View(thongtinbenhnhan);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }

        }
        public ActionResult Delete(int id)
        {
                if (Session["Matk"] != null && Session["Quyenadmin"] != null)
                {
                    var benhnhan = DuLieuHienTang.Thongtinbenhnhan.Where(n => n.Mabaiviet == id).FirstOrDefault();
            return View(benhnhan);
                }
                else
                {
                    return Redirect("~/NguoiDung/Khongduoctruycap");
                }
            }
        [HttpPost]
        public ActionResult Delete(Thongtinbenhnhan ttbenh)
        {
            var tttbenhnhan = DuLieuHienTang.Thongtinbenhnhan.Where(n => n.Mabaiviet == ttbenh.Mabaiviet).FirstOrDefault();
            DuLieuHienTang. Thongtinbenhnhan.Remove(tttbenhnhan);
            DuLieuHienTang.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }

    
}