using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface ISchoolClassService
    {
        Task<List<SchoolClass>> GetAllClasses();
        Task<List<SchoolClass>> GetOneYearClasses(int number);
        Task<bool> CreateClass(SchoolClass schoolClass);
        Task<bool> DeleteClass(int id);
        Task<SchoolClass> GetClass(int id);
        Task<bool> UpdateClass(int id, SchoolClass schoolClass);
        Task<int> GetIdClassByName(string name);

    }
}
