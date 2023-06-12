using Microsoft.EntityFrameworkCore;
using SupportForSchoolActivities.DAL.Interfaces.EntitiesInterfaces;
using SupportForSchoolActivities.DAL.Repository.BaseRepositories;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Repository.EntitiesRepositories
{
    public class GradeRepository : BaseEntitiesRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Grade>> SelectAsync()
        {
            return await _db.Grade
                .AsNoTracking()
                .Include(g => g.Student)
                .Include(g => g.Student.Parent)
                .Include(g => g.Student.SchoolClass)
                .Include(g => g.Subject)
                .ToListAsync();
        }
    }
}
