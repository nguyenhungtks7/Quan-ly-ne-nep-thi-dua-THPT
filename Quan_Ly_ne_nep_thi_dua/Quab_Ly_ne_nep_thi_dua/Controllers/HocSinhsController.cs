using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quab_Ly_ne_nep_thi_dua.Models;
using PagedList.Mvc;
using PagedList;
using OfficeOpenXml;

namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class HocSinhsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: HocSinhs
        //public ActionResult Index(string searchString, int? page)
        //{
        //    ViewBag.CurrentFilter = searchString;
        //    var hocSinhs = from s in db.HocSinhs select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        hocSinhs = hocSinhs.Where(s => s.HoTenHocSinh.Contains(searchString));
        //    }

        //    int pageSize = 3; // Kích thước trang
        //    int pageNumber = (page ?? 1); // Trang hiện tại (mặc định là 1)
        //    return View(hocSinhs.OrderBy(h => h.IdHocSinh).ToPagedList(pageNumber, pageSize));
        //}
        public ActionResult Index(string searchLop, string searchHoTen, string searchDiaChi, string searchEmail, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            // Lưu trữ giá trị tìm kiếm hiện tại
            ViewBag.CurrentHoTenFilter = searchHoTen;
            ViewBag.CurrentDiaChiFilter = searchDiaChi;
            ViewBag.CurrentEmailFilter = searchEmail;

 
            var hocSinhs = from s in db.HocSinhs select s;

            var lops = db.Lops.ToList();
            ViewBag.LopList = new SelectList(lops, "TenLop", "TenLop");
            ViewBag.Loplist2 = new SelectList(lops, "IdLop", "TenLop");
            if (!String.IsNullOrEmpty(searchLop))
            {
                hocSinhs = hocSinhs.Where(s => s.Lop.TenLop.Contains(searchLop));
            }

            if (!String.IsNullOrEmpty(searchHoTen))
            {
                hocSinhs = hocSinhs.Where(s => s.HoTenHocSinh.Contains(searchHoTen));
            }

            if (!String.IsNullOrEmpty(searchDiaChi))
            {
                hocSinhs = hocSinhs.Where(s => s.DiaChi.Contains(searchDiaChi));
            }

            if (!String.IsNullOrEmpty(searchEmail))
            {
                hocSinhs = hocSinhs.Where(s => s.Email.Contains(searchEmail));
            }

            if (!hocSinhs.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }
        
            int pageSize = 10; 
            int pageNumber = (page ?? 1); 
            return View(hocSinhs.OrderByDescending(h => h.IdHocSinh).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase file, int? idlop)
        {
            // Code xử lý import từ file Excel vào database ở đây
            using (var package = new ExcelPackage(file.InputStream))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First(); // Lấy worksheet đầu tiên

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Bắt đầu từ hàng thứ 2 để bỏ qua dòng tiêu đề
                {
                    HocSinh hocSinh = new HocSinh();
                    hocSinh.HoTenHocSinh = worksheet.Cells[row, 1].Value.ToString();
                    hocSinh.NgaySinh = DateTime.Parse(worksheet.Cells[row, 2].Value.ToString());
                    hocSinh.GioiTinh = worksheet.Cells[row, 3].Value.ToString();
                    hocSinh.DiaChi = worksheet.Cells[row, 4].Value.ToString();
                    hocSinh.Email = worksheet.Cells[row, 5].Value.ToString();
                    hocSinh.IdLop = idlop;

                    db.HocSinhs.Add(hocSinh);
                }
                TempData["Importthanhcong"] = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSelected(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var idArray = ids.Split(',');
                foreach (var id in idArray)
                {
                    var hocSinh = db.HocSinhs.Find(int.Parse(id));
                    if (hocSinh != null)
                    {
                        db.HocSinhs.Remove(hocSinh);
                    }
                }
                db.SaveChanges();
                TempData["Xoathanhcong"] = true; // Thay đổi sang giá trị boolean true để chỉ ra xóa thành công
            }
            else
            {
                TempData["ErrorMessage"] = "Không có học sinh nào được chọn để xóa.";
            }
            return RedirectToAction("Index");
        }

        // GET: HocSinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

        // POST: HocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHocSinh,HoTenHocSinh,NgaySinh,GioiTinh,DiaChi,Email,IdLop")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                db.HocSinhs.Add(hocSinh);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }

            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", hocSinh.IdLop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", hocSinh.IdLop);
            return View(hocSinh);
        }

        // POST: HocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHocSinh,HoTenHocSinh,NgaySinh,GioiTinh,DiaChi,Email,IdLop")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocSinh).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", hocSinh.IdLop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(hocSinh);
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
    }
}
