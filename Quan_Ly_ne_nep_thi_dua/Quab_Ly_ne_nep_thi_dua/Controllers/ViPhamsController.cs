using PagedList;
using Quab_Ly_ne_nep_thi_dua.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class ViPhamsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: ViPhams
        public ActionResult Index(string searchLop, string searchHoTen, string searchNgaySinh,string Tuan, string Thang, string HocKy, string NamHoc, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
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


        
            viPhams = viPhams.OrderByDescending(v => v.IdViPham);

            int pageSize = 10; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Số trang hiện tại (mặc định là trang 1)

            return View(viPhams.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GetHocSinhsByLop(int idLop)
        {
            var hocSinhs = db.HocSinhs.Where(h => h.IdLop == idLop).Select(h => new
            {
                Value = h.IdHocSinh,
                Text = h.HoTenHocSinh
            }).ToList();
            return Json(hocSinhs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CalculateThangNamHocKy(DateTime selectedDate)
        {
            int thang = selectedDate.Month;
            int namHoc = 0;
            if (thang >= 1 && thang <= 6)
            {
                namHoc = selectedDate.Year - 1;
            }
            else if (thang >= 8 && thang <= 9)
            {
                namHoc = selectedDate.Year;
            }
            else
            {
                namHoc = selectedDate.Year;
            }

            int kyHoc = (thang >= 1 && thang <= 6) ? 2 : 1;

            return Json(new { Thang = thang, NamHoc = namHoc, KyHoc = kyHoc }, JsonRequestBehavior.AllowGet);
        }

        // GET: ViPhams/Details/5
        public ActionResult Details(int? id)
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

        // GET: ViPhams/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

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
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.LopList = new SelectList(db.Lops, "IdLop", "TenLop");
            //ViewBag.LopList = new SelectList(db.Lops, "IdLop", "TenLop");
            ViewBag.IdHocSinh = new SelectList(db.HocSinhs, "IdHocSinh", "HoTenHocSinh", viPham.IdHocSinh);
            return View(viPham);
        }

        // GET: ViPhams/Edit/5
        public ActionResult Edit(int? id)
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
            var hocSinh = db.HocSinhs.Find(viPham.IdHocSinh);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", hocSinh.IdLop);
            ViewBag.IdHocSinh = new SelectList(db.HocSinhs.Where(h => h.IdLop == hocSinh.IdLop), "IdHocSinh", "HoTenHocSinh", viPham.IdHocSinh);
            //ViewBag.IdHocSinh = new SelectList(db.HocSinhs, "IdHocSinh", "HoTenHocSinh", viPham.IdHocSinh);
            return View(viPham);
        }

        // POST: ViPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViPham,IdHocSinh,LoaiViPham,MoTaViPham,NgayViPham,TenTuan,DiemTru")] ViPham viPham)
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
                db.Entry(viPham).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.LopList = new SelectList(db.Lops, "IdLop", "TenLop");
            ViewBag.IdHocSinh = new SelectList(db.HocSinhs, "IdHocSinh", "HoTenHocSinh", viPham.IdHocSinh);
            return View(viPham);
        }

        // GET: ViPhams/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: ViPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViPham viPham = db.ViPhams.Find(id);
            db.ViPhams.Remove(viPham);
            db.SaveChanges();
            TempData["Xoathanhcong"] = true;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

    }
}
