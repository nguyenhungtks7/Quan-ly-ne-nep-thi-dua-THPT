using Quab_Ly_ne_nep_thi_dua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class HomeController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();
        public ActionResult Index()
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            return View();
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
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public ActionResult Doimatkhau()
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Index");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Doimatkhau(Updatepassword model)
        {
            if (ModelState.IsValid)
            {
                string tendn = (string)Session["Tendangnhap"];
                var existingUser = db.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == tendn);

                if (existingUser != null && existingUser.MatKhau == HashPassword(model.CurrentPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("", "Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
                        return View(model);
                    }

                    existingUser.MatKhau = HashPassword(model.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không đúng.");
                    return View(model);
                }
            }

            return View(model);
        }
    }
}