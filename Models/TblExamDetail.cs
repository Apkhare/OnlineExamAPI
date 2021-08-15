using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblExamDetail
    {
        public TblExamDetail()
        {
            TblQuestionDetails = new HashSet<TblQuestionDetail>();
        }

        public int FileId { get; set; }
        public int? TechnologyId { get; set; }
        public string LevelId { get; set; }
        public int Duration { get; set; }
        public int CutOff { get; set; }

        public virtual TblLevel Level { get; set; }
        public virtual TblTechnology Technology { get; set; }
        public virtual ICollection<TblQuestionDetail> TblQuestionDetails { get; set; }
    }
}
