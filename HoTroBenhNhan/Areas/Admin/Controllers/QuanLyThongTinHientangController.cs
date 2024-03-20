using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Areas.Admin.Controllers
{
    public class QuanLyThongTinHientangController : Controller
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
                var thongtinhientang = DuLieuHienTang.Thongtinhientang.OrderByDescending(m => m.MaHT);
                thongtinhientang = (IOrderedQueryable<Thongtinhientang>)DuLieuHienTang.Thongtinhientang.OrderByDescending(m => m.Ngaytaobaiviet);
                return View(thongtinhientang);
            }
            else
            {
                return Redirect("~/NguoiDung/Khongduoctruycap");
            }

        }
    }
}