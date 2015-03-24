using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tuvantuyensinhsv.v2.Models
{
     [MetadataType(typeof(Truong))]
    public sealed class MetaDataTruong
    {  
        public int ID { get; set; }

        [Required]
        [Display(Name = "Trường")]
        public string Ten { get; set; }
        public string Website { get; set; }

        [Display(Name = "Tỉnh/Thành phố")]
        public int IDThanhPho { get; set; }

        [Display(Name = "Mã trường")]
        public string MaTruong { get; set; }

        [Display(Name = "Loại trường")]
        public string LoaiTruong { get; set; }
    
    }
}