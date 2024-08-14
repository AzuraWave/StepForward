using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StepForward.Models
{
    public class SectionTypeViewModel
    {
        public Section_Type NewSectionType { get; set; }
        public IEnumerable<Section_Type> SectionTypes { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
        public string SourceAction { get; set; }
    }
}