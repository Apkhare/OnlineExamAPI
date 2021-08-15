using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Controllers
{   
    #region User Module Display Qustions
    [Route("api/DisplayQuestions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly db_Online_ExamContext db;

        public QuestionsController(db_Online_ExamContext context)
        {
            db = context;
        }
        
        //getting SubjectId
        #region Fetching Technology/Subject ID from Technology Table
        [HttpGet]
        [Route("subjectid")]
        public IActionResult GetSubjects()
        {
            return Ok(db.TblTechnologies.ToList());
        }
        #endregion
        
        //getting Exam ID
        #region Fetching File ID form Exam Table
        [HttpGet]
        [Route("examid")]
        public IActionResult GetExamId(int tech_id, string lev_id)
        {
            //dynamic exam;
            var ExamId = (from e in db.TblExamDetails
                          where e.TechnologyId == tech_id && e.LevelId == lev_id
                          select new { e.FileId }).SingleOrDefault();
            //int ex= Convert.ToInt32(ExamId);

            return Ok(ExamId);
        }
        #endregion

        //getting Questions
        #region Fetching Questions from Questions Table
        [HttpGet]
        [Route("api/levels/{id}")]
        public IActionResult GetQuestions(int id)
        {

            return Ok(db.TblQuestionDetails.Where(t => t.FileId == id).ToList());
        }
        #endregion

        //getting ExamTable
        #region Fetching Exam deatils based on File ID
        [HttpGet]
        [Route("examdetails/{id}")]
        public IActionResult GetExamDetailsbyid(int id)
        {
            return Ok(db.TblExamDetails.Where(t => t.FileId == id).ToList());
        }
        #endregion
        
        
        [HttpGet]
        [Route("examdetails")]
        public IActionResult GetExamDetails()
        {
            return Ok(db.TblExamDetails.ToList());
        }
            
        #region Fetching Questions from the database
        [Route("Questions")]
        [HttpGet]
        public List<TblQuestionDetail> BindQuestions()
        {
            List<TblQuestionDetail> quest = new List<TblQuestionDetail>();
            using (db)
            {
                quest = db.TblQuestionDetails.ToList();
            }
            return quest;
        }
        #endregion
    }
    #endregion


}
