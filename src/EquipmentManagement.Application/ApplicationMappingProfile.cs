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
        CreateMap<Equipment, EquipmentWithStatus>()
            .ForMember(dest => dest.EmployeeId, opt =>
            opt.MapFrom(src => (src.LastRecord != null)? src.LastRecord.EmployeeId : null))
            .ForMember(dest => dest.EmployeeFullname, opt =>
            opt.MapFrom(src => (src.LastRecord != null && src.LastRecord.Employee != null) 
            ? src.LastRecord.Employee.Fullname : null))
            .ForMember(dest => dest.StatusId, opt =>
            opt.MapFrom(src => (src.LastRecord != null) ? src.LastRecord.StatusId : default))
            .ForMember(dest => dest.StatusTitle, opt =>
            opt.MapFrom(src => (src.LastRecord != null && src.LastRecord.Status != null) 
            ? src.LastRecord.Status.Title : null));

        CreateMap<RegisterCommand, ApplicationUser>()
            .ConstructUsing(src => ApplicationUser.Create(src.Login, src.Password, src.Role));

        CreateMap<AddEquipmentTypeCommand, EquipmentType>();
    }
}
