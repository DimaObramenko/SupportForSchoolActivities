using SupportForSchoolActivities.DAL.Interfaces.EntitiesInterfaces;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.EntityServices
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<bool> CreateSchedule(Schedule schedule)
        {
            try
            {
                await _scheduleRepository.CreateAsync(schedule);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSchedule(int id)
        {
            var schedule = await _scheduleRepository.GetAsync(id);
            if(schedule == null)
            {
                return false;
            }
            try
            {
                await _scheduleRepository.DeleteAsync(schedule);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            return await _scheduleRepository.SelectAsync();
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return await _scheduleRepository.GetAsync(id);
        }

        public async Task<bool> UpdateSchedule(int id, Schedule schedule)
        {
            if(schedule == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldSchedule = await _scheduleRepository.GetAsync(id);
                oldSchedule.Subject = schedule.Subject;
                oldSchedule.DayOfWeek = schedule.DayOfWeek;
                oldSchedule.SchoolClass = schedule.SchoolClass;
                oldSchedule.LessonNumber = schedule.LessonNumber;
                await _scheduleRepository.UpdateAsync(oldSchedule);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
