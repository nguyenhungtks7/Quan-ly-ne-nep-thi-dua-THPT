using Quab_Ly_ne_nep_thi_dua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;

using PagedList.Mvc;
using PagedList;
using System.Web.UI;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
      
        public ActionResult Index()
        {
            return View();
        }
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();
        public ActionResult Tracuudiemthidua(string searchLop, string NamHoc, string HocKy, string Thang, string Tuan, int? page)
        {
       
            var totalPointsByClass =
                from neNep in db.DiemNeNeps
                join hocTap in db.DiemHocTaps on new { neNep.IdLop, neNep.Tuan, neNep.Thang, neNep.HocKy, neNep.NamHoc } equals new { hocTap.IdLop, hocTap.Tuan, hocTap.Thang, hocTap.HocKy, hocTap.NamHoc }
                select new XepLoaiDanhGiaTongDiemcualop
                {
                    idLop = (int)neNep.IdLop,
                    Lop = neNep.Lop.TenLop,
                    DiemHocTap = hocTap.DiemHocTap1,
                    DiemNeNep = neNep.DiemNeNep1,
                    TotalPoints = ((hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0)),
                    Tuan = neNep.Tuan,
                    Thang = neNep.Thang,
                    HocKy = neNep.HocKy,
                    NamHoc = neNep.NamHoc,
                    IsSaved = db.DanhGias.Any(dg => dg.IdLop == neNep.IdLop && dg.Tuan == neNep.Tuan && dg.Thang == neNep.Thang && dg.HocKy == neNep.HocKy && dg.NamHoc == neNep.NamHoc),
                    Xeploai = (((hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0)) >= 200 ? "Tốt" :
                               (((hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0)) >= 160 ? "Khá" :
                               (((hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0)) >= 120 ? "Trung Bình" :
                               (((hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0)) >= 80 ? "Yếu" : "Kém"))))

                };
            var lop = from s in db.Lops select s;
            var lops = db.Lops.ToList();
            ViewBag.LopList = new SelectList(lops, "TenLop", "TenLop");

            if (!String.IsNullOrEmpty(searchLop))
            {
                totalPointsByClass = totalPointsByClass.Where(s => s.Lop.Contains(searchLop));
            }
            if (!String.IsNullOrEmpty(NamHoc))
            {
                totalPointsByClass = totalPointsByClass.Where(s => s.NamHoc.Contains(NamHoc));
            }
            if (!String.IsNullOrEmpty(HocKy))
            {
                totalPointsByClass = totalPointsByClass.Where(s => s.HocKy.Contains(HocKy));
            }
            if (!String.IsNullOrEmpty(Thang))
            {
                totalPointsByClass = totalPointsByClass.Where(s => s.Thang.Contains(Thang));
            }
            if (!String.IsNullOrEmpty(Tuan))
            {
                totalPointsByClass = totalPointsByClass.Where(s => s.Tuan.Contains(Tuan));
            }
            if (!totalPointsByClass.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }
            //var diemHocTaps = db.DiemHocTaps.Include(d => d.Lop);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(totalPointsByClass.OrderByDescending(h => h.NamHoc).ToPagedList(pageNumber, pageSize));

            //return View(totalPointsByClass.ToList());
        }
        public ActionResult Timkiemvipham(string searchLop, string searchHoTen, string searchNgaySinh, string Tuan, string Thang, string HocKy, string NamHoc, int? page)
        {
            var viPhams = db.ViPhams.Include(v => v.HocSinh);
            var lops = db.Lops.ToList();
            ViewBag.LopList = new SelectList(lops, "TenLop", "TenLop");
            // Lọc theo lớp
            if (!string.IsNullOrEmpty(searchLop))
            {
                viPhams = viPhams.Where(v => v.HocSinh.Lop.TenLop.Contains(searchLop));
            }

            // Lọc theo họ tên
            if (!string.IsNullOrEmpty(searchHoTen))
            {
                viPhams = viPhams.Where(v => v.HocSinh.HoTenHocSinh.Contains(searchHoTen));
            }

            // Lọc theo ngày sinh
            if (!string.IsNullOrEmpty(searchNgaySinh))
            {
                var ngaySinh = DateTime.Parse(searchNgaySinh);
                viPhams = viPhams.Where(v => v.HocSinh.NgaySinh == ngaySinh);
            }
            if (!string.IsNullOrEmpty(Tuan))
            {
                viPhams = viPhams.Where(v => v.TenTuan == Tuan);
            }
            // Lọc theo tháng
            if (!string.IsNullOrEmpty(Thang))
            {
                viPhams = viPhams.Where(v => v.Thang == Thang);
            }

            // Lọc theo học kỳ
            if (!string.IsNullOrEmpty(HocKy))
            {
                viPhams = viPhams.Where(v => v.HocKy == HocKy);
            }

            // Lọc theo năm học
            if (!string.IsNullOrEmpty(NamHoc))
            {
                viPhams = viPhams.Where(v => v.NamHoc == NamHoc);
            }
            if (!viPhams.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }


            // Sắp xếp theo ngày vi phạm
            viPhams = viPhams.OrderBy(v => v.NgayViPham);

            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại (mặc định là trang 1)

            return View(viPhams.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            //ViewBag.IdHocSinh = new SelectList(db.HocSinhs, "IdHocSinh", "HoTenHocSinh");
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

        // POST: ViPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViPham,IdHocSinh,LoaiViPham,MoTaViPham,NgayViPham,TenTuan,DiemTru")] ViPham viPham)
        {

            if (ModelState.IsValid)
            {
                if (viPham.NgayViPham != null)
                {
                    int thang = viPham.NgayViPham.Month;
                    int namHoc = 0;
                    if (thang >= 1 && thang <= 6)
                    {
                        namHoc = viPham.NgayViPham.Year - 1;
                    }
                    else if (thang >= 8 && thang <= 9)
                    {
                        namHoc = viPham.NgayViPham.Year;
                    }
                    else
                    {
                        namHoc = viPham.NgayViPham.Year;
                    }

                    int kyHoc = (thang >= 1 && thang <= 6) ? 2 : 1;

                    viPham.Thang = "" + thang;
                    viPham.NamHoc = namHoc + " - " + (namHoc + 1);


                    viPham.HocKy = "Học kỳ " + kyHoc;
                }
                db.ViPhams.Add(viPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHocSinh = new SelectList(db.HocSinhs, "IdHocSinh", "HoTenHocSinh", viPham.IdHocSinh);
            return View(viPham);
        }
        public ActionResult Chitietvipham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViPham viPham = db.ViPhams.Find(id);
            if (viPham == null)
            {
                return HttpNotFound();
            }
            return View(viPham);
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
                return RedirectToAction("Dangnhap", "Dangnhap");
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
        public ActionResult Lienhe()
        {
            return View();
        }
    }
}