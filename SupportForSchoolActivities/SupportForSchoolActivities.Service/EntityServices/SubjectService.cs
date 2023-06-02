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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<bool> Create(Subject subject)
        {
            var subjects = await _subjectRepository.SelectAsync();
            var sub = subjects.FirstOrDefault(s => s.Name == subject.Name);
            if (sub != null)
            {
                return false;
            }
            try
            {
                await _subjectRepository.CreateAsync(subject);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var subject = await _subjectRepository.GetAsync(id);
            if(subject == null)
            {
                return false;
            }
            try
            {
                await _subjectRepository.DeleteAsync(subject);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _subjectRepository.SelectAsync();
        }

        public async Task<Subject> GetSubject(int id)
        {
            return await _subjectRepository.GetAsync(id);
        }

        public async Task<bool> UpdateSubject(int id, Subject subject)
        {
            if (subject == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldClass = await _subjectRepository.GetAsync(id);
                oldClass.Name = subject.Name;
                await _subjectRepository.UpdateAsync(oldClass);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
