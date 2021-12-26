using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;

namespace TopLearn.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IOrderService _orderService;
        private IUserService _userService;
        public CourseController(ICourseService courseService,IOrderService orderService,IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
        }
        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            ViewData["Groups"] = _courseService.GetAllGroups();
            ViewData["SelectedGroup"] = selectedGroups;
            ViewData["PageId"] = pageId;
            return View(_courseService.GetCourse(pageId,filter,getType,orderByType,startPrice,endPrice,selectedGroups,3));
        }
        [Route("ShowCourse/{courseId}")]
        public IActionResult ShowCourse(int courseId)
        {
            var course = _courseService.GetCourseForShow(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [Authorize]
        [Route("BuyCourse/{courseId}")]
        public IActionResult BuyCourse(int courseId)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, courseId);
            return Redirect("/ShowOrder/"+orderId);
        }
        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetEpisodeById(episodeId);

            if (episode == null)
            {
                return Forbid();
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Course",
                "Episodes",
                episode.EpisodeFileName
                );
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file,"application/force-download",fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserHaveCourse(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult CreateComment(CourseComment courseComment)
        {
            courseComment.IsDeleted = false;
            courseComment.CreateDate = DateTime.Now;
            courseComment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseService.AddComment(courseComment);
            return View("ShowComments",_courseService.GetAllCourseComments(courseComment.CourseId));
        }

        public IActionResult ShowComments(int id,int pageId=1)
        {
            return View(_courseService.GetAllCourseComments(id, pageId));
        }
    }
}
