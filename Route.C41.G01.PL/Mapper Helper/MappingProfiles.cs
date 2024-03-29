using AutoMapper;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.DAL.Models.ViewModels;

namespace Route.C41.G01.PL.Mapper_Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
