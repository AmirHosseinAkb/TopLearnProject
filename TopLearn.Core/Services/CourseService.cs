using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Entities.Course;
using TopLearn.Data.Context;
using TopLearn.Core.DTOs.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using TopLearn.Core.Generators;
using System.IO;
using TopLearn.Core.Convertors;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private TopLearnContext _context;
        public CourseService(TopLearnContext context)
        {
            _context = context;
        }

        public void AddComment(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "NoPhoto.png";

            if (courseImage != null)
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseImage.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Image",
                    course.CourseImageName
                    );
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    courseImage.CopyTo(stream);
                }
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Thumbnail",
                    course.CourseImageName
                    );
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 200);
            }
            if (courseDemo != null)
            {
                course.CourseDemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Demos",
                    course.CourseDemoFileName
                    );
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                episode.EpisodeFileName = episodeFile.FileName;
                string episodePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Course",
                    "Episodes",
                    episode.EpisodeFileName
                    );
                using (var stream = new FileStream(episodePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }
            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
        }

        public void AddGroup(CourseGroup group)
        {
            _context.CourseGroups.Add(group);
            _context.SaveChanges();
        }

        public void DeleteEpisode(CourseEpisode episode)
        {
            episode.IsDeleted = true;
            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public void EditCourse(Course course, IFormFile courseImage, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;
            if (courseImage != null)
            {
                string imagePath = "";
                if (course.CourseImageName != "NoPhoto.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Image",
                        course.CourseImageName
                        );
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Thumbnail",
                        course.CourseImageName
                        );
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseImage.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Image",
                        course.CourseImageName
                        );
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    courseImage.CopyTo(stream);
                }
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Thumbnail",
                        course.CourseImageName
                        );
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 200);
            }
            if (courseDemo != null)
            {
                string demoPath = "";
                if (course.CourseDemoFileName != null)
                {
                    demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Demos",
                        course.CourseDemoFileName
                        );
                    if (File.Exists(demoPath))
                    {
                        File.Delete(demoPath);
                    }
                }
                course.CourseDemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                demoPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Demos",
                        course.CourseDemoFileName
                        );
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string episodePath = "";
                if (episode.EpisodeFileName != null)
                {
                    episodePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Episodes",
                        episode.EpisodeFileName
                    );
                    if (File.Exists(episodePath))
                    {
                        File.Delete(episodePath);
                    }
                }
                episode.EpisodeFileName = episodeFile.FileName;

                episodePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Course",
                        "Episodes",
                        episode.EpisodeFileName
                    );
                using (var stream = new FileStream(episodePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }
            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComment>,int> GetAllCourseComments(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;

            var query= _context.CourseComments.Include(c=>c.User).Where(c => c.CourseId == courseId).OrderByDescending(c=>c.CreateDate).Skip(skip).Take(take).ToList();
            int pageCount = _context.CourseComments.Where(c => c.CourseId == courseId).Count() / take;
            if (_context.CourseComments.Where(c => c.CourseId == courseId).Count() % take != 0)
            {
                pageCount++;
            }
            return Tuple.Create(query, pageCount);
        }

        public List<CourseGroup> GetAllGroups()
        {
            return _context.CourseGroups.Include(g=>g.CourseGroups).ToList();
        }

        public Tuple<List<ShowCoursesListItem>, int> GetCourse(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;
            IQueryable<Course> result = _context.Courses;
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }
            switch (getType)
            {
                case "all":
                    break;

                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updateDate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }
            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }
            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < endPrice);   
            }

            if (selectedGroups != null)
            {
                foreach (var groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubId == groupId);
                }
            }

            int skip = (pageId - 1) * take;

            int pageCount;
            if (result.Count() % take == 0)
            {
                pageCount = result.Count() / take;
            }
            else
            {
                pageCount = (result.Count() / take)+1;
            }
            

            var query = result.Include(c=>c.CourseEpisodes).Select(c => new ShowCoursesListItem()
            {
                CourseId = c.CourseId,
                CourseImageName = c.CourseImageName,
                CoursePrice = c.CoursePrice,
                CourseTitle = c.CourseTitle,
                TimeCount=new TimeSpan(c.CourseEpisodes.Sum(e=>e.EpisodeTime.Seconds))
            }).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public Course GetCourseByCourseId(int courseId)
        {
            return _context.Courses.SingleOrDefault(c => c.CourseId == courseId);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses.Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus).Include(c => c.CourseLevel)
                .Include(c=>c.UserCourses)
                .Include(c => c.User).SingleOrDefault(c => c.CourseId == courseId);
        }

        public List<SelectListItem> GetCourseGroups()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<GetCoursesForShowViewModel> GetCoursesForShow()
        {
            return _context.Courses.Include(c => c.CourseEpisodes)
                .Select(c => new GetCoursesForShowViewModel()
                {
                    CourseId = c.CourseId,
                    CourseImageName = c.CourseImageName,
                    CourseTitle = c.CourseTitle,
                    CoursePrice = c.CoursePrice,
                    EpisodeCount = c.CourseEpisodes.Count()
                }).ToList();
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.SingleOrDefault(e => e.EpisodeId == episodeId);
        }

        public List<CourseEpisode> GetEpisodesOfCourse(int courseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels
                .Select(l => new SelectListItem()
                {
                    Text = l.LevelTitle,
                    Value = l.LevelId.ToString()
                }).ToList();
        }

        public List<ShowCoursesListItem> GetPopularCourses()
        {
            return _context.Courses.Include(c => c.OrderDetails)
                .Include(c => c.CourseEpisodes)
                .Where(c=>c.OrderDetails.Any())
                .OrderByDescending(c => c.OrderDetails.Count())
                .Select(c => new ShowCoursesListItem()
                {
                    CourseId=c.CourseId,
                    CourseImageName=c.CourseImageName,
                    CoursePrice=c.CoursePrice,
                    CourseTitle=c.CourseTitle,
                    TimeCount=new TimeSpan(c.CourseEpisodes.Sum(e=>e.EpisodeTime.Minutes))
                }).ToList();
                
        }

        public List<SelectListItem> GetStatuses()
        {
            return _context.CourseStatuses
                .Select(s => new SelectListItem()
                {
                    Text = s.StatusTitle,
                    Value = s.StatusId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupsOfGroup(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Include(ur => ur.User).Where(ur => ur.RoleId == 2)
                .Select(ur => ur.User)
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.UserId.ToString()
                }).ToList();
        }

        public bool IsExistEpisode(string episodeName)
        {
            return _context.CourseEpisodes.Any(e => e.EpisodeFileName == episodeName);
        }

        public void UpdateGroup(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
            _context.SaveChanges();
        }
    }
}
