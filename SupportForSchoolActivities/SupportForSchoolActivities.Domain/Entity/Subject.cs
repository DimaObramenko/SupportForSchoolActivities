using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
