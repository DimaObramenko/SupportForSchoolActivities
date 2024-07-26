using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces.EntityInterfaces
{
    public interface IGradeService
    {
        Task<bool> CreateGrade(Grade grade);
        Task<bool> UpdateGrade(int id, Grade grade);
        Task<bool> DeleteGrade(int id);
        Task<Grade> GetGradeById(int id);
        Task<List<Grade>> GetAllGrades();
    }
}
