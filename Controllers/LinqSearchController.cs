using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqSearchController : ControllerBase
    {
        private readonly db_Online_ExamContext db;

        public LinqSearchController(db_Online_ExamContext context)
        {
            db = context;
        }

        #region Search for Users based on filters
        [HttpGet]
        public  IActionResult Getsp_Search_User_Details([FromQuery(Name="technology")] string technology, [FromQuery(Name = "state")] string state, [FromQuery(Name = "city")] string city, [FromQuery(Name = "level")] int level, [FromQuery(Name = "mark1")] int mark1, [FromQuery(Name = "mark2")] int mark2)
        {
            var query = (from u in db.TblUserDetails
                               join r in db.TblReports
                               on u.UserId equals r.UserId
                               join t in db.TblTechnologies
                               on r.TechnologyId equals t.TechnologyId
                               join l in db.TblLevels
                               on r.LevelId equals l.LevelId
                               where t.TechnologyName == technology && u.State == state && u.City == city && l.LevelNumber == level && (mark1 <= r.MarksObtained && r.MarksObtained <= mark2)
                               select new { u.UserName, u.Email, u.MobileNumber, u.State, u.City }).ToList();

            if (query != null)
            {
                return Ok(query);
            }
            else
            {
                //return NotFound(" No Employee data found !!!");
                return Ok("No data found !!!");
            }
        }
        #endregion
        

        [Route("Technology")]
        [HttpGet]
        public IActionResult GetTechnology()
        {
            var tech = (from t in db.TblTechnologies
                        select t.TechnologyName).ToList();
            return Ok(tech);
        }
        
        
        #region Fetching Level Number in ascending order
        [Route("Level")]
        [HttpGet]
        public IActionResult GetLevel()
        {
            var level = (from l in db.TblLevels
                         orderby l.LevelNumber
                         select l.LevelNumber).ToList();
                         
            return Ok(level);
        }
        #endregion

        
        #region Fetching Distinct States
        [Route("State")]
        [HttpGet]
        public IActionResult GetState()
        {
            var state = (from u in db.TblUserDetails
                         select u.State).Distinct().ToList();
            return Ok(state);
        }
        #endregion

        
        #Fetching City from a particular state
        [Route("state/{state}")]
        [HttpGet]
        public IActionResult GetCity(string state)
        {
            var city = (from u in db.TblUserDetails
                        where u.State == state
                        select u.City).Distinct().ToList();
            return Ok(city);
        }
        #endregion

    

        

    }
}
