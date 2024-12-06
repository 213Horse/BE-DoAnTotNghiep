namespace CleanMinimalApi.Application.Reviews.Queries.GetReviewByStar;

using Entities;
using MediatR;

public class GetReviewByStarQuery : IRequest<List<ReviewByStar>>
{
    public int Stars { get; init; }
}
