using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;
using TopLearn.Core.Security;

namespace TopLearn.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        private ICourseService _courseService;
        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void GetInformations()
        {
            var groups = _courseService.GetCourseGroups();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="لطفا انتخاب کنید",Value=""}
            };
            var subGroups = _courseService.GetSubGroupsOfGroup(int.Parse(groups.First().Value));
            list.AddRange(subGroups);
            ViewData["SubGroups"] = new SelectList(list, "Value", "Text");

            var statuses = _courseService.GetStatuses();
            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");
        }
        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            GetInformations();
        }

        public IActionResult OnPost(IFormFile imgCourseUp,IFormFile demoUp)
        {
            if (!ModelState.IsValid)
            {
                GetInformations();
                return Page();
            }
            if (!imgCourseUp.IsImage())
            {
                GetInformations();
                ModelState.AddModelError("Tags", "تصویر دوره را انتخاب کنید");
                return Page();
            }
            //AddCourse
            _courseService.AddCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
