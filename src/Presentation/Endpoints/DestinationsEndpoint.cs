namespace CleanMinimalApi.Presentation.Endpoints;

using CleanMinimalApi.Presentation.Filters;
using Entities = Application.Destinations.Entities;
using Queries = Application.Destinations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanMinimalApi.Application.Common.Exceptions;
using CleanMinimalApi.Presentation.Requests;
using CleanMinimalApi.Application.Destinations.Commands.CreateDestination;
using Microsoft.AspNetCore.Authorization;

public static class DestinationsEndpoint
{
    public static WebApplication MapDestinationsEndpoint(this WebApplication app)
    {
        var root = app.MapGroup("/api/destination")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("destination")
            .WithDescription("Lookup and Find Destinations")
            .WithOpenApi();


        _ = root.MapGet("/", GetDestinations)
          .Produces<List<Entities.Destination>>()
          .ProducesProblem(StatusCodes.Status500InternalServerError)
          .WithSummary("Lookup all Authors")
          .RequireAuthorization()
          .WithDescription("\n    GET /destinations");

        _ = root.MapPost("/", CreateDestination)
            .Produces<Entities.Destination>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Create a Destination");
        //.WithDescription("\n    POST /destination\n     {         \"name\": 5       }", "\"description\": 5       }");

        return app;
    }
    public static async Task<IResult> GetDestinations([FromServices] IMediator mediator)
    {
        try
        {
            return Results.Ok(await mediator.Send(new Queries.GetDestinations.GetDestinationsQuery()));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> CreateDestination([Validate][FromBody] CreateDestinationRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new CreateDestinationCommand
            {
                Name = request.Name,
                Description = request.Description
            });

            return Results.Created($"/api/destination/{response.Id}", response);
        }
        catch (NotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

}
