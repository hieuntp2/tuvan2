using System;
using System.ComponentModel.DataAnnotations;

namespace tuvantuyensinhsv.v2.Models
{
    [MetadataType(typeof(TruongNganh))]
    public sealed class MetaDataTruongNganh
    {
        [Required]
        public int MaTruong { get; set; }

        [Required]
        public int IDNganh { get; set; }

        [Required]
        public int KhoiThi { get; set; }

        [Range(0, 100)]
        [Display(Name = "Điểm 1")]
        public Nullable<double> Diem1 { get; set; }

        [Display(Name = "Điểm 2")]
        [Range(0, 100)]
        public Nullable<double> Diem2 { get; set; }

        [Display(Name = "Điểm 3")]
        [Range(0, 100)]
        public Nullable<double> Diem3 { get; set; }
        public System.DateTime NgayCapNhat { get; set; }
    }
}