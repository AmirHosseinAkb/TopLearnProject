using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public List<GetCoursesForShowViewModel> Courses { get; set; }
        public void OnGet()
        {
            Courses = _courseService.GetCoursesForShow();
        }
    }
}
