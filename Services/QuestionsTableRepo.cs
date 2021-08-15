using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjSearchStudent.Services
{
    public interface QuestionsTableRepo
    {
        //IActionResult UploadExcel(IFormCollection formdata);

        IActionResult GetQuestionsbyExamid(int id);
    }
}
