using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjSearchStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrjSearchStudent.Controllers
{
    #region User Module API's
    [Route("api/User")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly db_Online_ExamContext db;

        public UserDetailsController(db_Online_ExamContext context)
        {
            db = context;
        }

       
        #region Validating Email and Passsowrd while login
        [Route("do-login")]
        [HttpGet]
        public IActionResult checkLogin([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password)
        {
            try
            {
                TblUserDetail result = db.TblUserDetails.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                //TblUserDetails pass = db.TblUserDetails.Where(x => x.Password == password).FirstOrDefault();


                if (result != null)
                    return Ok("Success");
                else
                    return Ok("Invalid");

                /*
                if(result.Email !=null && pass.Password !=null)
                {
                    return Ok("Success");
                }
                else
                {
                    return Ok("Invalid");
                }*/
            }

            catch (Exception e)
            {
                Console.Write(e);
                return Ok("Invalid");
            }
        }
        #endregion
        
        #region Fetch User ID based on email
        [Route("userid")]
        [HttpGet]
        public IActionResult getUserId([FromQuery(Name = "email")] string email)
        {
            try
            {
                var result = (from x in db.TblUserDetails where x.Email == email select x.UserId).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Ok("Cannot Retrieve UserId");
            }
        }
        #endregion



        /*
        // DELETE: api/TblUserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUserDetail(int id)
        {
            var tblUserDetail = await _context.TblUserDetails.FindAsync(id);
            if (tblUserDetail == null)
            {
                return NotFound();
            }
            _context.TblUserDetails.Remove(tblUserDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        

        private bool TblUserDetailExists(int id)
        {
            return db.TblUserDetails.Any(e => e.UserId == id);
        }
        */
        #region Display Report based on Technology and Level
        [HttpGet]
       /* [Route("reportdetails/{id}")]
        public IActionResult GetReportbyId(int id)

        {
            return Ok(db.TblReports.Where(e => e.UserId == id).ToList());
        } */

        [Route("reportdetails/{userid}")]
        public IActionResult GetReportbyId(int userid)
        {
            var report = (from r in db.TblReports
                          join t in db.TblTechnologies
                          on r.TechnologyId equals t.TechnologyId
                          join l in db.TblLevels
                          on r.LevelId equals l.LevelId
                          where r.UserId == userid
                          select new
                          {
                              t.TechnologyName,
                              l.LevelNumber,
                              r.MarksObtained,
                              r.SubmissionDate
                          }
                              ).ToList();
            return Ok(report);
        }
        #endregion
        
        #region Adding exam report of the user
        [HttpPost]
        [Route("addreport")]
        public IActionResult AddReport(int userid, int techid, string levelid, int marks)
        {
            TblReport report = new TblReport();
            if (TblUserExists(userid))
            {

                report.UserId = userid;
                report.TechnologyId = techid;
                report.LevelId = levelid;
                report.MarksObtained = marks;
                db.TblReports.Add(report);
                db.SaveChanges();

                return Ok("Added");
            }
            else
            {
                return BadRequest("No Student with this id");
            }

        }
        #endregion


        private bool TblUserExists(int id)
        {
            return db.TblUserDetails.Any(e => e.UserId == id);

        }
        
        #region Fetching Report from Report table
        [HttpGet]
        [Route("allreportdetails")]
        public IActionResult GetAllReportDetails()
        {
            return Ok(db.TblReports.ToList());
        }
        #endregion
        
        #region Sending OTP of email exists
        [HttpGet]
        [Route("forgot-pass")]
        //public async Task<int> ForgotId([FromQuery(Name = "email")] string email)
        public IActionResult ForgotId([FromQuery(Name = "email")] string email)
        {
            TblUserDetail fp = db.TblUserDetails.Where(a => a.Email == email).FirstOrDefault();
            if (fp == null)
            {
                return Ok("Incorrect Email ID");

                /*return 0;*/
            }

            string to = fp.Email;

            using SmtpClient smtp = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("ltiexamportal@gmail.com", "LtiExam@123"),
            };

            string subject = "Forgot Password";
            Random rand = new Random();
            int otp = rand.Next(100000, 999999);
            string body = "OTP for forget Password is " + otp;
            try
            {

                smtp.Send("ltiexamportal@gmail.com", to, subject, body);
                /*return otp;*/
                return Ok(otp);
            }
            catch (Exception e)
            {
                return BadRequest("Email couldn't be sent");

                /*return -1;*/
            }

            /*int otp = SendEmailOtp(to);
                        return Ok(new { otp = otp, Email = email });*/
        }
        #endregion
        
        #region Change Password after OTP verification
        [HttpPut]
        [Route("update-pass")]
        /* public IActionResult UpdatePassword([FromQuery (Name="email")] string email, [FromQuery(Name = "password")] string password)
        */
        public IActionResult UpdatePassword(string email, string password)

        {
            try
            {
                var up = (from x in db.TblUserDetails where x.Email == email select x).FirstOrDefault();
                up.Password = password;
                db.SaveChanges();

                return Ok("Password Changed");
            }

            catch (Exception e)
            {
                return BadRequest("Invalid Email/OTP");
            }
        }
        #endregion


    }
    #endregion
}
/*
      // GET: api/TblUserDetails
      [HttpGet]
      public async Task<ActionResult<IEnumerable<TblUserDetail>>> GetTblUserDetails()
      {
          return await db.TblUserDetails.ToListAsync();
      }

      // GET: api/TblUserDetails/5
      [HttpGet("{id}")]
      public async Task<ActionResult<TblUserDetail>> GetTblUserDetail(int id)
      {
          var tblUserDetail = await db.TblUserDetails.FindAsync(id);

          if (tblUserDetail == null)
          {
              return NotFound();
          }

          return tblUserDetail;
      }

      */
/*
// PUT: api/TblUserDetails/5
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
[HttpPut("{id}")]
public async Task<IActionResult> PutTblUserDetail(int id, TblUserDetail tblUserDetail)
{
    if (id != tblUserDetail.UserId)
    {
        return BadRequest();
    }
    _context.Entry(tblUserDetail).State = EntityState.Modified;
    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!TblUserDetailExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    return NoContent();
}
*/
// POST: api/TblUserDetails
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
