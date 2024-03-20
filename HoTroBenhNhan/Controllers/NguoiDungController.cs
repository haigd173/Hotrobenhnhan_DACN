using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace HoTroBenhNhan.Controllers
{

    public class NguoiDungController : Controller
    {
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();


        [HttpGet]
        public ActionResult Dangki()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangki(Taikhoan _taikhoan)
        {
            
            if (ModelState.IsValid)
            {
                var check = DuLieuHienTang.Taikhoan.FirstOrDefault(s => s.TenDN == _taikhoan.TenDN);
                if (check == null)
                {
                    try
                    {


                        string fileName = Path.GetFileNameWithoutExtension(_taikhoan.ImageUpload.FileName);
                        string extension = Path.GetExtension(_taikhoan.ImageUpload.FileName);
                        fileName = fileName + extension;
                        _taikhoan.Cccdtruoc = "/Content/images/" + fileName;
                        _taikhoan.Cccdsau = "/Content/images/" + fileName;
                        _taikhoan.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Content/images/"), fileName));
                        _taikhoan.Matkhau = GetMD5(_taikhoan.Matkhau);
                        DuLieuHienTang.Configuration.ValidateOnSaveEnabled = false;
                        DuLieuHienTang.Taikhoan.Add(_taikhoan);
                        DuLieuHienTang.SaveChanges();
                        return RedirectToAction("DangNhap");
                    }
                    catch
                    {
                        ViewBag.error2 = "Vui lòng tải ảnh căn cước công dân";
                        return View();
                        
                    }
                }
                else
                {
                    ViewBag.error="Email , tên đăng nhập đã tôn tại";
                    return View();
                }

            }
            return View();
        }
        //Mã hoá password
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult DangNhap(string tendn, string matkhau)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(matkhau);
                var data = DuLieuHienTang.Taikhoan.Where(s => s.TenDN.Equals(tendn) && s.Matkhau.Equals(f_password)).ToList();
                
                if (data.Count() > 0)
                {
                    //add session
                    Session["TenDN"] = data.FirstOrDefault().TenDN;
                    Session["Matk"] = data.FirstOrDefault().Matk;
                    Session["Quyenadmin"] = data.FirstOrDefault().Quyenadmin;
                    Session["Quyendangbai"] = data.FirstOrDefault().Quyendangbai;

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không đúng vui lòng kiểm tra lại ";
                    return View();
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap");
        }
        public ActionResult khongduoctruycap()
        {
            return View();
        }
    }

}
