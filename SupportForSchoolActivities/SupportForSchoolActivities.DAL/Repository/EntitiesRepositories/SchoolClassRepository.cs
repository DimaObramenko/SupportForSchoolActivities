using SupportForSchoolActivities.DAL.Interfaces.EntitiesInterfaces;
using SupportForSchoolActivities.DAL.Repository.BaseRepositories;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SupportForSchoolActivities.DAL.Repository.EntitiesRepositories
{
    public class SchoolClassRepository : BaseEntitiesRepository<SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<SchoolClass> AddStudentsToClass(SchoolClass schoolClass, List<Student> students)
        {
            for (int i = 0; i < students.Count; i++)
            {
                schoolClass.Students.Add(students[i]);
            }
            _db.SchoolClass.Update(schoolClass);
            await _db.SaveChangesAsync();
            return schoolClass;
        }
    }
}
