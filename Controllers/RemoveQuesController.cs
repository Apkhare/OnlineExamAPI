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
    public class RemoveQuesController : ControllerBase
    {
        
        private readonly db_Online_ExamContext db;

        public RemoveQuesController(db_Online_ExamContext context)
        {
            db = context;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQues(string technology, int level)
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

            //dynamic examtable = await db.TblExamDetails.FindAsync(examid);

            //dynamic questable = await db.TblQuestionDetails.FindAsync(examid);

            var exam = await db.TblExamDetails.FindAsync(examid);

            db.TblExamDetails.Remove(exam);

            //var ques = await db.TblQuestionDetails.FindAsync(qexamid);

            db.TblQuestionDetails.RemoveRange(ques);

            db.SaveChanges();

            return Ok(ques);


            


        }


        [Route("Technology")]
        [HttpGet]
        public IActionResult GetTechnology()
        {
            var tech = (from t in db.TblTechnologies
                        select t.TechnologyName).ToList();
            return Ok(tech);
        }


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

    }
}

