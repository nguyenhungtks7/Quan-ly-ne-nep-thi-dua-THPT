using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quab_Ly_ne_nep_thi_dua.Models
{
    public class XepLoaiDanhGiaTongDiemcualop
    {
        public int idLop { get; set; }
        public string Lop { get; set; }
        public decimal? DiemHocTap { get; set; }
        public decimal? DiemNeNep { get; set; }
        public decimal TotalPoints { get; set; }
        public string Tuan { get; set; }
        public string Thang { get; set; }
        public string HocKy { get; set; }
        public string NamHoc { get; set; }
        public string Xeploai { get; set; }
        public bool IsSaved { get; set; }
    }
}