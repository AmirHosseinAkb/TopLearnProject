using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Course
{
    public class GetCoursesForShowViewModel
    {
        public int CourseId { get; set; }
        public string CourseImageName { get; set; }
        public string CourseTitle { get; set; }
        public int CoursePrice { get; set; }
        public int EpisodeCount { get; set; }
    }
}
