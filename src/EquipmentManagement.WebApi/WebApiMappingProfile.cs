using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Application.Statuses.Add;
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
            opt => opt.MapFrom(src => src.LastRecord));
        CreateMap<EquipmentWithStatus, EquipmentWithStatusResponse>()
            .ForMember(dest => dest.LastRecordId,
            opt => opt.MapFrom(src => src.LastRecord));            
        CreateMap<Status, StatusResponse>();
    }
}
