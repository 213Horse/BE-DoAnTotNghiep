namespace CleanMinimalApi.Application.Authors.Queries.GetAuthors;

using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;

public class GetDestinationsHandler(IAuthorsRepository repository) : IRequestHandler<GetDestinationsQuery, List<Author>>
{
    public async Task<List<Author>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAuthors(cancellationToken);
    }
}
