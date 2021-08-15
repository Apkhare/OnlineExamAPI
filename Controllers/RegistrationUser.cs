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
    public class RegistrationUser : ControllerBase
    {
        private readonly db_Online_ExamContext db;

        public RegistrationUser(db_Online_ExamContext context)
        {
            db = context;
        }

        [HttpPost]

        public IActionResult UserDetails(string email, string mobileno, string username, string city, string state, DateTime date_of_birth, string qualification, string password, int year_of_passing)
        {
            

            TblUserDetail usertable = new TblUserDetail();
            usertable.Email = email;
            usertable.MobileNumber = mobileno;
            usertable.UserName = username;
            usertable.City = city;
            usertable.State = state;
            usertable.DateOfBirth = date_of_birth;
            usertable.Qualification = qualification;
            usertable.Password = password;
            usertable.YearOfPassing = year_of_passing;


            db.TblUserDetails.Add(usertable);

            db.SaveChanges();

            return Ok();
        }
    }
}
