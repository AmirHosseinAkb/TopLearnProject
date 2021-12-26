using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int courseId)
        {
            CourseEpisode = new CourseEpisode()
            {
                CourseId = courseId
            };
        }

        public IActionResult OnPost(IFormFile episodeFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (episodeFile == null)
            {
                ViewData["NullEpisode"] = true;
                return Page();
            }
            if (_courseService.IsExistEpisode(episodeFile.FileName))
            {
                ViewData["ExistEpisode"] = true;
                return Page();
            }
            _courseService.AddEpisode(CourseEpisode, episodeFile);

            return Redirect("/Admin/Courses/EpisodeIndex/" + CourseEpisode.CourseId);
        }
    }
}
