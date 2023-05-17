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
    public class ParentRepository : BaseUsersRepository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
