namespace CleanMinimalApi.Application.Destinations.Commands.CreateDestination;

using Entities;
using MediatR;

public class CreateDestinationCommand : IRequest<Destination>
{
    public string Name { get; init; }
    public string Description { get; init; }
}
