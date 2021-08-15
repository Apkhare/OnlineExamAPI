using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblReport
    {
        public int ReportId { get; set; }
        public int? UserId { get; set; }
        public int? TechnologyId { get; set; }
        public string LevelId { get; set; }
        public int MarksObtained { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public TimeSpan? SubmissionTime { get; set; }

        public virtual TblLevel Level { get; set; }
        public virtual TblTechnology Technology { get; set; }
        public virtual TblUserDetail User { get; set; }
    }
}
