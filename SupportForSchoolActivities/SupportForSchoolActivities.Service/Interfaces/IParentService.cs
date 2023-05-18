using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces
{
    public interface IParentService
    {
        Task<List<Parent>> GetAllParents();
        Task<Parent> GetParent(string id);
        Task<bool> CreateParent(Parent parent);
        Task<bool> UpdateParent(string id, Parent parent);
        Task<bool> DeleteParent(string id);
    }
}
