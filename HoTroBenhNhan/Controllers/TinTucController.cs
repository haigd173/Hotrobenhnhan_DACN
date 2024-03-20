using HoTroBenhNhan.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoTroBenhNhan.Controllers
{
    public class TinTucController : Controller
    {
        // GET: TinTuc
        DulieuhientangEntities DuLieuHienTang = new DulieuhientangEntities();
        // GET: TinTuc
        public ActionResult Baiviet(int Id)
        {
            var objBaiViet = DuLieuHienTang.baiViet.Where(n => n.idbaiViet == Id).FirstOrDefault();
            return View(objBaiViet);
        }

        [HttpGet]
        public ActionResult dsBaiViet(int? page, string Search, string sortOrder)
        {
            int pageSize;
            int pageNumber;

            if (Search == null)
            {
                if (page == null) page = 1;
                var dsBaiViet = (from l in DuLieuHienTang.baiViet
                                 select l).OrderByDescending(x => x.idbaiViet);
                pageSize = 4;
                pageNumber = (page ?? 1);
                return View(dsBaiViet.ToPagedList(pageNumber, pageSize));
            }

            var objSearch = DuLieuHienTang.baiViet.Where(n => n.TenBaiViet.Contains(Search)).ToList();
            if (page == null) page = 1;
            var dsBaiVietSearch = (from l in objSearch
                                   select l).OrderBy(x => x.idbaiViet);
            pageSize = 6;
            pageNumber = (page ?? 1);
            return View(dsBaiVietSearch.ToPagedList(pageNumber, pageSize));
        }
    }
}