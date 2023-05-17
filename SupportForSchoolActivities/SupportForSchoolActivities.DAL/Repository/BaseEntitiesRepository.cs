using SupportForSchoolActivities.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Repository
{
    public abstract class BaseEntitiesRepository<TEntity> : BaseRepository<TEntity>, IBaseEntitiesRepository<TEntity>
        where TEntity : class
    {
        protected BaseEntitiesRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _db.Set<TEntity>().Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }
    }
}
