using SupportForSchoolActivities.DAL.Interfaces.EntitiesInterfaces;
using SupportForSchoolActivities.DAL.Repository.EntitiesRepositories;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.EntityServices
{
    public class RemarkService : IRemarkService
    {
        private readonly IRemarkRepository _remarkRepository;

        public RemarkService(IRemarkRepository remarkRepository)
        {
            _remarkRepository = remarkRepository;
        }

        public async Task<bool> CreateRemark(Remark remark)
        {
            try
            {
                await _remarkRepository.CreateAsync(remark);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRemark(int id)
        {
            var remark = await _remarkRepository.GetAsync(id);
            if (remark == null)
            {
                return false;
            }
            try
            {
                await _remarkRepository.DeleteAsync(remark);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Remark>> GetAllRemarks()
        {
            return await _remarkRepository.SelectAsync();
        }

        public async Task<Remark> GetRemark(int id)
        {
            return await _remarkRepository.GetAsync(id);
        }

        public async Task<bool> UpdateRemark(int id, Remark remark)
        {
            if (remark == null || id == 0)
            {
                return false;
            }
            try
            {
                var oldRemark = await _remarkRepository.GetAsync(id);
                oldRemark.Description = remark.Description;
                oldRemark.Student = remark.Student;
                oldRemark.Date = remark.Date;
                await _remarkRepository.UpdateAsync(oldRemark);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
