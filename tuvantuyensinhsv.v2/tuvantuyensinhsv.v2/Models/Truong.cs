//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tuvantuyensinhsv.v2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Truong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truong()
        {
            this.TruongNganhs = new HashSet<TruongNganh>();
            this.AspNetUsers = new HashSet<AspNetUser>();
        }
    
        public string Ten { get; set; }
        public string Website { get; set; }
        public int IDThanhPho { get; set; }
        public string MaTruong { get; set; }
        public string LoaiTruong { get; set; }
        public string linkLogo { get; set; }
        public string DeAnTuyenSInh { get; set; }
        public string GioiThieu { get; set; }
    
        public virtual ThanhPho ThanhPho { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TruongNganh> TruongNganhs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
