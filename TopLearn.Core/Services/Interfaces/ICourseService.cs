using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Data.Entities.Course;
using TopLearn.Core.DTOs.Course;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region GetInformations
        List<CourseGroup> GetAllGroups();
        List<GetCoursesForShowViewModel> GetCoursesForShow();
        List<SelectListItem> GetCourseGroups();
        List<SelectListItem> GetSubGroupsOfGroup(int groupId);
        List<SelectListItem> GetStatuses();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetTeachers();
        #endregion

        #region Course
        void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo);
        Course GetCourseByCourseId(int courseId);
        void EditCourse(Course course, IFormFile courseImage, IFormFile courseDemo);
        List<CourseEpisode> GetEpisodesOfCourse(int courseId);
        bool IsExistEpisode(string episodeName);
        void AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        CourseEpisode GetEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        void DeleteEpisode(CourseEpisode episode);
        Tuple<List<ShowCoursesListItem>, int> GetCourse(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
        Course GetCourseForShow(int courseId);
        List<ShowCoursesListItem> GetPopularCourses();
        void AddGroup(CourseGroup group);
        void UpdateGroup(CourseGroup group);
        #endregion

        #region Comment

        void AddComment(CourseComment comment);
        Tuple<List<CourseComment>,int> GetAllCourseComments(int courseId,int pageId=1);

        #endregion
    }
}
