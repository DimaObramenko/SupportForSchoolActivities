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
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;

        public ParentService(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<bool> CreateParent(Parent parent)
        {
            try
            {
                await _parentRepository.CreateAsync(parent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteParent(string id)
        {
            var parent = await _parentRepository.GetAsync(id);
            if(parent == null)
            {
                return false;
            }
            try
            {
                await _parentRepository.DeleteAsync(parent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Parent>> GetAllParents()
        {
            return await _parentRepository.SelectAsync();
        }

        public async Task<Parent> GetParent(string id)
        {
            return await _parentRepository.GetAsync(id);
        }

        public async Task<bool> UpdateParent(string id, Parent parent)
        {
            if (parent == null || id == null)
            {
                return false;
            }
            try
            {
                var oldParent = await _parentRepository.GetAsync(id);
                oldParent.FirstName = parent.FirstName;
                oldParent.LastName = parent.LastName;
                oldParent.Email = parent.Email;
                oldParent.PhoneNumber = parent.PhoneNumber;
                oldParent.UserName = parent.UserName;
                await _parentRepository.UpdateAsync(oldParent);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
