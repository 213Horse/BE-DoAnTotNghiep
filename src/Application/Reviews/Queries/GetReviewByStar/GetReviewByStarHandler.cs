namespace CleanMinimalApi.Application.Reviews.Queries.GetReviewByStar;

using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exceptions;
using Entities;
using MediatR;

public class GetReviewByStarHandler(IReviewsRepository repository) : IRequestHandler<GetReviewByStarQuery, List<ReviewByStar>>
{
    public async Task<List<ReviewByStar>> Handle(GetReviewByStarQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetReviewByStar(request.Stars, cancellationToken);

        NotFoundException.ThrowIfNull(result, EntityType.Review);

        return result;
    }
}
