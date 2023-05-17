using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Interfaces.BaseInterfaces
{
    public interface IBaseUsersRepository<T> : IBaseRepository<T>
    {
        Task<T> GetAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
