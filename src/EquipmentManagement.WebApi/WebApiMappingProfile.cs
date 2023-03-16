using AutoMapper;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.WebApi.Requests;

namespace EquipmentManagement.WebApi
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {            
            CreateMap<AddEmployeeRequest, AddEmployeeCommand>();
            CreateMap<AddEquipmentRequest, AddEquipmentCommand>();
        }
    }
}
