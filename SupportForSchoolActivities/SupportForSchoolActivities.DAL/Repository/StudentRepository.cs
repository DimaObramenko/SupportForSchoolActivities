using Microsoft.EntityFrameworkCore;
using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.DAL.Repository.BaseRepositories;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Repository
{
    public class StudentRepository : BaseUsersRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<bool> CreateAsync(Student student, Parent parent)
        {
            try
            {
                student.Parent = parent;
                await _db.Student.AddAsync(student);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<List<Student>> SelectAsync()
        {
            return await _db.Student
                .AsNoTracking()
                .Include(s => s.Parent)
                .Include(s => s.SchoolClass)
                .ToListAsync();
        }
    }
}
