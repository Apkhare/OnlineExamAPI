using System;
using System.Collections.Generic;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class TblUserDetail
    {
        public TblUserDetail()
        {
            TblReports = new HashSet<TblReport>();
        }

        public int UserId { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string Password { get; set; }
        public bool? Role { get; set; }
        public int? YearOfPassing { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TblReport> TblReports { get; set; }
    }
}
