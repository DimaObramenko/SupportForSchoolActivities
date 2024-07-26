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
    public class ScheduleRepository : BaseEntitiesRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Schedule>> SelectAsync()
        {
            return await _db.Schedule
                .AsNoTracking()
                .Include(s => s.SchoolClass)
                .Include(s => s.Subject)
                .ToListAsync();
        }
    }
}
