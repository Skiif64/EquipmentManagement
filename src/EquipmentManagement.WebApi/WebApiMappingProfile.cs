using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Application.Statuses.Add;
using EquipmentManagement.Auth;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.WebApi;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<AddEmployeeRequest, AddEmployeeCommand>();
        CreateMap<AddEquipmentRequest, AddEquipmentCommand>();
        CreateMap<AddEquipmentRecordRequest, AddEquipmentRecordCommand>();
        CreateMap<AddStatusRequest, AddStatusCommand>();
        CreateMap<UpdateEquipmentRecordRequest, UpdateEquipmentRecordCommand>();

        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Equipment, EquipmentResponse>()
            .ForMember(dest => dest.LastRecordId,
            opt => opt.MapFrom(src => (src.LastRecord != null) ? src.LastRecord.Id : default));
        CreateMap<EquipmentWithStatus, EquipmentWithStatusResponse>()
            .ForMember(dest => dest.LastRecordId,
            opt => opt.MapFrom(src => (src.LastRecord != null) ? src.LastRecord.Id : default))
            .ForMember(dest => dest.EmployeeId,
            opt => opt.MapFrom(src => (src.LastRecord != null) ? src.LastRecord.EmployeeId : null))
            .IncludeBase<Equipment, EquipmentResponse>();
        CreateMap<EquipmentRecord, EquipmentRecordResponse>();
        CreateMap<Status, StatusResponse>();
        CreateMap<AuthentificationResult, AuthentificationResponse>();

        CreateMap<RegisterRequest, ApplicationUser>()
            .ConstructUsing(src => ApplicationUser.Create(src.Login, src.Password, src.Role));

    }
}
