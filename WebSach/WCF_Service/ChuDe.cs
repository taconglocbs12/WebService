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
    
    public partial class ChuDe
    {
        public ChuDe()
        {
            this.Saches = new HashSet<Sach>();
        }
    
        public int ChuDeID { get; set; }
        public string TenChuDe { get; set; }
    
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
