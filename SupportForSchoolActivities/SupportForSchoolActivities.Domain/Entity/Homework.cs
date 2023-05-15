using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Domain.Entity
{
    public class Homework
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
