﻿using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.ApplicationUsers.Register;
using EquipmentManagement.Application.ApplicationUsers.SignIn;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.Employees.Update;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments;
using EquipmentManagement.Application.Equipments.Create;
using EquipmentManagement.Application.Equipments.Update;
using EquipmentManagement.Application.EquipmentTypes.Add;
using EquipmentManagement.Application.Images.RemoveByNames;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Application.Statuses.Add;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.WebApi;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<AddEmployeeRequest, AddEmployeeCommand>();
        CreateMap<AddEquipmentRequest, CreateEquipmentCommand>();
        CreateMap<AddEquipmentRecordRequest, AddEquipmentRecordCommand>();
        CreateMap<AddStatusRequest, AddStatusCommand>();
        CreateMap<UpdateEquipmentRecordRequest, UpdateEquipmentRecordCommand>();
        CreateMap<AddEquipmentTypeRequest, AddEquipmentTypeCommand>();
        CreateMap<UpdateEmployeeRequest, UpdateEmployeeCommand>();
        CreateMap<DeleteImagesRequest, RemoveImagesCommand>();
        CreateMap<UpdateEquipmentRequest, UpdateEquipmentCommand>();

        CreateMap(typeof(PagedList<>), typeof(PagedListResponse<>));

        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Equipment, EquipmentResponse>()            
            .ForMember(dest => dest.Type,
            opt => opt.MapFrom(src => src.Type.Name));

        CreateMap<EquipmentDto, EquipmentResponse>()
            .IncludeBase<Equipment, EquipmentResponse>()
            .ForMember(dest => dest.EmployeeFullname, 
            opt => opt.MapFrom(src => (src.CurrentEmployee != null) ? src.CurrentEmployee.Fullname : default));
              
        CreateMap<EquipmentRecord, EquipmentRecordResponse>()
            .ForMember(dest => dest.EmployeeFullname,
            opt => opt.MapFrom(src => (src.Employee != null) ? src.Employee.Fullname : default))
            .ForMember(dest => dest.StatusTitle,
            opt => opt.MapFrom(src => src.Status.Title));
        CreateMap<Status, StatusResponse>();
        CreateMap<AuthenticationResult, AuthentificationResponse>();
        CreateMap<EmployeeStatus, EmployeeStatusResponse>();

        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginRequest, SignInCommand>();
        CreateMap<ApplicationUser, ApplicationUserResponse>();
        CreateMap<JournalRecord, JournalRecordResponse>();
        CreateMap<EquipmentType, EquipmentTypeResponse>();
    }
}
