using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<bool> Create(Subject subject);
        Task<bool> Delete(int id);
        Task<Subject> GetSubject(int id);
        Task<bool> UpdateSubject(int id, Subject subject);
    }
}
