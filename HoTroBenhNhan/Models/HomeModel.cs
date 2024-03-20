using HoTroBenhNhan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoTroBenhNhan.Models
{
    public class HomeModel
    {
        public List<Thongtinbenhnhan> listThongtinbenhnhan { get; set; }
        public List<Thongtinhientang> listThongtinhientang { get; set; }
        
    }
    public class NguoiHienModel
    {
        public List<Taikhoan> listTaiKhoan { get; set; }
        public List<Thongtinhientang> listThongtinhientang { get; set; }
    }
}