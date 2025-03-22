﻿using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public interface IDepartment:IGenericRepository<Department>
    {
       
    }
}
