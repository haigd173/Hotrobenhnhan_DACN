using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Controllers
{
    public class KhieuNaiController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        // GET: KhieuNai
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TaoKhieuNai()
        {
            Khieunai khieunai = new Khieunai();
            return View(khieunai);

        }
        [HttpPost]
        public ActionResult TaoKhieuNai(Khieunai kkhieunai)
        {
            if (kkhieunai.LinkBaiviet != null || kkhieunai.NoidungKN != null)
            {
                kkhieunai.Matk = (int)Session["Matk"];
                DuLieuHienTang.Khieunai.Add(kkhieunai);
                DuLieuHienTang.SaveChanges();
                return View("KNThanhcong");

            }
            else
            {
                ViewBag.error = " vui lòng nhập đầy đủ thông tin ";
                return View();
            }

        }
    }
}