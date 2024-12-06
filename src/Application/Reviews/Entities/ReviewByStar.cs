namespace CleanMinimalApi.Application.Reviews.Entities;

using Application.Authors.Entities;
using Application.Movies.Entities;

public record ReviewByStar(
    Guid Id,
    int Stars,
    ReviewedMovie ReviewedMovie = null,
    ReviewAuthor ReviewAuthor = null);
