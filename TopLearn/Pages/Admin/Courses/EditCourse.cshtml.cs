using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;
using TopLearn.Core.Security;
using Microsoft.AspNetCore.Http;

namespace TopLearn.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;
        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void GetInformations(Course course)
        {
            var groups = _courseService.GetCourseGroups();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", course.GroupId);

            var subGroups = _courseService.GetSubGroupsOfGroup(Course.GroupId);
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", course.SubId);

            var statuses = _courseService.GetStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text", course.StatusId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", course.LevelId);

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", course.TeacherId);
        }

        [BindProperty]
        public Course Course { get; set; }

        public void OnGet(int courseId)
        {
            Course = _courseService.GetCourseByCourseId(courseId);
            GetInformations(Course);
        }
        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
            {
                GetInformations(Course);
                return Page();
            }
            if (!imgCourseUp.IsImage())
            {
                GetInformations(Course);
                ModelState.AddModelError("Tags", "تصویر دوره را انتخاب کنید");
                return Page();
            }

            //Edit Course
            _courseService.EditCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
