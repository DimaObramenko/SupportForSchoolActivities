﻿using SupportForSchoolActivities.DAL.Interfaces.BaseInterfaces;
using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.DAL.Interfaces
{
    public interface IStudentRepository : IBaseUsersRepository<Student>
    {
        Task<bool> CreateAsync(Student student, Parent parent);
    }
}
