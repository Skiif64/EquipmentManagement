﻿using AutoMapper;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.WebApi.Requests;

namespace EquipmentManagement.WebApi;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {            
        CreateMap<AddEmployeeRequest, AddEmployeeCommand>();
        CreateMap<AddEquipmentRequest, AddEquipmentCommand>();
        CreateMap<AddEquipmentRecordRequest, AddEquipmentRecordCommand>();
    }
}
