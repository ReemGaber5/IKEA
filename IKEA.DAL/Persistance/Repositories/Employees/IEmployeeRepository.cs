﻿using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
    
    }
}
