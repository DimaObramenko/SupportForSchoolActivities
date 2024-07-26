using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface IHomeworkService
    {
        Task<bool> CreateHomework(Homework homework);
        Task<bool> UpdateHomework(int id, Homework homework);
        Task<bool> DeleteHomework(int id);
        Task<List<Homework>> GetAllHomeworks();
        Task<Homework> GetHomeworkById(int id);
    }
}
