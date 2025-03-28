using HCM.Domain.Models;
using HCM.Domain.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HCM.ApiService.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/login", async (
            LoginRequest request,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
                return Results.Unauthorized();

            var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Results.Unauthorized();

            return Results.Ok(new { message = "Login successful" });
        });

        group.MapPost("/logout", async (
            SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok(new { message = "Logged out" });
        });

        group.MapGet("/me", [Authorize] async (
            UserManager<ApplicationUser> userManager,
            HttpContext httpContext) =>
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user == null)
                return Results.Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            return Results.Ok(new
            {
                user.Id,
                user.Email,
                user.FullName,
                Roles = roles
            });
        });

        return app;
    }
}