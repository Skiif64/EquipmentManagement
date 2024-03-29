﻿using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentDtoQueryHandler
    : IQueryHandler<GetEquipmentDtoQuery, PagedList<EquipmentDto>>
{
    private const int Page = 1;
    private const int PageSize = 10;    
    private readonly IApplicationDbContext _context;    

    public GetEquipmentDtoQueryHandler(IApplicationDbContext context)
    {
        _context = context;        
    }

    public async Task<PagedList<EquipmentDto>> Handle(GetEquipmentDtoQuery request,
        CancellationToken cancellationToken)
    {
        var filters = request.Filter?.Split("-");
        string? typeFilter = null;
        string? statusFilter = null;
        if(filters is not null)
        {
            if(filters.Length > 0)
                typeFilter = filters[0];
            if(filters.Length > 1)
                statusFilter = filters[1];
        }
        var records = _context
            .Set<EquipmentRecord>()  
            .AsNoTracking()
            .Include(x => x.Status)
            .Include(x => x.Employee)
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Type)
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Images)
            .OrderByDescending(x => x.Modified)
            .AsEnumerable()
            .DistinctBy(x => x.EquipmentId)                        
            .Select(x => new EquipmentDto
            {
                Id = x.Equipment.Id,
                Article = x.Equipment.Article,
                Author = x.Equipment.Author,
                CreatedAt = x.Equipment.CreatedAt,
                CurrentEmployee = x.Employee,
                CurrentStatus = x.Status,
                Description = x.Equipment.Description,
                Images = x.Equipment.Images,
                SerialNumber = x.Equipment.SerialNumber,
                Type = x.Equipment.Type,
                TypeId = x.Equipment.TypeId,
            })
            .Where(record => SearchSelector(record, request.SearchCriteria))
            .Where(record => EquipmentTypeSearchSelector(record, typeFilter) 
            && EquipmentStatusSearchSelector(record, statusFilter))
            
            .AsQueryable()
            ?? throw new NotFoundException("Records");        
            

        int page = request.Page ?? Page;
        int pageSize = request.PageSize ?? PageSize;
        return PagedList<EquipmentDto>.Create(records, page, pageSize);
    }

    private bool SearchSelector(EquipmentDto item, string? criteria)
    {        
        var query = criteria ?? string.Empty;
        const StringComparison comparsion = StringComparison.InvariantCultureIgnoreCase;

        if (item.Type.Name.Contains(query, comparsion))
            return true;
        if (item.CurrentStatus?.Title.Contains(query, comparsion) ?? false)
            return true;
        if (item.Article.Contains(query, comparsion))
            return true;
        if (item.SerialNumber.Contains(query, comparsion))
            return true;
        if (item.CurrentEmployee?.Fullname.Contains(query, comparsion) ?? false)
            return true;
        if (item.Description?.Contains(query, comparsion) ?? false)
            return true;

        return false;
    }

    private bool EquipmentTypeSearchSelector(EquipmentDto equipment, string? typeFilter)
    {
        if (string.IsNullOrWhiteSpace(typeFilter))
            return true;

        return equipment.Type.Name
            .Contains(typeFilter, StringComparison.InvariantCultureIgnoreCase);
    }

    private bool EquipmentStatusSearchSelector(EquipmentDto equipment, string? statusFilter)
    {
        if (string.IsNullOrWhiteSpace(statusFilter))
            return true;

        return equipment.CurrentStatus?.Title
            .Contains(statusFilter, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
}
