//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HoTroBenhNhan.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuanHuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuanHuyen()
        {
            this.Thongtinbenhnhan = new HashSet<Thongtinbenhnhan>();
            this.XaPhuong = new HashSet<XaPhuong>();
        }
    
        public long ID { get; set; }
        public string tenQuanHuyen { get; set; }
        public long tinhThanhPhoId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Thongtinbenhnhan> Thongtinbenhnhan { get; set; }
        public virtual TinhThanhPho TinhThanhPho { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XaPhuong> XaPhuong { get; set; }
    }
}
