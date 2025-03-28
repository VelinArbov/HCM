using HCM.Domain.Models;
using HCM.Domain.Models.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace HCM.ApiService.Endpoints;

public static class PeopleEndpoints
{
    public static IEndpointRouteBuilder MapPeopleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/people")
            .RequireAuthorization();

        group.MapGet("/", [Authorize(Roles = "HR Admin,Manager")] async (IPeopleService service) =>
        {
            var people = await service.GetAllAsync();
            return Results.Ok(people);
        });

        group.MapGet("/{id:guid}", [Authorize(Roles = "HR Admin,Manager,Employee")]
            async (Guid id, IPeopleService service) =>
            {
                var person = await service.GetByIdAsync(id);
                return person is null ? Results.NotFound() : Results.Ok(person);
            });

        group.MapPost("/", [Authorize(Roles = "HR Admin")] async (Person person, IPeopleService service) =>
        {
            var created = await service.CreateAsync(person);
            return Results.Created($"/api/people/{created.Id}", created);
        });

        group.MapPut("/{id:guid}", [Authorize(Roles = "HR Admin")]
            async (Guid id, Person updated, IPeopleService service) =>
            {
                var result = await service.UpdateAsync(id, updated);
                return result ? Results.NoContent() : Results.NotFound();
            });

        group.MapDelete("/{id:guid}", [Authorize(Roles = "HR Admin")] async (Guid id, IPeopleService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}