using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Data.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Display(Name = "عنوان اپیزود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string EpisodeTitle { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
        public bool IsDeleted { get; set; }

        public Course Course { get; set; }

    }
}
