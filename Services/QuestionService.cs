using Microsoft.AspNetCore.Mvc;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Services
{
    public class QuestionService
    {
        private readonly db_Online_ExamContext db;

        public QuestionService(db_Online_ExamContext context)
        {
            db = context;
        }
        public IActionResult GetQuestionsbyExamid(int id)
        {
            dynamic questions = db.TblQuestionDetails.Where(t => t.FileId == id).ToList();
            return questions;
            // throw new NotImplementedException();
        }
    }
}
