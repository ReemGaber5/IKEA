using AutoMapper;
using IKEA.BLL.DTOs.Departments;
using IKEA.PL.ViewModel;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentViewModel, CreatedDepartmentDTO>().ReverseMap();

            CreateMap<DepartmentDetailsDTO, DepartmentViewModel>().ReverseMap();

            CreateMap<UpdatedDepartmentDTO, DepartmentViewModel>().ReverseMap();


        }
    }
}
