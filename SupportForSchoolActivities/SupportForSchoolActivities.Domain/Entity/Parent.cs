using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Parent : User
    {
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
