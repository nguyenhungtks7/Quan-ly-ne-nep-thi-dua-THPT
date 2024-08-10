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
    public class LopsController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: Lops
        public ActionResult Index(string searchLop, string searchnamhoc, string searchhotengvcn, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
            ViewBag.CurrentnamhocFilter = searchnamhoc;
            ViewBag.CurrenthotengvcnFilter = searchhotengvcn;


            var lop = from s in db.Lops select s;
            var lops = db.Lops.ToList();
            ViewBag.LopList = new SelectList(lops, "TenLop", "TenLop");

            if (!String.IsNullOrEmpty(searchLop))
            {
                lop = lop.Where(s => s.TenLop.Contains(searchLop));
            }
            if (!String.IsNullOrEmpty(searchnamhoc))
            {
                lop = lop.Where(s => s.NamHoc.Contains(searchnamhoc));
            }

            if (!String.IsNullOrEmpty(searchhotengvcn))
            {
                lop = lop.Where(s => s.GVCN.HoTenGVCN.Contains(searchhotengvcn));
            }

            if (!lop.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(lop.OrderByDescending(h => h.IdLop).ToPagedList(pageNumber, pageSize));
        }

        // GET: Lops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Lops/Create
        public ActionResult Create()
        {
            var teachers = db.GVCNs.Select(g => new {
                Id = g.IdGVCN,
                FullName = g.IdGVCN + " - " + g.HoTenGVCN
            }).ToList();

            ViewBag.IdGVCN = new SelectList(teachers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLop,TenLop,NamHoc,IdGVCN")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên lớp đã tồn tại trong cơ sở dữ liệu chưa
                var existingClass = db.Lops.FirstOrDefault(x => x.TenLop == lop.TenLop && x.NamHoc == lop.NamHoc);

                if (existingClass != null)
                {
                    ModelState.AddModelError("TenLop", "Tên lớp đã tồn tại. Vui lòng chọn tên lớp khác.");
                    var teachers2 = db.GVCNs.Select(g => new {
                        Id = g.IdGVCN,
                        FullName = g.IdGVCN + " - " + g.HoTenGVCN
                    }).ToList();

                    ViewBag.IdGVCN = new SelectList(teachers2, "Id", "FullName", lop.IdGVCN);
                    return View(lop);
                }
                var existingTeacher = db.Lops.FirstOrDefault(x => x.IdGVCN == lop.IdGVCN);

                if (existingTeacher != null)
                {
                    ModelState.AddModelError("IdGVCN", "Giáo viên chủ nhiệm này đã được chỉ định cho lớp khác. Vui lòng chọn giáo viên khác.");
                    var teachers3 = db.GVCNs.Select(g => new {
                        Id = g.IdGVCN,
                        FullName = g.IdGVCN + " - " + g.HoTenGVCN
                    }).ToList();

                    ViewBag.IdGVCN = new SelectList(teachers3, "Id", "FullName", lop.IdGVCN);
                    return View(lop);
                }
                // Nếu tên lớp chưa tồn tại, thêm lớp mới vào cơ sở dữ liệu
                db.Lops.Add(lop);
                db.SaveChanges();
                TempData["Themthanhcong"] = true;
                return RedirectToAction("Index");
            }

            var teachers = db.GVCNs.Select(g => new {
                Id = g.IdGVCN,
                FullName = g.IdGVCN + " - " + g.HoTenGVCN
            }).ToList();

            ViewBag.IdGVCN = new SelectList(teachers, "Id", "FullName", lop.IdGVCN);
            return View(lop);
        }
        //public ActionResult Create()
        //{
        //    ViewBag.IdGVCN = new SelectList(db.GVCNs, "IdGVCN", "HoTenGVCN");
        //    return View();
        //}

        //// POST: Lops/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdLop,TenLop,NamHoc,IdGVCN")] Lop lop)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Lops.Add(lop);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IdGVCN = new SelectList(db.GVCNs, "IdGVCN", "HoTenGVCN" , lop.IdGVCN);
        //    return View(lop);
        //}

        // GET: Lops/Edit/5'
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lop lop = db.Lops.Find(id);

            if (lop == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách giáo viên từ cơ sở dữ liệu
            var teachers = db.GVCNs.Select(g => new {
                Id = g.IdGVCN,
                FullName = g.IdGVCN  + " - " + g.HoTenGVCN
            }).ToList();

            // Tạo SelectList cho dropdown list giáo viên chủ nhiệm với giáo viên đã được chọn mặc định
            ViewBag.IdGVCN = new SelectList(teachers, "Id", "FullName", lop.IdGVCN);

            return View(lop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLop,TenLop,NamHoc,IdGVCN")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên lớp đã tồn tại trong cơ sở dữ liệu chưa (nếu lớp đã thay đổi tên)
                var existingClass = db.Lops.FirstOrDefault(x => x.TenLop == lop.TenLop && x.IdLop != lop.IdLop);

                if (existingClass != null)
                {
                    ModelState.AddModelError("TenLop", "Tên lớp đã tồn tại. Vui lòng chọn tên lớp khác.");
                    var teachers2 = db.GVCNs.Select(g => new {
                        Id = g.IdGVCN,
                        FullName = g.IdGVCN + " - " + g.HoTenGVCN
                    }).ToList();

                    ViewBag.IdGVCN = new SelectList(teachers2, "Id", "FullName", lop.IdGVCN);
                    return View(lop);
                }

                // Kiểm tra xem giáo viên chủ nhiệm đã được chỉ định cho một lớp khác chưa
                var existingTeacher = db.Lops.FirstOrDefault(x => x.IdGVCN == lop.IdGVCN && x.IdLop != lop.IdLop);

                if (existingTeacher != null)
                {
                    ModelState.AddModelError("IdGVCN", "Giáo viên chủ nhiệm này đã được chỉ định cho lớp khác. Vui lòng chọn giáo viên khác.");
                    var teachers3 = db.GVCNs.Select(g => new {
                        Id = g.IdGVCN,
                        FullName = g.IdGVCN + " - " + g.HoTenGVCN
                    }).ToList();

                    ViewBag.IdGVCN = new SelectList(teachers3, "Id", "FullName", lop.IdGVCN);
                    return View(lop);
                }

                // Cập nhật lớp vào cơ sở dữ liệu
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Suathanhcong"] = true;
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, trả về view với thông tin và lỗi tương ứng
            var teachers = db.GVCNs.Select(g => new {
                Id = g.IdGVCN,
                FullName = g.IdGVCN + " - " + g.HoTenGVCN
            }).ToList();

            ViewBag.IdGVCN = new SelectList(teachers, "Id", "FullName", lop.IdGVCN);
            return View(lop);
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lop lop = db.Lops.Find(id);
        //    if (lop == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IdGVCN = new SelectList(db.GVCNs, "IdGVCN", "HoTenGVCN", lop.IdGVCN);
        //    return View(lop);
        //}

        //// POST: Lops/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdLop,TenLop,NamHoc,IdGVCN")] Lop lop)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lop).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdGVCN = new SelectList(db.GVCNs, "IdGVCN", "HoTenGVCN", lop.IdGVCN);
        //    return View(lop);
        //}

        // GET: Lops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lop lop = db.Lops.Find(id);
            db.Lops.Remove(lop);
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
