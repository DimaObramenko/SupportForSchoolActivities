using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.DAL.Repository;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateStudent(Student student, Parent parent)
        {
            try
            {
                await _studentRepository.CreateAsync(student, parent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateStudent(Student student)
        {
            try
            {
                await _studentRepository.CreateAsync(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudent(string id)
        {
            var student = await _studentRepository.GetAsync(id);
            if(student == null)
            {
                return false;
            }
            try
            {
                await _studentRepository.DeleteAsync(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var students = await _studentRepository.SelectAsync();
            return students;
        }

        public async Task<Student> GetStudent(string id)
        {
            var student = await _studentRepository.GetAsync(id);
            return student;
        }

        public async Task<bool> UpdateStudent(string id, Student student)
        {
            if (student == null || id == null)
            {
                return false;
            }
            try
            {
                var oldStudent = await _studentRepository.GetAsync(id);
                oldStudent.FirstName = student.FirstName;
                oldStudent.LastName = student.LastName;
                oldStudent.Email = student.Email;
                oldStudent.PhoneNumber = student.PhoneNumber;
                oldStudent.UserName = student.UserName;
                await _studentRepository.UpdateAsync(oldStudent);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
