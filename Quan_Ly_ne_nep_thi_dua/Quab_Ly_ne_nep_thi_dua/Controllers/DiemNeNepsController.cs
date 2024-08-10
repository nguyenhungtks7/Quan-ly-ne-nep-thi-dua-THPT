using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Quab_Ly_ne_nep_thi_dua.Models;

namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class DiemNeNepsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: DiemNeNeps
        public ActionResult Index(string searchLop, string NamHoc, string HocKy, string Thang, string Tuan, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            var Diem = from s in db.DiemNeNeps select s;
            var lop = from s in db.Lops select s;
            var lops = db.Lops.ToList();
            ViewBag.LopList = new SelectList(lops, "TenLop", "TenLop");

            if (!String.IsNullOrEmpty(searchLop))
            {
                Diem = Diem.Where(s => s.Lop.TenLop.Contains(searchLop));
            }
            if (!String.IsNullOrEmpty(NamHoc))
            {
                Diem = Diem.Where(s => s.NamHoc.Contains(NamHoc));
            }
            if (!String.IsNullOrEmpty(HocKy))
            {
                Diem = Diem.Where(s => s.HocKy.Contains(HocKy));
            }
            if (!String.IsNullOrEmpty(Thang))
            {
                Diem = Diem.Where(s => s.Thang.Contains(Thang));
            }
            if (!String.IsNullOrEmpty(Tuan))
            {
                Diem = Diem.Where(s => s.Tuan.Contains(Tuan));
            }
            if (!Diem.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }
         
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Diem.OrderByDescending(h => h.IdDiemnn).ToPagedList(pageNumber, pageSize));
           
        }

        // GET: DiemNeNeps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemNeNep diemNeNep = db.DiemNeNeps.Find(id);
            if (diemNeNep == null)
            {
                return HttpNotFound();
            }
            return View(diemNeNep);
        }
        public ActionResult CalculateDiemNeNep(string tuan, string thang, string hocky, string namhoc, int lop)
        {
            //ví dụ data truyền vào
            //tuan
            // thang 8
            // hocky 1
            // namhoc 2023 - 2024
            var hocSinhsInClassIds = db.HocSinhs.Where(h => h.IdLop == lop).Select(h => h.IdHocSinh).ToList();

            // Nếu không có học sinh trong lớp
            if (hocSinhsInClassIds.Count == 0)
            {
                return Json(new { ErrorMessage = "Không có học sinh trong lớp này!" }, JsonRequestBehavior.AllowGet);
            }

            // Tính tổng điểm trừ của lớp trong tuần, tháng, học kỳ và năm cụ thể
            var tongDiemTru = db.ViPhams.Where(v => v.TenTuan == tuan && v.Thang == thang && v.HocKy == hocky && v.NamHoc == namhoc && hocSinhsInClassIds.Contains(v.IdHocSinh.Value))
                                         .Sum(v => v.DiemTru);
            if(tongDiemTru == null)
            {
                tongDiemTru = 0;
            }
            return Json(new { TongDiemTru = 100 + tongDiemTru }, JsonRequestBehavior.AllowGet);
        }

        // GET: DiemNeNeps/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

        // POST: DiemNeNeps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDiemnn,IdLop,DiemNeNep1,NgayCapNhat,Tuan,Thang,HocKy,NamHoc")] DiemNeNep diemNeNep)
        {
            if (ModelState.IsValid)
            {
                db.DiemNeNeps.Add(diemNeNep);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }

            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemNeNep.IdLop);
            return View(diemNeNep);
        }

        // GET: DiemNeNeps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemNeNep diemNeNep = db.DiemNeNeps.Find(id);
            if (diemNeNep == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemNeNep.IdLop);
            return View(diemNeNep);
        }

        // POST: DiemNeNeps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDiemnn,IdLop,DiemNeNep1,NgayCapNhat,Tuan,Thang,HocKy,NamHoc")] DiemNeNep diemNeNep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemNeNep).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemNeNep.IdLop);
            return View(diemNeNep);
        }

        // GET: DiemNeNeps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemNeNep diemNeNep = db.DiemNeNeps.Find(id);
            if (diemNeNep == null)
            {
                return HttpNotFound();
            }
            return View(diemNeNep);
        }

        // POST: DiemNeNeps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemNeNep diemNeNep = db.DiemNeNeps.Find(id);
            db.DiemNeNeps.Remove(diemNeNep);
            TempData["Xoathanhcong"] = true;
            db.SaveChanges();
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
