using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class ThiDuaController : Controller
    {
        // GET: ThiDua
        public ActionResult ThiDua()
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            return View();
        }
    }
}