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
    public class RemarkRepository : BaseEntitiesRepository<Remark>, IRemarkRepository
    {
        public RemarkRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Remark>> SelectAsync()
        {
            return await _db.Remark
                .AsNoTracking()
                .Include(r => r.Student)
                .Include(r => r.Student.Parent)
                .Include(r => r.Student.SchoolClass)
                .ToListAsync();
        }
    }
}
