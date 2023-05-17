using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Interfaces.BaseInterfaces
{
    public interface IBaseEntitiesRepository<T> : IBaseRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
    }
}
