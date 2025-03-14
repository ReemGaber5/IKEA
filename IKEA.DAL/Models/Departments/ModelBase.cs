using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Departments
{
    public class ModelBase
    {
        public int Id { get; set; } 
        public bool IsDeleted { get; set; } 
        public int CreatedBy {  get; set; }
        public DateTime Createdon { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedon { get; set; }




    }
}
