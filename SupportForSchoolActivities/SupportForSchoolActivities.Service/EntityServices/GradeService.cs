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
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<bool> CreateGrade(Grade grade)
        {
            try
            {
                await _gradeRepository.CreateAsync(grade);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await _gradeRepository.GetAsync(id);
            if (grade == null)
            {
                return false;
            }
            try
            {
                await _gradeRepository.DeleteAsync(grade);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Grade>> GetAllGrades()
        {
            return await _gradeRepository.SelectAsync();
        }

        public async Task<Grade> GetGradeById(int id)
        {
            return await _gradeRepository.GetAsync(id);
        }

        public async Task<bool> UpdateGrade(int id, Grade grade)
        {
            if (grade == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldGrade = await _gradeRepository.GetAsync(id);
                oldGrade.Subject = grade.Subject;
                oldGrade.Student = grade.Student;
                oldGrade.Mark = grade.Mark;
                oldGrade.Date = grade.Date;
                await _gradeRepository.UpdateAsync(oldGrade);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
