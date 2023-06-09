﻿using MediatR;

namespace EquipmentManagement.Application.Abstractions;

public interface ICommand : IRequest
{

}

public interface ICommand<TResponse> : IRequest<TResponse>
{

}
