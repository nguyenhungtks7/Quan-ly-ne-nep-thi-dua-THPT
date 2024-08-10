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
namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class GVCNsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: GVCNs
        public ActionResult Index(string searchHoTen, string searchSDT, string searchEmail, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            ViewBag.CurrentHoTenFilter = searchHoTen;
            ViewBag.CurrentSDTFilter = searchSDT;
            ViewBag.CurrentEmailFilter = searchEmail;


            var GVCN = from s in db.GVCNs select s;

            if (!String.IsNullOrEmpty(searchHoTen))
            {
                GVCN = GVCN.Where(s => s.HoTenGVCN.Contains(searchHoTen));
            }

            if (!String.IsNullOrEmpty(searchSDT))
            {
                GVCN = GVCN.Where(s => s.SoDienThoai.Contains(searchSDT));
            }

            if (!String.IsNullOrEmpty(searchEmail))
            {
                GVCN = GVCN.Where(s => s.Email.Contains(searchEmail));
            }

            if (!GVCN.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(GVCN.OrderByDescending(h => h.IdGVCN).ToPagedList(pageNumber, pageSize));
        }

        // GET: GVCNs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GVCN gVCN = db.GVCNs.Find(id);
            if (gVCN == null)
            {
                return HttpNotFound();
            }
            return View(gVCN);
        }

        // GET: GVCNs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GVCNs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGVCN,HoTenGVCN,NgaySinh,SoDienThoai,Email,GioiTinh")] GVCN gVCN)
        {
            if (ModelState.IsValid)
            {
                db.GVCNs.Add(gVCN);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;

                return RedirectToAction("Index");
            }

            return View(gVCN);
        }

        // GET: GVCNs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GVCN gVCN = db.GVCNs.Find(id);
            if (gVCN == null)
            {
                return HttpNotFound();
            }
            return View(gVCN);
        }

        // POST: GVCNs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGVCN,HoTenGVCN,NgaySinh,SoDienThoai,Email,GioiTinh")] GVCN gVCN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gVCN).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }
            return View(gVCN);
        }

        // GET: GVCNs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GVCN gVCN = db.GVCNs.Find(id);
            if (gVCN == null)
            {
                return HttpNotFound();
            }
            return View(gVCN);
        }

        // POST: GVCNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GVCN gVCN = db.GVCNs.Find(id);
            db.GVCNs.Remove(gVCN);
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
