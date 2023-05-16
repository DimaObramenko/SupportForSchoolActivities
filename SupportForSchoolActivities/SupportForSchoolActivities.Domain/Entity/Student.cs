using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Student : User
    {
        public virtual ICollection<Grade> Grades { get; set; }
        [Required]
        public virtual Parent Parent { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}
