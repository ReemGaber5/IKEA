﻿using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepo : GenericRepository<Department>, IDepartment
    {
        private readonly ApplicationDbContext _Context;

        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
            {
                _Context = context;
            }


        }
    }
}

