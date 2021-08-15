using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Controllers
{
    [Route("api/AddQuestions")]
    [ApiController]
    public class AddQuesController : ControllerBase
    {
        private readonly db_Online_ExamContext db;

        public AddQuesController(db_Online_ExamContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult AddExamDetails(string technology, int level, int duration, int cutoff )
        {
            var techid = (from t in db.TblTechnologies
                           where t.TechnologyName == technology
                           select t.TechnologyId).FirstOrDefault();

            var levelid = (from l in db.TblLevels
                           where l.LevelNumber == level
                           select l.LevelId).FirstOrDefault();

            TblExamDetail examtable = new TblExamDetail();
            examtable.TechnologyId = techid;
            examtable.LevelId = levelid;
            examtable.Duration = duration;
            examtable.CutOff = cutoff;

            db.TblExamDetails.Add(examtable);

            db.SaveChanges();

            dynamic fileid = (from e in db.TblExamDetails
                            where e.TechnologyId == techid && e.LevelId == levelid
                            select e.FileId).FirstOrDefault();
;
            return Ok(fileid);
        }

        [Route("UploadExcel/{fileid}")]
        [HttpPost]
        public IActionResult ExcelUpload(int fileid, IFormCollection formdata)
        {

            var file = HttpContext.Request.Form.Files[0];
            try
            {
                using (db)
                {


                    Stream stream = file.OpenReadStream();

                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    }


                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];
                    for (int i = 1; i < finalRecords.Rows.Count; i++)
                    {
                        TblQuestionDetail tbq = new TblQuestionDetail();
                        tbq.FileId = fileid;
                        tbq.Question = finalRecords.Rows[i][0].ToString();
                        tbq.Option1 = finalRecords.Rows[i][1].ToString();
                        tbq.Option2 = finalRecords.Rows[i][2].ToString();
                        tbq.Option3 = finalRecords.Rows[i][3].ToString();
                        tbq.Option4 = finalRecords.Rows[i][4].ToString();
                        tbq.CorrectAnswer = finalRecords.Rows[i][5].ToString();

                        db.TblQuestionDetails.Add(tbq);
                    }

                    int output = db.SaveChanges();
                    if (output > 0)
                    {
                        //isSaveSuccess = true;
                        return Ok();
                    }
                    else
                    {
                        //isSaveSuccess = false;
                        return BadRequest(new { message = "Invalid file extension" });
                    }
                }
            }
            catch (Exception e) {
            }

            return BadRequest(new { message = "Invalid file extension" });

        }

        
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
            var result1 = (from l in db.TblLevels
                           select l).ToList();

            var result2 = (from t in db.TblTechnologies
                           join e in db.TblExamDetails
                           on t.TechnologyId equals e.TechnologyId
                           where t.TechnologyName == technology
                           select e).ToList();
            
            List<int> Levellist = (from e in result1
                                       where !(from m in result2
                                               select m.LevelId).Contains(e.LevelId)
                                               orderby e.LevelNumber
                                       select e.LevelNumber).ToList();


            return Ok(Levellist);
        }
    }
}

