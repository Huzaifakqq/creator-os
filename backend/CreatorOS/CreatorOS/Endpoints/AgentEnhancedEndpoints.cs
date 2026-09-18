using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AgentEnhancedEndpoints
{
    public static void MapAgentEnhancedEndpoints(this WebApplication app)
    {
        app.MapPost("/api/agents/{id}/task", [Authorize] async (Guid id, AgentTaskRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (agent == null) return Results.NotFound();
            var permissions = await db.AiAgentPermissions.Where(p => p.AgentId == id).ToListAsync();
            var needsApproval = permissions.Any(p => p.PermissionType == req.TaskType && p.RequiresApproval);
            var task = new AiAgentTask
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, AgentId = id,
                TaskType = req.TaskType, InputData = req.Input,
                Status = needsApproval ? "pending_approval" : "pending",
                RequiresApproval = needsApproval, CreatedAt = DateTime.UtcNow
            };
            db.AiAgentTasks.Add(task);
            await db.SaveChangesAsync();
            return Results.Created($"/api/agents/tasks/{task.Id}", new { task.Id, task.Status, needsApproval });
        }).WithName("CreateAgentTask").WithTags("Agents");

        app.MapPost("/api/agents/tasks/{taskId}/approve", [Authorize] async (Guid taskId, ApproveTaskRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var task = await db.AiAgentTasks.FindAsync(taskId);
            if (task == null) return Results.NotFound();
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            task.Status = req.Approved ? "completed" : "rejected";
            task.ApprovedBy = userId;
            task.ApprovedAt = DateTime.UtcNow;
            if (!req.Approved) task.RejectionReason = req.Reason;
            await db.SaveChangesAsync();
            return Results.Ok(new { task.Id, task.Status });
        }).WithName("ApproveAgentTask").WithTags("Agents");

        app.MapGet("/api/agents/{type}/tasks", [Authorize] async (string type, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.AgentType == type && a.TenantId == tenantId);
            if (agent == null) return Results.NotFound();
            var tasks = await db.AiAgentTasks.Where(t => t.AgentId == agent.Id)
                .OrderByDescending(t => t.CreatedAt).Take(50)
                .Select(t => new { t.Id, t.TaskType, t.Status, t.TokensUsed, t.CreatedAt }).ToListAsync();
            return Results.Ok(tasks);
        }).WithName("GetAgentTasksByType").WithTags("Agents");

        app.MapGet("/api/agents/{id}/audit-logs", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var logs = await db.AiAgentAuditLogs.Where(l => l.AgentId == id && l.TenantId == tenantId)
                .OrderByDescending(l => l.CreatedAt).Take(100)
                .Select(l => new { l.Id, l.Action, l.EntityType, l.EntityId, l.CreatedAt }).ToListAsync();
            return Results.Ok(logs);
        }).WithName("GetAgentAuditLogs").WithTags("Agents");

        app.MapPut("/api/agents/{id}/permissions", [Authorize] async (Guid id, UpdatePermissionsRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var existing = await db.AiAgentPermissions.Where(p => p.AgentId == id).ToListAsync();
            db.AiAgentPermissions.RemoveRange(existing);
            foreach (var perm in req.Permissions)
            {
                db.AiAgentPermissions.Add(new AiAgentPermission
                {
                    Id = Guid.NewGuid(), AgentId = id, PermissionType = perm.PermissionType,
                    RequiresApproval = perm.RequiresApproval, AutoApproveBelowCents = perm.AutoApproveBelowCents,
                    CreatedAt = DateTime.UtcNow
                });
            }
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Permissions updated" });
        }).WithName("UpdateAgentPermissions").WithTags("Agents");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record AgentTaskRequest(string TaskType, string Input);
public record ApproveTaskRequest(bool Approved, string? Reason);
public record UpdatePermissionsRequest(List<PermInput> Permissions);
public record PermInput(string PermissionType, bool RequiresApproval, long AutoApproveBelowCents);
