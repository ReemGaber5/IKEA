﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Departments
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        [Display(Name="Date Of Creation")]
        public DateOnly C0reationDate { get; set; }
    }
}
