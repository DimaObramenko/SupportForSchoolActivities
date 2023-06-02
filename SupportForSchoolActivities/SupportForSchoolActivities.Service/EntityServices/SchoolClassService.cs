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
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _classRepository;

        public SchoolClassService(ISchoolClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<bool> CreateClass(SchoolClass schoolClass)
        {
            var sClasses = await _classRepository.SelectAsync();
            var sClass = sClasses.FirstOrDefault(c => c.Name == schoolClass.Name);
            if(sClass != null)
            {
                return false;
            }
            try
            {
                await _classRepository.CreateAsync(schoolClass);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteClass(int id)
        {
            var schoolClass = await _classRepository.GetAsync(id);
            if(schoolClass == null)
            {
                return false;
            }
            try
            {
                await _classRepository.DeleteAsync(schoolClass);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<SchoolClass>> GetAllClasses()
        {
            return await _classRepository.SelectAsync();
        }

        public async Task<SchoolClass> GetClass(int id)
        {
            return await _classRepository.GetAsync(id);
        }

        public async Task<int> GetIdClassByName(string name)
        {
            var allClasses = await _classRepository.SelectAsync();
            var schoolClass = allClasses.FirstOrDefault(c => c.Name == name);
            
            return schoolClass.Id;
        }

        public async Task<List<SchoolClass>> GetOneYearClasses(int number)
        {
            var allSchoolClasses = await _classRepository.SelectAsync();
            var schoolClasses = allSchoolClasses.Where(c => c.ClassNumber == number).ToList();
            return schoolClasses;
        }

        public async Task<bool> UpdateClass(int id, SchoolClass schoolClass)
        {
            if(schoolClass == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldClass = await _classRepository.GetAsync(id);
                oldClass.Name= schoolClass.Name;
                oldClass.ClassNumber = schoolClass.ClassNumber;
                await _classRepository.UpdateAsync(oldClass);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
