using AutoMapper;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Statuses.Add;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<AddEmployeeCommand, Employee>();
        CreateMap<AddEquipmentCommand, Equipment>();
        CreateMap<AddEquipmentRecordCommand, EquipmentRecord>();
        CreateMap<AddStatusCommand, Status>();
    }
}
