﻿using AutoMapper;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.WebApi.Requests;

namespace EquipmentManagement.WebApi
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<AddEmployeeCommand, Employee>();
            CreateMap<AddEmployeeRequest, AddEmployeeCommand>();            
        }
    }
}
