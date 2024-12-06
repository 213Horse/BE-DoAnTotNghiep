/*namespace CleanMinimalApi.Application.Destinations.Commands.CreateDestination;

using CleanMinimalApi.Application.Destinations.Entities;
using CleanMinimalApi.Application.Destinations;
using MediatR;

public class CreateDestinationHandler(
    IDestinationsRepository destinationsRepository) : IRequestHandler<CreateDestinationCommand, Destination>
{
    public async Task<Destination> Handle(CreateDestinationCommand request, CancellationToken cancellationToken)
    {
        return await destinationsRepository
            .CreateDestination(request.Name, request.Description, cancellationToken);
    }
}

*/
