﻿using EquipmentManagement.Application.Models;
using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application.Abstractions;
public interface IJournal
{
    Task WriteAsync(EventId eventId,
        string message,
        string? user = null,
        DateTimeOffset? created = null,
        CancellationToken cancellationToken = default);
}
