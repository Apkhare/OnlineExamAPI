using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblQuestionDetail
    {
        public int QuestionId { get; set; }
        public int? FileId { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }

        public virtual TblExamDetail File { get; set; }
    }
}
