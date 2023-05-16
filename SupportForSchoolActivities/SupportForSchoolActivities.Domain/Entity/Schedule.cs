using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Range(1, 8)]
        public int LessonNumber { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}
