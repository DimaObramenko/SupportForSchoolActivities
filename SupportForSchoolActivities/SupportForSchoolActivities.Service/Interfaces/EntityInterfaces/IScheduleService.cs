using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface IScheduleService
    {
        Task<bool> CreateSchedule(Schedule schedule);
        Task<bool> UpdateSchedule(int id, Schedule schedule);
        Task<bool> DeleteSchedule(int id);
        Task<List<Schedule>> GetAllSchedules();
        Task<Schedule> GetScheduleById(int id);
    }
}
