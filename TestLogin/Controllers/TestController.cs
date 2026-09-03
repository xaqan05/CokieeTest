using Microsoft.AspNetCore.Mvc;

namespace TestLogin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Set()
    {
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI5YjAyNmRmLWRlZDYtNDEzZi1iNDljLTZjNTVmMDI2NDY2ZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjEiLCJuYmYiOjE3ODg0MzcxMTUsImV4cCI6MTc4ODQzODAxNSwiaXNzIjoiaHR0cHM6Ly9oaXJpLmF6IiwiYXVkIjoiSGlyaU1pY3Jvc2VydmljZXMifQ.2frMKFRwiVlNJMrryjokgRKMVxzInrfNfabW5Uxmos8";

        string refreshToken = "erkuwQ1UUfVmc/bdZCKyD4SpRRjaWXM6OQpDvmv3f37vFByPIwIJd2716VeHCH7r1z9RFCqYtVucozvVqysoAw==";

        SetTokenCookies(token, refreshToken);

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {

        var token = Request.Cookies["refresh_token"];

        if (string.IsNullOrWhiteSpace(token))
            token = "salam";

        // LogoutAsync boş tokendə heç nə silmir və səssizcə uğur qaytarır — sessiya açıq qalardı.
        return Ok(token);
    }

    private void SetTokenCookies(string accesToken, string refreshToken)
    {
        Response.Cookies.Append(
            "access_token",
            accesToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddMinutes(150)
            });

        Response.Cookies.Append(
            "refresh_token",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddMinutes(150)
            });
    }
}
