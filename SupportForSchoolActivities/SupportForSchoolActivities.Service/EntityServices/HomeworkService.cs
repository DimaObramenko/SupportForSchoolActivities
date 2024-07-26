using SupportForSchoolActivities.DAL.Interfaces.EntitiesInterfaces;
using SupportForSchoolActivities.DAL.Repository.EntitiesRepositories;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.EntityServices
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }

        public async Task<bool> CreateHomework(Homework homework)
        {
            try
            {
                await _homeworkRepository.CreateAsync(homework);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteHomework(int id)
        {
            var homework = await _homeworkRepository.GetAsync(id);
            if (homework == null)
            {
                return false;
            }
            try
            {
                await _homeworkRepository.DeleteAsync(homework);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Homework>> GetAllHomeworks()
        {
            return await _homeworkRepository.SelectAsync();
        }

        public async Task<Homework> GetHomeworkById(int id)
        {
            return await _homeworkRepository.GetAsync(id);
        }

        public async Task<bool> UpdateHomework(int id, Homework homework)
        {
            if (homework == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldHomework = await _homeworkRepository.GetAsync(id);
                oldHomework.Subject = homework.Subject;
                oldHomework.Deadline = homework.Deadline;
                oldHomework.SchoolClass = homework.SchoolClass;
                oldHomework.Description = homework.Description;
                await _homeworkRepository.UpdateAsync(oldHomework);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
