using Microsoft.EntityFrameworkCore;
using SupportForSchoolActivities.DAL.Interfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Repository.BaseRepositories
{
    public abstract class BaseUsersRepository<TEntity> : BaseRepository<TEntity>, IBaseUsersRepository<TEntity>
        where TEntity : class
    {
        protected BaseUsersRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<TEntity> GetAsync(string id)
        {
            return await _db.Set<TEntity>().FindAsync(id);

        }
        public async Task<bool> DeleteByIdAsync(string id)
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
    }
}
