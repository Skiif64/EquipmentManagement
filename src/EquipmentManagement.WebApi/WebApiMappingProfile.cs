using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments.Add;

namespace EquipmentManagement.WebApi;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {            
        CreateMap<AddEmployeeRequest, AddEmployeeCommand>();
        CreateMap<AddEquipmentRequest, AddEquipmentCommand>();
        CreateMap<AddEquipmentRecordRequest, AddEquipmentRecordCommand>();
        CreateMap<UpdateEquipmentRecordRequest, UpdateEquipmentRecordCommand>();
    }
}
