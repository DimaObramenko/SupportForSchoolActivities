using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacher(string id);
        Task<bool> CreateTeacher(Teacher teacher);
        Task<bool> UpdateTeacher(string id, Teacher teacher);
        Task<bool> DeleteTeacher(string id);
        Task<List<Subject>> GetAllSubjectOfThisTeacher(string id);
    }
}
