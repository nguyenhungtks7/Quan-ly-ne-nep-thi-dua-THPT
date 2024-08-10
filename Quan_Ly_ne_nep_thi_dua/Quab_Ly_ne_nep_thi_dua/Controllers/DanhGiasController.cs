using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quab_Ly_ne_nep_thi_dua.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Web.UI;
using PagedList;

namespace Quab_Ly_ne_nep_thi_dua.Controllers
{
    public class DanhGiasController : Controller
    {
        private QL_nenepthiduaEntities db = new QL_nenepthiduaEntities();

        // GET: DanhGias
        //public ActionResult Index()
        //{
        //    var danhGias = db.DanhGias.Include(d => d.Lop);
        //    return View(danhGias.ToList());
        //}

        //var totalPointsByClass = db.DiemNeNeps
        //    .Join(db.DiemHocTaps,
        //        neNep => neNep.IdLop,
        //        hocTap => hocTap.IdLop,
        //        (neNep, hocTap) => new XepLoaiDanhGiaTongDiemcualop
        //        {
        //            idLop =(int) neNep.IdLop,
        //            Lop = neNep.Lop.TenLop,
        //            DiemHocTap = hocTap.DiemHocTap1,
        //            DiemNeNep = neNep.DiemNeNep1,
        //            TotalPoints = (hocTap.DiemHocTap1 ?? 0) + (neNep.DiemNeNep1 ?? 0),
        //            Tuan = neNep.Tuan,
        //            Thang = neNep.Thang,
        //            HocKy = neNep.HocKy,
        //            NamHoc = neNep.NamHoc
        //        })
        //    .ToList();

        //return View(totalPointsByClass);
        public ActionResult Index(string searchLop, string NamHoc, string HocKy, string Thang, string Tuan, int? page)
        {
            if (Session["Tendangnhap"] == null)
            {
                return RedirectToAction("Dangnhap", "Dangnhap");
            }
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
            return View(totalPointsByClass.OrderByDescending(s => s.NamHoc)
                                       .ThenByDescending(s => s.HocKy)
                                       .ThenByDescending(s => s.Thang)
                                       .ThenByDescending(s => s.Tuan).ToPagedList(pageNumber, pageSize));

            //return View(totalPointsByClass.ToList());
        }
        public ActionResult Thongkenam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Thongkenam(string namHoc)
        {
            var result = db.DanhGias
            .Where(d => d.NamHoc == namHoc)
               .GroupBy(d => d.IdLop)
              .Select(g => new ThongKeNamViewModel
              {
                  TenLop = g.FirstOrDefault().Lop.TenLop,
                  TongDiem = (decimal)g.Sum(d => d.TongDiem)
              })
                 .OrderByDescending(x => x.TongDiem)
                 .ToList();

            // Đánh rank cho từng lớp dựa trên tổng điểm
            int rank = 1;
            foreach (var item in result)
            {
                item.Rank = rank;
                rank++;
            }

            ViewBag.NamHoc = namHoc;
            return View(result);
        }
        public ActionResult ExportToExcel(string namHoc)
        {
            var result = db.DanhGias
                .Where(d => d.NamHoc == namHoc)
                .GroupBy(d => d.IdLop)
                .Select(g => new ThongKeNamViewModel
                {
                    TenLop = g.FirstOrDefault().Lop.TenLop,
                    TongDiem = (decimal)g.Sum(d => d.TongDiem)
                })
                .OrderByDescending(x => x.TongDiem)
                .ToList();

            // Tạo một đối tượng ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Tạo một Worksheet với tên "ThongKeNam"
                var worksheet = package.Workbook.Worksheets.Add("ThongKeNam");
                //var table = worksheet.Cells["A1:C1"];
                //table.Style.Font.Bold = true;
                // Gán giá trị cho các ô tiêu đề
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["B1"].Value = "Tên Lớp";
                worksheet.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["C1"].Value = "Tổng Điểm";
                worksheet.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //var headerRange = worksheet.Cells[1, 1, 1, 3];
                var headerRange = worksheet.Cells["A1:C1"];
                headerRange.Style.Font.Bold = true; // Chữ in đậm
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                int rowIndex = 2;
                foreach (var item in result)
                {
                    worksheet.Cells[rowIndex, 1].Value = rowIndex - 1;
                    worksheet.Cells[rowIndex, 2].Value = item.TenLop;
                    worksheet.Cells[rowIndex, 3].Value = item.TongDiem;

                    // Thêm đường viền cho mỗi ô
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    rowIndex++;
                }

                // Tự động điều chỉnh cỡ của các cột để vừa với dữ liệu
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Trả về file Excel
                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;
                return File(fileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TỔNG HỢP THI ĐUA CÁC CHI ĐOÀN HỌC SINH NĂM HỌC " + namHoc + ".xlsx");
            }
        }
        public ActionResult Thongkeki()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Thongkeki(string namHoc, string kiHoc)
        {
            // Kiểm tra kiểu dữ liệu của biến kiHoc (ví dụ: kiHoc có thể là string hoặc enum)

            var result = db.DanhGias
                           .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc)
                           .GroupBy(d => d.IdLop)
                           .Select(g => new ThongKeKiViewModel
                           {
                               TenLop = g.FirstOrDefault().Lop.TenLop,
                               TongDiem = (decimal)g.Sum(d => d.TongDiem)
                           })
                           .OrderByDescending(x => x.TongDiem)
                           .ToList();

            int rank = 1;
            foreach (var item in result)
            {
                item.Rank = rank;
                rank++;
            }

            ViewBag.NamHoc = namHoc;
            ViewBag.KiHoc = kiHoc;
            return View(result);
        }

        public ActionResult ExportToExcelki(string namHoc, string kiHoc)
        {
            var result = db.DanhGias
                .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc) // Lọc dữ liệu theo cả năm học và kì học
                .GroupBy(d => d.IdLop)
                .Select(g => new ThongKeNamViewModel
                {
                    TenLop = g.FirstOrDefault().Lop.TenLop,
                    TongDiem = (decimal)g.Sum(d => d.TongDiem)
                })
                .OrderByDescending(x => x.TongDiem)
                .ToList();

            // Tạo một đối tượng ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Tạo một Worksheet với tên "ThongKeNam"
                var worksheet = package.Workbook.Worksheets.Add("ThongKeNam");
                //var table = worksheet.Cells["A1:C1"];
                //table.Style.Font.Bold = true;
                // Gán giá trị cho các ô tiêu đề
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["B1"].Value = "Tên Lớp";
                worksheet.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["C1"].Value = "Tổng Điểm";
                worksheet.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //var headerRange = worksheet.Cells[1, 1, 1, 3];
                var headerRange = worksheet.Cells["A1:C1"];
                headerRange.Style.Font.Bold = true; // Chữ in đậm
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                int rowIndex = 2;
                foreach (var item in result)
                {
                    worksheet.Cells[rowIndex, 1].Value = rowIndex - 1;
                    worksheet.Cells[rowIndex, 2].Value = item.TenLop;
                    worksheet.Cells[rowIndex, 3].Value = item.TongDiem;

                    // Thêm đường viền cho mỗi ô
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    rowIndex++;
                }

                // Tự động điều chỉnh cỡ của các cột để vừa với dữ liệu
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Trả về file Excel
                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;
                return File(fileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TỔNG HỢP THI ĐUA CÁC CHI ĐOÀN HỌC SINH NĂM HỌC " + namHoc + " - " + kiHoc + ".xlsx");
            }
        }
        public ActionResult ThongkeThang()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ThongkeThang(string namHoc, string kiHoc, string Thang)
        {

            var result = db.DanhGias
                           .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc && d.Thang == Thang)
                           .GroupBy(d => d.IdLop)
                           .Select(g => new ThongKeThangViewModel
                           {
                               TenLop = g.FirstOrDefault().Lop.TenLop,
                               TongDiem = (decimal)g.Sum(d => d.TongDiem)
                           })
                           .OrderByDescending(x => x.TongDiem)
                           .ToList();

            int rank = 1;
            foreach (var item in result)
            {
                item.Rank = rank;
                rank++;
            }

            ViewBag.NamHoc = namHoc;
            ViewBag.KiHoc = kiHoc;
            ViewBag.Thang = Thang;
            return View(result);
        }
        public ActionResult ExportToExcelThang(string namHoc, string kiHoc, string Thang)
        {
            var result = db.DanhGias
                       .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc && d.Thang == Thang)
                       .GroupBy(d => d.IdLop)
                       .Select(g => new ThongKeThangViewModel
                       {
                           TenLop = g.FirstOrDefault().Lop.TenLop,
                           TongDiem = (decimal)g.Sum(d => d.TongDiem)
                       })
                       .OrderByDescending(x => x.TongDiem)
                       .ToList();

            // Tạo một đối tượng ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Tạo một Worksheet với tên "ThongKeNam"
                var worksheet = package.Workbook.Worksheets.Add("ThongKeNam");
                //var table = worksheet.Cells["A1:C1"];
                //table.Style.Font.Bold = true;
                // Gán giá trị cho các ô tiêu đề
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["B1"].Value = "Tên Lớp";
                worksheet.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["C1"].Value = "Tổng Điểm";
                worksheet.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //var headerRange = worksheet.Cells[1, 1, 1, 3];
                var headerRange = worksheet.Cells["A1:C1"];
                headerRange.Style.Font.Bold = true; // Chữ in đậm
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                int rowIndex = 2;
                foreach (var item in result)
                {
                    worksheet.Cells[rowIndex, 1].Value = rowIndex - 1;
                    worksheet.Cells[rowIndex, 2].Value = item.TenLop;
                    worksheet.Cells[rowIndex, 3].Value = item.TongDiem;

                    // Thêm đường viền cho mỗi ô
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    rowIndex++;
                }

                // Tự động điều chỉnh cỡ của các cột để vừa với dữ liệu
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Trả về file Excel
                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;
                return File(fileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TỔNG HỢP THI ĐUA CÁC CHI ĐOÀN HỌC SINH NĂM HỌC " + namHoc + " - " + kiHoc + " - Tháng" + Thang + ".xlsx");
            }
        }
        public ActionResult ThongkeTuan()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThongkeTuan(string namHoc, string kiHoc, string Thang, string Tuan)
        {
            var result = db.DanhGias
                            .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc && d.Thang == Thang && d.Tuan == Tuan)
                            .GroupBy(d => d.IdLop)
                            .Select(g => new ThongKeTuanViewModel
                            {
                                TenLop = g.FirstOrDefault().Lop.TenLop,
                                TongDiem = (decimal)g.Sum(d => d.TongDiem),
                                GVCN = g.FirstOrDefault().Lop.GVCN.HoTenGVCN,
                            
                            })
                            .OrderByDescending(x => x.TongDiem)
                            .ToList();

            int rank = 1;
            foreach (var item in result)
            {
                item.Rank = rank;
                item.XepLoai = XepLoai(item.TongDiem);
                rank++;
            }

            ViewBag.NamHoc = namHoc;
            ViewBag.KiHoc = kiHoc;
            ViewBag.Thang = Thang;
            ViewBag.Tuan = Tuan;
            return View(result);
        }
        private string XepLoai(decimal tongDiem)
        {
            if (tongDiem >= 200)
                return "Tốt";
            else if (tongDiem >= 160)
                return "Khá";
            else if (tongDiem >= 120)
                return "Trung Bình";
            else if (tongDiem >= 80)
                return "Yếu";
            else
                return "Kém";
        }
        public ActionResult ExportToExcelTuan(string namHoc, string kiHoc, string thang, string tuan)
        {
            var result = db.DanhGias
                        .Where(d => d.NamHoc == namHoc && d.HocKy == kiHoc && d.Thang == thang && d.Tuan == tuan)
                        .GroupBy(d => d.IdLop)
                        .Select(g => new ThongKeTuanViewModel
                        {
                            TenLop = g.FirstOrDefault().Lop.TenLop,
                            TongDiem = (decimal)g.Sum(d => d.TongDiem),
                            GVCN = g.FirstOrDefault().Lop.GVCN.HoTenGVCN,
                            XepLoai = ((decimal)g.Sum(d => d.TongDiem)) >= 200 ? "Tốt" :
                               ((decimal)g.Sum(d => d.TongDiem)) >= 160 ? "Khá" :
                               (((decimal)g.Sum(d => d.TongDiem))) >= 120 ? "Trung Bình" :
                               ((decimal)g.Sum(d => d.TongDiem)) >= 80 ? "Yếu" : "Kém"
                            
                        })
                        .OrderByDescending(x => x.TongDiem)
                        .ToList();

            // Tạo một đối tượng ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Tạo một Worksheet với tên "ThongKeTuan"
                var worksheet = package.Workbook.Worksheets.Add("ThongKeTuan");
                var headerRange = worksheet.Cells["A1:E1"];
                headerRange.Style.Font.Bold = true; // Chữ in đậm
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // Gán giá trị cho các ô tiêu đề
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["B1"].Value = "Tên Lớp";
                worksheet.Cells["B1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["C1"].Value = "Tổng Điểm";
                worksheet.Cells["C1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["D1"].Value = "GVCN";
                worksheet.Cells["D1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["E1"].Value = "Xếp loại";
                worksheet.Cells["E1"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                int rowIndex = 2;
                foreach (var item in result)
                {
                    worksheet.Cells[rowIndex, 1].Value = rowIndex - 1;
                    worksheet.Cells[rowIndex, 2].Value = item.TenLop;
                    worksheet.Cells[rowIndex, 3].Value = item.TongDiem;
                    worksheet.Cells[rowIndex, 4].Value = item.GVCN;
                    worksheet.Cells[rowIndex, 5].Value = item.XepLoai;

                    // Thêm đường viền cho mỗi ô
                    worksheet.Cells[rowIndex, 1, rowIndex, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[rowIndex, 1, rowIndex, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    rowIndex++;
                }

                // Tự động điều chỉnh cỡ của các cột để vừa với dữ liệu
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Trả về file Excel
                var fileStream = new MemoryStream();
                package.SaveAs(fileStream);
                fileStream.Position = 0;
                return File(fileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TỔNG HỢP THI ĐUA CÁC CHI ĐOÀN HỌC SINH NĂM HỌC " + namHoc + " - " + kiHoc + " - Tháng " + thang + " - Tuần " + tuan + ".xlsx");
            }
        }

        //public ActionResult ExportToExcel(string namHoc)
        //{
        //    var result = db.DanhGias
        //        .Where(d => d.NamHoc == namHoc)
        //        .GroupBy(d => d.IdLop)
        //        .Select(g => new ThongKeNamViewModel
        //        {
        //            TenLop = g.FirstOrDefault().Lop.TenLop,
        //            TongDiem = (decimal)g.Sum(d => d.TongDiem)
        //        })
        //        .OrderByDescending(x => x.TongDiem)
        //        .ToList();

        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("ThongKeNam");

        //        worksheet.Cells["A1"].Value = "STT";
        //        worksheet.Cells["B1"].Value = "Tên Lớp";
        //        worksheet.Cells["C1"].Value = "Tổng Điểm";

        //        int rowIndex = 2;
        //        foreach (var item in result)
        //        {
        //            worksheet.Cells[rowIndex, 1].Value = rowIndex - 1;
        //            worksheet.Cells[rowIndex, 2].Value = item.TenLop;
        //            worksheet.Cells[rowIndex, 3].Value = item.TongDiem;
        //            rowIndex++;
        //        }

        //        // Trả về file Excel
        //        var fileStream = new MemoryStream();
        //        package.SaveAs(fileStream);
        //        fileStream.Position = 0;
        //        return File(fileStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TỔNG HỢP THI ĐUA CÁC CHI ĐOÀN HỌC SINH NĂM HỌC "+ namHoc + ".xlsx");
        //    }
        //}


        [HttpPost]
        public ActionResult LuuDanhGia(int idLop, decimal totalPoints, string xepLoai, string tuan, string thang, string hocKy, string namHoc)
        {
            // Ở đây, bạn có thể thực hiện các thao tác để lưu dữ liệu vào cơ sở dữ liệu hoặc thực hiện các công việc khác cần thiết.

            // Ví dụ:
            DanhGia danhGia = new DanhGia(); // Tạo một đối tượng danhGia nếu cần
            danhGia.IdLop = idLop;
            danhGia.TongDiem = totalPoints;
            danhGia.XepLoai = xepLoai;
            danhGia.Tuan = tuan;
            danhGia.Thang = thang;
            danhGia.HocKy = hocKy;
            danhGia.NamHoc = namHoc;

            // Lưu dữ liệu vào cơ sở dữ liệu
            db.DanhGias.Add(danhGia);
            db.SaveChanges();
            // Trả về kết quả cho client (có thể là một JSON hoặc view tùy theo nhu cầu của bạn)
            return RedirectToAction("Index");
            //return Json(new { success = true, message = "Dữ liệu đã được lưu thành công!" });
        }

        [HttpPost]
        public ActionResult Capnhap(int idLop, decimal totalPoints, string xepLoai, string tuan, string thang, string hocKy, string namHoc)
        {
            // Ở đây, bạn có thể thực hiện các thao tác để lưu dữ liệu vào cơ sở dữ liệu hoặc thực hiện các công việc khác cần thiết.

            // Ví dụ:
            DanhGia danhGia = new DanhGia(); // Tạo một đối tượng danhGia nếu cần
            danhGia.IdLop = idLop;
            danhGia.TongDiem = totalPoints;
            danhGia.XepLoai = xepLoai;
            danhGia.Tuan = tuan;
            danhGia.Thang = thang;
            danhGia.HocKy = hocKy;
            danhGia.NamHoc = namHoc;

            // Lưu dữ liệu vào cơ sở dữ liệu
            db.Entry(danhGia).State = EntityState.Modified;
            db.SaveChanges();
            // Trả về kết quả cho client (có thể là một JSON hoặc view tùy theo nhu cầu của bạn)
            return RedirectToAction("Index");
            //return Json(new { success = true, message = "Dữ liệu đã được lưu thành công!" });
        }


        //private bool CheckIfDataIsSaved(string tuan, string thang, string hocKy, string namHoc)
        //{

        //        var existingData = db.DanhGias
        //            .Where(x => x.Tuan == tuan && x.Thang == thang && x.HocKy == hocKy && x.NamHoc == namHoc)
        //            .FirstOrDefault();


        //        if (existingData != null)
        //        {
        //            return true;
        //        }
        //        return false;

        //}
        // GET: DanhGias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // GET: DanhGias/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop");
            return View();
        }

        // POST: DanhGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDanhGia,IdLop,TongDiem,ThuTu,XepLoai")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.DanhGias.Add(danhGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", danhGia.IdLop);
            return View(danhGia);
        }

        // GET: DanhGias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", danhGia.IdLop);
            return View(danhGia);
        }

        // POST: DanhGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDanhGia,IdLop,TongDiem,ThuTu,XepLoai")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLop = new SelectList(db.Lops, "IdLop", "TenLop", danhGia.IdLop);
            return View(danhGia);
        }

        // GET: DanhGias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // POST: DanhGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhGia danhGia = db.DanhGias.Find(id);
            db.DanhGias.Remove(danhGia);
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
