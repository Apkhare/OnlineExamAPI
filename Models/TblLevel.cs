using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblLevel
    {
        public TblLevel()
        {
            TblExamDetails = new HashSet<TblExamDetail>();
            TblReports = new HashSet<TblReport>();
        }

        public string LevelId { get; set; }
        public int LevelNumber { get; set; }

        public virtual ICollection<TblExamDetail> TblExamDetails { get; set; }
        public virtual ICollection<TblReport> TblReports { get; set; }
    }
}
