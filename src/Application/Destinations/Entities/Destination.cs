namespace CleanMinimalApi.Application.Destinations.Entities;
public record Destination(Guid Id, string Name, string Description, DateTime DateCreated, DateTime DateModified);
