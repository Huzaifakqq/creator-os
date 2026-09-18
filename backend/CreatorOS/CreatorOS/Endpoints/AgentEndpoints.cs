using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class AgentEndpoints
{
    public static void MapAgentEndpoints(this WebApplication app)
    {
        app.MapGet("/api/agents", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agents = await db.AiAgents.Where(a => a.TenantId == tenantId)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new { a.Id, a.Name, a.AgentType, a.Status, a.ModelName, a.CreditsUsed, a.MonthlyCreditLimit, a.CreatedAt })
                .ToListAsync();
            return Results.Ok(agents);
        }).WithName("GetAgents").WithTags("Agents");

        app.MapGet("/api/agents/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            return agent != null ? Results.Ok(agent) : Results.NotFound();
        }).WithName("GetAgent").WithTags("Agents");

        app.MapPost("/api/agents", [Authorize] async (CreateAgentRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = new AiAgent
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                AgentType = req.AgentType, ModelName = req.Model ?? "gpt-4o",
                ModelProvider = "openai", SystemPrompt = req.SystemPrompt ?? "",
                MonthlyCreditLimit = 1000, CreditsUsed = 0, Status = "active",
                RequiresApproval = false, CreatedAt = DateTime.UtcNow
            };
            db.AiAgents.Add(agent);
            await db.SaveChangesAsync();
            return Results.Created($"/api/agents/{agent.Id}", new { agent.Id, agent.Name });
        }).WithName("CreateAgent").WithTags("Agents");

        app.MapPut("/api/agents/{id}", [Authorize] async (Guid id, UpdateAgentRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (agent == null) return Results.NotFound();
            if (req.Name != null) agent.Name = req.Name;
            if (req.SystemPrompt != null) agent.SystemPrompt = req.SystemPrompt;
            if (req.Model != null) agent.ModelName = req.Model;
            agent.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { agent.Id, agent.Name });
        }).WithName("UpdateAgent").WithTags("Agents");

        app.MapDelete("/api/agents/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (agent == null) return Results.NotFound();
            db.AiAgents.Remove(agent);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Agent deleted" });
        }).WithName("DeleteAgent").WithTags("Agents");

        app.MapPost("/api/agents/{id}/test", [Authorize] async (Guid id, TestAgentRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var agent = await db.AiAgents.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (agent == null) return Results.NotFound();
            var task = new AiAgentTask
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, AgentId = id,
                TaskType = "test", InputData = req.Message,
                Status = "completed", TokensUsed = req.Message.Length / 4,
                CostCents = 0, CreatedAt = DateTime.UtcNow, CompletedAt = DateTime.UtcNow
            };
            db.AiAgentTasks.Add(task);
            await db.SaveChangesAsync();
            return Results.Ok(new { task.Id, task.Status, message = "Test task logged. AI call pending OpenAI integration." });
        }).WithName("TestAgent").WithTags("Agents");

        app.MapGet("/api/agents/{id}/tasks", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var tasks = await db.AiAgentTasks.Where(t => t.AgentId == id && t.TenantId == tenantId)
                .OrderByDescending(t => t.CreatedAt).Take(50)
                .Select(t => new { t.Id, t.TaskType, t.Status, t.TokensUsed, t.CreatedAt })
                .ToListAsync();
            return Results.Ok(tasks);
        }).WithName("GetAgentTasks").WithTags("Agents");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateAgentRequest(string Name, string AgentType, string? Model, string? SystemPrompt);
public record UpdateAgentRequest(string? Name, string? SystemPrompt, string? Model);
public record TestAgentRequest(string Message);
