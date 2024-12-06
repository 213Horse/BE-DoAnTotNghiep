namespace CleanMinimalApi.Application.Destinations.Queries.GetDestinations;

using Entities;
using MediatR;

public class GetDestinationsQuery : IRequest<List<Destination>>
{
}
