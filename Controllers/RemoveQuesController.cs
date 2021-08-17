using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    ///
    /// 
    /// 
    ///
    

    #region Admin Module Removing Questions
    public class RemoveQuesController : ControllerBase
    {
        
        private readonly db_Online_ExamContext db;

        public RemoveQuesController(db_Online_ExamContext context)
        {
            db = context;
        }
        #region Deleting questions from questions and exam table
        [HttpDelete]
        public async Task<IActionResult> DeleteQues(string technology, int level)
        {


            try
            {
                var examid = (from t in db.TblTechnologies
                              join e in db.TblExamDetails
                              on t.TechnologyId equals e.TechnologyId
                              join l in db.TblLevels
                              on e.LevelId equals l.LevelId
                              where t.TechnologyName == technology && l.LevelNumber == level
                              select e.FileId).FirstOrDefault();

                var ques = (from q in db.TblQuestionDetails
                            where q.FileId == examid
                            select q).ToList();



                var exam = await db.TblExamDetails.FindAsync(examid);

                db.TblExamDetails.Remove(exam);

                db.TblQuestionDetails.RemoveRange(ques);

                db.SaveChanges();

                return Ok(ques);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Ok("Invalid");
            }


        }
        #endregion

       

        #region Display Levels based on Technology whose questions exist
        [Route("{technology}")]
        [HttpGet]
        public IActionResult GetLevel(string technology)
        {
            var level = (from t in db.TblTechnologies
                         join e in db.TblExamDetails
                         on t.TechnologyId equals e.TechnologyId
                         join l in db.TblLevels
                         on e.LevelId equals l.LevelId
                         where t.TechnologyName == technology
                         select l.LevelNumber).ToList();
            return Ok(level);
        }
        #endregion

    }
    #endregion
}

