using HCM.Web.Models.Auth;
using HCM.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HCM.Web.Components.Pages;

public partial class Login : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected AuthStateService AuthState { get; set; } = default!;

    protected LoginRequest LoginModel = new();
    protected string? ErrorMessage;

    protected async Task HandleLogin()
    {
        try
        {
            var response = await Http.PostAsJsonAsync("/api/auth/login", LoginModel);
            if (response.IsSuccessStatusCode)
            {
                Nav.NavigateTo("/");
                await AuthState.LoadUserAsync();
                Nav.NavigateTo("/");
            }
            else
            {
                ErrorMessage = "Invalid credentials";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Login failed: " + ex.Message;
        }
    }
}