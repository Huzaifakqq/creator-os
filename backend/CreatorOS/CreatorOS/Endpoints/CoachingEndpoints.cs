using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class CoachingEndpoints
{
    public static void MapCoachingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/coaching/slots", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var slots = await db.CoachingSlots.Where(s => s.TenantId == tenantId && s.IsActive)
                .Select(s => new { s.Id, s.Title, s.Description, s.DurationMinutes, s.PriceCents, s.SessionType, s.AvailableDays, s.TimeZone })
                .ToListAsync();
            return Results.Ok(slots);
        }).WithName("GetCoachingSlots").WithTags("Coaching");

        app.MapPost("/api/coaching/slots", [Authorize] async (CreateSlotRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var slot = new CoachingSlot
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, CoachId = userId,
                Title = req.Title, Description = req.Description,
                DurationMinutes = req.DurationMinutes ?? 60, PriceCents = req.PriceCents,
                SessionType = req.SessionType ?? "video", MaxParticipants = 1,
                AvailableDays = req.AvailableDays, AvailableTimeSlots = req.AvailableTimeSlots,
                TimeZone = req.TimeZone ?? "UTC", BufferMinutes = 15, IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            db.CoachingSlots.Add(slot);
            await db.SaveChangesAsync();
            return Results.Created($"/api/coaching/slots/{slot.Id}", new { slot.Id, slot.Title });
        }).WithName("CreateCoachingSlot").WithTags("Coaching");

        app.MapPut("/api/coaching/slots/{id}", [Authorize] async (Guid id, UpdateSlotRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var slot = await db.CoachingSlots.FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId);
            if (slot == null) return Results.NotFound();
            if (req.Title != null) slot.Title = req.Title;
            if (req.Description != null) slot.Description = req.Description;
            if (req.PriceCents.HasValue) slot.PriceCents = req.PriceCents.Value;
            if (req.AvailableDays != null) slot.AvailableDays = req.AvailableDays;
            slot.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { slot.Id, slot.Title });
        }).WithName("UpdateCoachingSlot").WithTags("Coaching");

        app.MapGet("/api/coaching/bookings", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var bookings = await db.CoachingBookings.Where(b => b.TenantId == tenantId)
                .OrderByDescending(b => b.CreatedAt).Take(50)
                .Select(b => new { b.Id, b.SlotId, b.ClientId, b.SessionDate, b.SessionTime, b.DurationMinutes, b.Status, b.MeetingUrl })
                .ToListAsync();
            return Results.Ok(bookings);
        }).WithName("GetCoachingBookings").WithTags("Coaching");

        app.MapPost("/api/coaching/slots/{slotId}/book", [Authorize] async (Guid slotId, BookSessionRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var slot = await db.CoachingSlots.FirstOrDefaultAsync(s => s.Id == slotId && s.TenantId == tenantId);
            if (slot == null) return Results.NotFound();
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var booking = new CoachingBooking
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, SlotId = slotId,
                ClientId = userId, SessionDate = req.SessionDate, SessionTime = req.SessionTime,
                DurationMinutes = slot.DurationMinutes, Status = "confirmed",
                IntakeFormData = req.IntakeFormData, ReminderSent = false,
                CreatedAt = DateTime.UtcNow
            };
            db.CoachingBookings.Add(booking);
            await db.SaveChangesAsync();
            return Results.Created($"/api/coaching/bookings/{booking.Id}", new { booking.Id, booking.Status });
        }).WithName("BookCoachingSession").WithTags("Coaching");

        app.MapPut("/api/coaching/bookings/{id}/cancel", [Authorize] async (Guid id, CancelBookingRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var booking = await db.CoachingBookings.FirstOrDefaultAsync(b => b.Id == id && b.TenantId == tenantId);
            if (booking == null) return Results.NotFound();
            booking.Status = "canceled";
            booking.CancellationReason = req.Reason;
            booking.CanceledAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { booking.Id, booking.Status });
        }).WithName("CancelCoachingBooking").WithTags("Coaching");

        app.MapPost("/api/coaching/bookings/{bookingId}/notes", [Authorize] async (Guid bookingId, CreateNoteRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var note = new CoachingSessionNote
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, BookingId = bookingId,
                AuthorId = userId, NoteType = req.NoteType ?? "general",
                Content = req.Content, IsPrivate = req.IsPrivate, CreatedAt = DateTime.UtcNow
            };
            db.CoachingSessionNotes.Add(note);
            await db.SaveChangesAsync();
            return Results.Created($"/api/coaching/notes/{note.Id}", new { note.Id });
        }).WithName("CreateCoachingNote").WithTags("Coaching");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateSlotRequest(string Title, string? Description, int? DurationMinutes, long PriceCents, string? SessionType, string? AvailableDays, string? AvailableTimeSlots, string? TimeZone);
public record UpdateSlotRequest(string? Title, string? Description, long? PriceCents, string? AvailableDays);
public record BookSessionRequest(DateTime SessionDate, string SessionTime, string? IntakeFormData);
public record CancelBookingRequest(string? Reason);
public record CreateNoteRequest(string Content, string? NoteType, bool IsPrivate);
