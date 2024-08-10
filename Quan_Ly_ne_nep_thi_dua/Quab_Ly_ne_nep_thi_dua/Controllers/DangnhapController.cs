using Quab_Ly_ne_nep_thi_dua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class DangnhapController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();
        // GET: Dangnhap
        public ActionResult Dangxuat()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Index");
        }
        public ActionResult Dangnhap()
        {
            if (Session["Tendangnhap"] != null)
            {
                
                // Người dùng đã đăng nhập, chuyển hướng đến trang khác
                return RedirectToAction("Dangnhap", "Dangnhap"); // Hoặc bất kỳ trang nào bạn muốn
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(Login model)
        {
            if (ModelState.IsValid) // Kiểm tra xem ModelState có hợp lệ không
            {
                if (!string.IsNullOrEmpty(model.Password)) // Kiểm tra trống mật khẩu
                {
                    string pass = HashPassword(model.Password);
                    var user = db.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == model.UserName && u.MatKhau == pass);
                    if (user != null)
                    {
                        if (user.TrangThai == true)
                        {
                            if (user.QuyenTruyCap == 1)
                            {
                                //Admin
                                TempData["DangNhapThanhCong"] = true;

                                Session["Tendangnhap"] = user.TenDangNhap;
                                return RedirectToAction("Index", "Home");
                            }
                            else if (user.QuyenTruyCap == 2)
                            {
                                //Lớp trực ban
                                //Để tạm
                                TempData["DangNhapThanhCong"] = true;
                                Session["Tendangnhap"] = user.TenDangNhap;
                                return RedirectToAction("Index", "Index");
                            }
                            else if (user.QuyenTruyCap == 3)
                            {
                                //Giáo viên
                                TempData["DangNhapThanhCong"] = true;
                                Session["giaovien"] = true;
                                Session["Tendangnhap"] = user.TenDangNhap;
                                return RedirectToAction("Index", "Home");
                            }
                            else if (user.QuyenTruyCap == 4)
                            {
                                //ban giám hiệu
                                TempData["DangNhapThanhCong"] = true;
                                Session["bangiamhieu"] = true;
                                Session["Tendangnhap"] = user.TenDangNhap;
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            
                            ModelState.AddModelError("", "Tài khoản này hiện tại không hoạt động.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu.");
                    return View(model);
                }
            }


            return View(model);
        }

    }

}