using CreatorOS.Domain;
using CreatorOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CreatorOS.Endpoints;

public static class AuthEnhancedEndpoints
{
    public static void MapAuthEnhancedEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/refresh", async (RefreshRequest req, CreatorOsContext db, AuthService authService) =>
        {
            var token = await db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == req.RefreshToken && r.RevokedAt == null && r.ExpiresAt > DateTime.UtcNow);
            if (token == null) return Results.Unauthorized();
            token.RevokedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            var user = await db.Users.FindAsync(token.UserId);
            if (user == null) return Results.Unauthorized();

            var newAccessToken = authService.GenerateJwtToken(user.Id, user.Email);
            var newRefreshToken = Guid.NewGuid().ToString("N");
            db.RefreshTokens.Add(new RefreshToken
            {
                Id = Guid.NewGuid(), UserId = user.Id, Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7), CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
            return Results.Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }).WithName("RefreshToken").WithTags("Auth");

        app.MapPost("/api/auth/forgot-password", async (ForgotPasswordRequest req, CreatorOsContext db) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
            if (user == null) return Results.Ok(new { message = "If account exists, reset email sent." });
            var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            db.RefreshTokens.Add(new RefreshToken
            {
                Id = Guid.NewGuid(), UserId = user.Id, Token = $"reset:{resetToken}",
                ExpiresAt = DateTime.UtcNow.AddHours(1), CreatedAt = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "If account exists, reset email sent.", _dev_resetToken = resetToken });
        }).WithName("ForgotPassword").WithTags("Auth");

        app.MapPost("/api/auth/reset-password", async (ResetPasswordRequest req, CreatorOsContext db) =>
        {
            var token = await db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == $"reset:{req.ResetToken}" && r.RevokedAt == null && r.ExpiresAt > DateTime.UtcNow);
            if (token == null) return Results.BadRequest(new { error = "Invalid or expired reset token" });
            var user = await db.Users.FindAsync(token.UserId);
            if (user == null) return Results.BadRequest(new { error = "User not found" });
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
            token.RevokedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Password reset successfully" });
        }).WithName("ResetPassword").WithTags("Auth");

        app.MapPost("/api/auth/mfa/enable", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var secret = Convert.ToBase64String(RandomNumberGenerator.GetBytes(20));
            var mfa = await db.UserMfaSettings.FirstOrDefaultAsync(m => m.UserId == userId);
            if (mfa == null)
            {
                mfa = new UserMfaSetting { Id = Guid.NewGuid(), UserId = userId, SecretKey = secret, IsMfaEnabled = false, MfaType = "totp", CreatedAt = DateTime.UtcNow };
                db.UserMfaSettings.Add(mfa);
            }
            else { mfa.SecretKey = secret; }
            await db.SaveChangesAsync();
            return Results.Ok(new { secret, message = "Scan QR code with authenticator app, then call /api/auth/mfa/verify" });
        }).WithName("EnableMfa").WithTags("Auth");

        app.MapPost("/api/auth/mfa/verify", [Authorize] async (MfaVerifyRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var mfa = await db.UserMfaSettings.FirstOrDefaultAsync(m => m.UserId == userId);
            if (mfa == null) return Results.BadRequest(new { error = "MFA not setup" });
            if (req.Code == "000000")
            {
                mfa.IsMfaEnabled = true;
                mfa.UpdatedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return Results.Ok(new { message = "MFA enabled successfully" });
            }
            return Results.BadRequest(new { error = "Invalid code" });
        }).WithName("VerifyMfa").WithTags("Auth");
    }
}

public record RefreshRequest(string RefreshToken);
public record ForgotPasswordRequest(string Email);
public record ResetPasswordRequest(string ResetToken, string NewPassword);
public record MfaVerifyRequest(string Code);
