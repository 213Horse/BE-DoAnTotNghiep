namespace CleanMinimalApi.Application.Authors.Queries.GetAuthors;

using Entities;
using MediatR;

public class GetDestinationsQuery : IRequest<List<Author>>
{
}
