using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblTechnology
    {
        public TblTechnology()
        {
            TblExamDetails = new HashSet<TblExamDetail>();
            TblReports = new HashSet<TblReport>();
        }

        public int TechnologyId { get; set; }
        public string TechnologyName { get; set; }

        public virtual ICollection<TblExamDetail> TblExamDetails { get; set; }
        public virtual ICollection<TblReport> TblReports { get; set; }
    }
}
