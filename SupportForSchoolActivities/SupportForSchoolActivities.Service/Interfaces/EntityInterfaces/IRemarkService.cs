using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface IRemarkService
    {
        Task<bool> CreateRemark(Remark remark);
        Task<bool> UpdateRemark(int id, Remark remark);
        Task<bool> DeleteRemark(int id);
        Task<List<Remark>> GetAllRemarks();
        Task<Remark> GetRemark(int id);
    }
}
