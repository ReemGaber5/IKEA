using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Employees
{
    public class CreatedEmployeeDTO
    {
        [MaxLength(50,ErrorMessage ="Max Lenght Of Name is 50 Chars")]
        [MinLength(3, ErrorMessage = "Min Lenght Of Name is 3 Chars")]
        public string Name { get; set; }
        [Range(22,30)]
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal salary { get; set; }
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Numebr")]
        [Phone]
        public string? PhoneNUmber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
