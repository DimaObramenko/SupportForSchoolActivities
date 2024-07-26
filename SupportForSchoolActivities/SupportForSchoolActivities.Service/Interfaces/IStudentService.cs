using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudent(string id);
        Task<bool> CreateStudent(Student student, Parent parent);
        Task<bool> CreateStudent(Student student);
        Task<bool> UpdateStudent(string id, Student student);
        Task<bool> DeleteStudent(string id);
        Task<List<Student>> GetStudentsFromOneClass(int id);
    }
}
