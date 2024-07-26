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
    public class HomeworkRepository : BaseEntitiesRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Homework>> SelectAsync()
        {
            return await _db.Homework
                .AsNoTracking()
                .Include(h => h.SchoolClass)
                .Include(h => h.Subject)
                .ToListAsync();
        }
    }
}
