using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Quab_Ly_ne_nep_thi_dua.Models;
using PagedList.Mvc;
using PagedList;
namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class TaiKhoansController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: TaiKhoans
        public ActionResult Index(string taikhoan, int? Vaitro, bool? trangthai, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            ViewBag.taikhoanFilter = taikhoan;



            var tk = from s in db.TaiKhoans select s;

            if (!String.IsNullOrEmpty(taikhoan))
            {
                tk = tk.Where(s => s.TenDangNhap.Contains(taikhoan));
            }
            if (Vaitro != null)
            {
                tk = tk.Where(s => s.QuyenTruyCap == Vaitro);
            }
            if (trangthai != null)
            {
                tk = tk.Where(s => s.TrangThai == trangthai);
            }

            if (!tk.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tk.OrderByDescending(h => h.IdTaiKhoan).ToPagedList(pageNumber, pageSize));
        }

        // GET: TaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Create
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTaiKhoan,TenDangNhap,MatKhau,QuyenTruyCap,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.TaiKhoans.FirstOrDefault(u => u.TenDangNhap == taiKhoan.TenDangNhap);
                if (existingUser != null)
                {
                    ModelState.AddModelError("TenDangNhap", "Tài khoản đã tồn tại. Vui lòng chọn tên đăng nhập khác.");
                    return View(taiKhoan);
                }

               

                taiKhoan.MatKhau = HashPassword(taiKhoan.MatKhau);

                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }

            return View(taiKhoan);
        }
 
        // GET: TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTaiKhoan,TenDangNhap,MatKhau,QuyenTruyCap,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            TempData["Xoathanhcong"] = true;
            return RedirectToAction("Index");
        }
        public ActionResult DatLaiMatKhau(int id)
        {
            var taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

            // Hiển thị trang web với thông tin tài khoản và biểu mẫu đặt lại mật khẩu
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatLaiMatKhau(int? id)
        {
            var taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

         
            taiKhoan.MatKhau = HashPassword("111111");
            TempData["Datlaithanhcong"] = true;
            db.SaveChanges();

            return RedirectToAction("Index");
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
                    TempData["Doimatkhauthanhcong"] = true;
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
