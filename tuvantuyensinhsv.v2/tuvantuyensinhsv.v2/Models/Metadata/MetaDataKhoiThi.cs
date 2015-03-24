using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tuvantuyensinhsv.v2.Models
{
    [MetadataType(typeof(KhoiThi))]
    public sealed class MetaDataKhoiThi
    {
        [Required]
        [Display(Name = "Khối")]
        public string Ten { get; set; }
        public int ID { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
    }
}