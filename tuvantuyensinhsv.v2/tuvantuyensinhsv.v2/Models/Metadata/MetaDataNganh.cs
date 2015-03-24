using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tuvantuyensinhsv.v2.Models
{
     [MetadataType(typeof(Nganh))]
    public sealed class MetaDataNganh
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Ngành")]
        public string Ten { get; set; }

        [Display(Name = "Mã Ngành")]
        public string MaNganh { get; set; }
    }
}