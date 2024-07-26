using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Range(1, 11)]
        public int ClassNumber { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
