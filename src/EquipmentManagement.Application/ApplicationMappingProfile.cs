using AutoMapper;
using EquipmentManagement.Application.ApplicationUsers.Register;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.EquipmentTypes.Add;
using EquipmentManagement.Application.Models;
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

        CreateMap<RegisterCommand, ApplicationUser>()
            .ConstructUsing(src => ApplicationUser.Create(src.Login, src.Password, src.Role));

        CreateMap<AddEquipmentTypeCommand, EquipmentType>();
    }
}
