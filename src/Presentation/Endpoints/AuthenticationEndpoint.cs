namespace CleanMinimalApi.Presentation.Endpoints;

using CleanMinimalApi.Application.Common.Exceptions;
using CleanMinimalApi.Presentation.Filters;
using CleanMinimalApi.Presentation.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Entities = Application.Authentications.Entities;
using Commands = Application.Authentications.Commands;
using Queries = Application.Authentications.Queries;
using System.IdentityModel.Tokens.Jwt;

public static class AuthenticationEndpoint
{
    public static WebApplication MapAuthenticationEndpoint(this WebApplication app)
    {
        var root = app.MapGroup("/api/authentication")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("authentication")
            .WithDescription("Authentication")
            .WithOpenApi();

        _ = root.MapPost("/register", Register)
            .Produces<Entities.ApplicationUser>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Register an Account");

        _ = root.MapGet("/email-confirm/{userId}/{token}", EmailConfirm)
            .Produces<Entities.ApplicationUser>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Confirm an Email");

        _ = root.MapPost("/login", Login)
           .Produces<Entities.ApplicationUser>(StatusCodes.Status201Created)
           .ProducesProblem(StatusCodes.Status500InternalServerError)
           .ProducesValidationProblem()
           .WithSummary("Login");
        return app;
    }
    public static async Task<IResult> Register([Validate][FromBody] RegisterRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new Commands.Register.RegisterCommand
            {
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
            });
            return Results.Created($"/api/register/{response}", response);
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

    public static async Task<IResult> EmailConfirm(string userId, [FromRoute] string token, [FromServices] IMediator mediator)
    {
        try
        {
            var response = await mediator.Send(new Commands.EmailConfirm.EmailConfirmCommand
            {
                UserId = userId,
                Token = Uri.UnescapeDataString(token)
            });
            return Results.Created($"/api/email-confirm/{response}", response);
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

    public static async Task<IResult> Login([Validate][FromBody] LoginRequest request, [FromServices] IMediator mediator)
    {
        try
        {
            var token = await mediator.Send(new Queries.Login.LoginQuery
            {
                Username = request.Username,
                Password = request.Password,
            });
            if (token != null)
            {
                return Results.Created($"/api/login/", new JwtSecurityTokenHandler().WriteToken(token));

            }
            throw new NotFoundException("Username or password does not correct!");
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
