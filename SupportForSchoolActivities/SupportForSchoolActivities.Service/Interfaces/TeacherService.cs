using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.DAL.Repository;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<bool> CreateTeacher(Teacher teacher)
        {
            try
            {
                await _teacherRepository.CreateAsync(teacher);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTeacher(string id)
        {
            var teacher = await _teacherRepository.GetAsync(id);
            if (teacher == null)
            {
                return false;
            }
            try
            {
                await _teacherRepository.DeleteAsync(teacher);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Subject>> GetAllSubjectOfThisTeacher(string id)
        {
            var teacher = await _teacherRepository.GetAsync(id);
            return teacher.Subjects.ToList();
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _teacherRepository.SelectAsync();

        }

        public async Task<Teacher> GetTeacher(string id)
        {
            return await _teacherRepository.GetAsync(id);
        }

        public async Task<bool> UpdateTeacher(string id, Teacher teacher)
        {
            if (teacher == null || id == null)
            {
                return false;
            }
            try
            {
                var oldTeacher = await _teacherRepository.GetAsync(id);
                oldTeacher.FirstName= teacher.FirstName;
                oldTeacher.LastName= teacher.LastName;
                oldTeacher.Email= teacher.Email;
                oldTeacher.PhoneNumber= teacher.PhoneNumber;
                oldTeacher.UserName= teacher.UserName;
                oldTeacher.Subjects= teacher.Subjects;
                await _teacherRepository.UpdateAsync(oldTeacher);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
