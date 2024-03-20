using HoTroBenhNhan.Data;
using HoTroBenhNhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Controllers
{

    public class HomeController : Controller
    {

        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.listThongtinbenhnhan = DuLieuHienTang.Thongtinbenhnhan.ToList();
            objHomeModel.listThongtinhientang = DuLieuHienTang.Thongtinhientang.ToList();
            

            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}