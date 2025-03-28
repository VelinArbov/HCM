using Microsoft.AspNetCore.Components;

namespace HCM.Web.Services;

public class AuthStateService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _nav;

    public UserInfo? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser != null;

    public AuthStateService(HttpClient http, NavigationManager nav)
    {
        _http = http;
        _nav = nav;
    }

    public async Task LoadUserAsync()
    {
        try
        {
            var user = await _http.GetFromJsonAsync<UserInfo>("/api/auth/me");
            CurrentUser = user;
        }
        catch
        {
            CurrentUser = null;
        }
    }

    public async Task LogoutAsync()
    {
        await _http.PostAsync("/api/auth/logout", null);
        CurrentUser = null;
        _nav.NavigateTo("/login", forceLoad: true);
    }

    public class UserInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}