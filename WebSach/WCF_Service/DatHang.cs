//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class DatHang
    {
        public DatHang()
        {
            this.DatHangCTs = new HashSet<DatHangCT>();
        }
    
        public int DatHangID { get; set; }
        public Nullable<int> KhachHangID { get; set; }
        public System.DateTime NgayDatHang { get; set; }
        public Nullable<double> TriGia { get; set; }
        public bool DaGiao { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public string DiaDiem { get; set; }
        public string TenKhachHang { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<DatHangCT> DatHangCTs { get; set; }
    }
}
