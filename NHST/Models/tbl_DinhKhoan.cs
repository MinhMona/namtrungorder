//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_DinhKhoan
    {
        public int ID { get; set; }
        public string MaDinhKhoan { get; set; }
        public Nullable<int> LoaiGiaoDich { get; set; }
        public Nullable<int> DinhKhoanCha { get; set; }
        public string TenDinhKhoan { get; set; }
        public Nullable<int> TrangThai { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
