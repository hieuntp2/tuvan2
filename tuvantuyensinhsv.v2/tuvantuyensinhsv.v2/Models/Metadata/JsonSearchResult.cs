using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuvantuyensinhsv.v2.Models.Metadata
{
    public class JsonSearchResult
    {
        public string Href { get; set; }
        public string Ten { get; set; }
        public JsonSearchResult(string Ten, string Href)
        {
            this.Href = Href;
            this.Ten = Ten;
        }
    }
}