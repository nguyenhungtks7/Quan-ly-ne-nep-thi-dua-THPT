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
    public class DiemHocTapsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: DiemHocTaps
        public ActionResult Index(string searchLop, string NamHoc, string HocKy, string Thang, string Tuan, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            var Diem = from s in db.DiemHocTaps select s;
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
            //var diemHocTaps = db.DiemHocTaps.Include(d => d.Lop);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Diem.OrderByDescending(h => h.IdDiemht).ToPagedList(pageNumber, pageSize));
            //return View(diemHocTaps.ToList());
        }

        // GET: DiemHocTaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocTap diemHocTap = db.DiemHocTaps.Find(id);
            if (diemHocTap == null)
            {
                return HttpNotFound();
            }
            return View(diemHocTap);
        }

        // GET: DiemHocTaps/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

        // POST: DiemHocTaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDiemht,IdLop,DiemHocTap1,NgayCapNhat,Tuan,Thang,HocKy,NamHoc,Diemgiotot,Diemgiotb,Diemgioyeu,Diemgiokem,Diemtotsdb,Diemkemsdb")] DiemHocTap diemHocTap)
        {
            if (ModelState.IsValid)
            {
                db.DiemHocTaps.Add(diemHocTap);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }

            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemHocTap.IdLop);
            return View(diemHocTap);
        }

        // GET: DiemHocTaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocTap diemHocTap = db.DiemHocTaps.Find(id);
            if (diemHocTap == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemHocTap.IdLop);
            return View(diemHocTap);
        }

        // POST: DiemHocTaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDiemht,IdLop,DiemHocTap1,NgayCapNhat,Tuan,Thang,HocKy,NamHoc,Diemgiotot,Diemgiotb,Diemgioyeu,Diemgiokem,Diemtotsdb,Diemkemsdb")] DiemHocTap diemHocTap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemHocTap).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", diemHocTap.IdLop);
            return View(diemHocTap);
        }

        // GET: DiemHocTaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocTap diemHocTap = db.DiemHocTaps.Find(id);
            if (diemHocTap == null)
            {
                return HttpNotFound();
            }
            return View(diemHocTap);
        }

        // POST: DiemHocTaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemHocTap diemHocTap = db.DiemHocTaps.Find(id);
            db.DiemHocTaps.Remove(diemHocTap);
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
