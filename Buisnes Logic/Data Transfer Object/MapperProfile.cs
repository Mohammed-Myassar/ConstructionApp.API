using AutoMapper;
using Domain.Entities;

namespace BuisnesLogic.Data_Transfer_Object
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Mapper bitween ConstructionProjectDTO <-> ConstructionProject
            CreateMap<ConstructionProjectDTO, ConstructionProject>();
            CreateMap<ConstructionProject, ConstructionProjectDTO>();

            // Mapper bitween ProjectTaskDTO <-> ProjectTask
            CreateMap<ProjectTaskDTO, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskDTO>();

            // Mapper bitween EmployeeDTO <-> Employee
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();

            // Mapper bitween ResourceDTO <-> Resource
            CreateMap<ResourceDTO, Resource>();
            CreateMap<Resource, ResourceDTO>();

            // Mapper bitween ResourceUsageDTO <-> ResourceUsage
            CreateMap<ResourceUsageDTO, ResourceUsage>();
            CreateMap<ResourceUsage, ResourceUsageDTO>();

            // Mapper bitween PaymentTransactionDTO <-> PaymentTransaction
            CreateMap<PaymentTransactionDTO, PaymentTransaction>();
            CreateMap<PaymentTransaction, PaymentTransactionDTO>();
        }
    }
}
