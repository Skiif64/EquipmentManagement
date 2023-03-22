using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments.Add;
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
            .ForMember(dest => dest.StatusId,
            opt => opt.MapFrom(src => src.LastRecord.Status.Id))
            .ForMember(dest => dest.StatusId,
            opt => opt.MapFrom(src => src.LastRecord.Status.Title));
        CreateMap<Status, StatusResponse>();
    }
}
