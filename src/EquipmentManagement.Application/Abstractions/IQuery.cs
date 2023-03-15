using MediatR;

namespace EquipmentManagement.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
