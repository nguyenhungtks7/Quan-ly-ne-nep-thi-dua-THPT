﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quab_Ly_ne_nep_thi_dua.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DiemHocTap
    {
        [Key]
        public int IdDiemht { get; set; }

        [Display(Name = "ID Lớp")]
        public Nullable<int> IdLop { get; set; }

        [Display(Name = "Điểm Học Tập")]
        public Nullable<decimal> DiemHocTap1 { get; set; }

        [Display(Name = "Ngày Cập Nhật")]
        public Nullable<System.DateTime> NgayCapNhat { get; set; }

        [Display(Name = "Tuần")]
        [Required(ErrorMessage = "Vui lòng chọn tuần.")]
        public string Tuan { get; set; }

        [Display(Name = "Tháng")]
        [Required(ErrorMessage = "Vui lòng chọn tháng.")]
        public string Thang { get; set; }

        [Display(Name = "Học Kỳ")]
        [Required(ErrorMessage = "Vui lòng chọn học kỳ.")]
        public string HocKy { get; set; }

        [Display(Name = "Năm Học")]
        [Required(ErrorMessage = "Vui lòng chọn năm học.")]
        public string NamHoc { get; set; }

        [Display(Name = "Điểm Giờ Tốt")]
        public Nullable<decimal> Diemgiotot { get; set; }

        [Display(Name = "Điểm Giờ Trung Bình")]
        public Nullable<decimal> Diemgiotb { get; set; }

        [Display(Name = "Điểm Giờ Yếu")]
        public Nullable<decimal> Diemgioyeu { get; set; }

        [Display(Name = "Điểm Giờ Kém")]
        public Nullable<decimal> Diemgiokem { get; set; }

        [Display(Name = "Điểm tốt ghi trong sổ đầu bài (8, 9, 10) ")]
        public Nullable<decimal> Diemtotsdb { get; set; }

        [Display(Name = "Điểm kém ghi trong sổ đầu bài ( điểm <5)")]
        public Nullable<decimal> Diemkemsdb { get; set; }


        public virtual Lop Lop { get; set; }
    }
}
