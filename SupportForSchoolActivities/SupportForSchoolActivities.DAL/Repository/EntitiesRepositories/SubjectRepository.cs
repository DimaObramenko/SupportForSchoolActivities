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
    public class SubjectRepository : BaseEntitiesRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Subject>> SelectAsync()
        {
            return await _db.Subject
                .AsNoTracking()
                .Include(s=>s.Teachers)
                .ToListAsync();
        }
    }
}
