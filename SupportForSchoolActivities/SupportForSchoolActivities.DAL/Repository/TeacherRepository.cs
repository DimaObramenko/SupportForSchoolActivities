﻿using Microsoft.EntityFrameworkCore;
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
    public class TeacherRepository : BaseUsersRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<Teacher>> SelectAsync()
        {
            return await _db.Teacher
                .AsNoTracking()
                .Include(t => t.Subjects)
                .ToListAsync();
        }
    }
}
