using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Teacher : User
    {
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
