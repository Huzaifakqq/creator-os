using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this WebApplication app)
    {
        app.MapGet("/api/courses", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var courses = await db.Courses.Where(c => c.TenantId == tenantId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new { c.Id, c.Title, c.Description, c.Difficulty, c.IsPublished, c.EnrollmentCount, c.CompletionRate, c.CreatedAt })
                .ToListAsync();
            return Results.Ok(courses);
        }).WithName("GetCourses").WithTags("Courses");

        app.MapGet("/api/courses/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            return course != null ? Results.Ok(course) : Results.NotFound();
        }).WithName("GetCourse").WithTags("Courses");

        app.MapPost("/api/courses", [Authorize] async (CreateCourseRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var course = new Course
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Title = req.Title,
                Description = req.Description, Difficulty = req.Difficulty ?? "beginner",
                EstimatedHours = req.EstimatedHours, IsPublished = false,
                CertificateEnabled = false, EnrollmentCount = 0, CreatedAt = DateTime.UtcNow
            };
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return Results.Created($"/api/courses/{course.Id}", new { course.Id, course.Title });
        }).WithName("CreateCourse").WithTags("Courses");

        app.MapPut("/api/courses/{id}", [Authorize] async (Guid id, UpdateCourseRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            if (course == null) return Results.NotFound();
            if (req.Title != null) course.Title = req.Title;
            if (req.Description != null) course.Description = req.Description;
            if (req.Difficulty != null) course.Difficulty = req.Difficulty;
            course.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { course.Id, course.Title });
        }).WithName("UpdateCourse").WithTags("Courses");

        app.MapPost("/api/courses/{id}/publish", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var course = await db.Courses.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            if (course == null) return Results.NotFound();
            course.IsPublished = true;
            course.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { course.Id, message = "Course published" });
        }).WithName("PublishCourse").WithTags("Courses");

        app.MapGet("/api/courses/{courseId}/modules", [Authorize] async (Guid courseId, HttpContext http, CreatorOsContext db) =>
        {
            var modules = await db.CourseModules.Where(m => m.CourseId == courseId)
                .OrderBy(m => m.SortOrder)
                .Select(m => new { m.Id, m.Title, m.Description, m.SortOrder, m.IsPublished }).ToListAsync();
            return Results.Ok(modules);
        }).WithName("GetCourseModules").WithTags("Courses");

        app.MapPost("/api/courses/{courseId}/modules", [Authorize] async (Guid courseId, CreateModuleRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var maxOrder = await db.CourseModules.Where(m => m.CourseId == courseId).MaxAsync(m => (int?)m.SortOrder) ?? 0;
            var module = new CourseModule
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, CourseId = courseId,
                Title = req.Title, Description = req.Description, SortOrder = maxOrder + 1,
                IsPublished = false, CreatedAt = DateTime.UtcNow
            };
            db.CourseModules.Add(module);
            await db.SaveChangesAsync();
            return Results.Created($"/api/courses/modules/{module.Id}", new { module.Id, module.Title });
        }).WithName("CreateCourseModule").WithTags("Courses");

        app.MapGet("/api/courses/modules/{moduleId}/lessons", [Authorize] async (Guid moduleId, HttpContext http, CreatorOsContext db) =>
        {
            var lessons = await db.CourseLessons.Where(l => l.ModuleId == moduleId)
                .OrderBy(l => l.SortOrder)
                .Select(l => new { l.Id, l.Title, l.LessonType, l.IsFreePreview, l.IsPublished, l.EstimatedMinutes }).ToListAsync();
            return Results.Ok(lessons);
        }).WithName("GetModuleLessons").WithTags("Courses");

        app.MapPost("/api/courses/modules/{moduleId}/lessons", [Authorize] async (Guid moduleId, CreateLessonRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var maxOrder = await db.CourseLessons.Where(l => l.ModuleId == moduleId).MaxAsync(l => (int?)l.SortOrder) ?? 0;
            var lesson = new CourseLesson
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, ModuleId = moduleId,
                Title = req.Title, Description = req.Description, LessonType = req.LessonType ?? "video",
                Content = req.Content, VideoUrl = req.VideoUrl, SortOrder = maxOrder + 1,
                IsFreePreview = req.IsFreePreview, IsPublished = false, CreatedAt = DateTime.UtcNow
            };
            db.CourseLessons.Add(lesson);
            await db.SaveChangesAsync();
            return Results.Created($"/api/courses/lessons/{lesson.Id}", new { lesson.Id, lesson.Title });
        }).WithName("CreateCourseLesson").WithTags("Courses");

        app.MapPost("/api/courses/{courseId}/enroll", [Authorize] async (Guid courseId, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var enrollment = new CourseEnrollment
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, CourseId = courseId,
                StudentId = userId, Status = "active", ProgressPercent = 0, CreatedAt = DateTime.UtcNow
            };
            db.CourseEnrollments.Add(enrollment);
            var course = await db.Courses.FindAsync(courseId);
            if (course != null) course.EnrollmentCount += 1;
            await db.SaveChangesAsync();
            return Results.Ok(new { enrollment.Id, message = "Enrolled successfully" });
        }).WithName("EnrollCourse").WithTags("Courses");

        app.MapGet("/api/courses/{courseId}/progress", [Authorize] async (Guid courseId, HttpContext http, CreatorOsContext db) =>
        {
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var enrollment = await db.CourseEnrollments.FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == userId);
            if (enrollment == null) return Results.BadRequest(new { error = "Not enrolled" });
            var progress = await db.LessonProgresses.Where(lp => lp.EnrollmentId == enrollment.Id)
                .Select(lp => new { lp.LessonId, lp.Status, lp.CompletedAt, lp.TimeSpentSeconds }).ToListAsync();
            return Results.Ok(progress);
        }).WithName("GetCourseProgress").WithTags("Courses");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateCourseRequest(string Title, string? Description, string? Difficulty, decimal? EstimatedHours);
public record UpdateCourseRequest(string? Title, string? Description, string? Difficulty);
public record CreateModuleRequest(string Title, string? Description);
public record CreateLessonRequest(string Title, string? Description, string? LessonType, string? Content, string? VideoUrl, bool IsFreePreview);
