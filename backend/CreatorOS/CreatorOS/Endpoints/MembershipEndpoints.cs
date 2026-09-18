using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class MembershipEndpoints
{
    public static void MapMembershipEndpoints(this WebApplication app)
    {
        app.MapGet("/api/memberships/tiers", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var tiers = await db.MembershipTiers.Where(t => t.TenantId == tenantId && t.IsActive)
                .OrderBy(t => t.SortOrder)
                .Select(t => new { t.Id, t.Name, t.Description, t.PriceCents, t.BillingInterval, t.Features, t.MemberCount })
                .ToListAsync();
            return Results.Ok(tiers);
        }).WithName("GetMembershipTiers").WithTags("Memberships");

        app.MapPost("/api/memberships/tiers", [Authorize] async (CreateTierRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var maxOrder = await db.MembershipTiers.Where(t => t.TenantId == tenantId).MaxAsync(t => (int?)t.SortOrder) ?? 0;
            var tier = new MembershipTier
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, PriceCents = req.PriceCents,
                BillingInterval = req.BillingInterval ?? "monthly", Features = req.Features,
                IsActive = true, MemberCount = 0, SortOrder = maxOrder + 1,
                CreatedAt = DateTime.UtcNow
            };
            db.MembershipTiers.Add(tier);
            await db.SaveChangesAsync();
            return Results.Created($"/api/memberships/tiers/{tier.Id}", new { tier.Id, tier.Name });
        }).WithName("CreateMembershipTier").WithTags("Memberships");

        app.MapPut("/api/memberships/tiers/{id}", [Authorize] async (Guid id, UpdateTierRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var tier = await db.MembershipTiers.FirstOrDefaultAsync(t => t.Id == id && t.TenantId == tenantId);
            if (tier == null) return Results.NotFound();
            if (req.Name != null) tier.Name = req.Name;
            if (req.Description != null) tier.Description = req.Description;
            if (req.PriceCents.HasValue) tier.PriceCents = req.PriceCents.Value;
            tier.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { tier.Id, tier.Name });
        }).WithName("UpdateMembershipTier").WithTags("Memberships");

        app.MapGet("/api/memberships/spaces", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var spaces = await db.CommunitySpaces.Where(s => s.TenantId == tenantId)
                .Select(s => new { s.Id, s.Name, s.Description, s.SpaceType, s.MemberCount })
                .ToListAsync();
            return Results.Ok(spaces);
        }).WithName("GetCommunitySpaces").WithTags("Memberships");

        app.MapPost("/api/memberships/spaces", [Authorize] async (CreateSpaceRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var space = new CommunitySpace
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, SpaceType = req.SpaceType ?? "general",
                MemberCount = 0, CreatedAt = DateTime.UtcNow
            };
            db.CommunitySpaces.Add(space);
            await db.SaveChangesAsync();
            return Results.Created($"/api/memberships/spaces/{space.Id}", new { space.Id, space.Name });
        }).WithName("CreateCommunitySpace").WithTags("Memberships");

        app.MapGet("/api/memberships/spaces/{spaceId}/posts", [Authorize] async (Guid spaceId, HttpContext http, CreatorOsContext db) =>
        {
            var posts = await db.CommunityPosts.Where(p => p.SpaceId == spaceId)
                .OrderByDescending(p => p.CreatedAt).Take(50)
                .Select(p => new { p.Id, p.Title, p.Content, p.AuthorId, p.LikeCount, p.CommentCount, p.CreatedAt })
                .ToListAsync();
            return Results.Ok(posts);
        }).WithName("GetSpacePosts").WithTags("Memberships");

        app.MapPost("/api/memberships/spaces/{spaceId}/posts", [Authorize] async (Guid spaceId, CreatePostRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var post = new CommunityPost
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, SpaceId = spaceId,
                AuthorId = userId, Title = req.Title, Content = req.Content,
                IsPinned = false, LikeCount = 0, CommentCount = 0,
                CreatedAt = DateTime.UtcNow
            };
            db.CommunityPosts.Add(post);
            await db.SaveChangesAsync();
            return Results.Created($"/api/memberships/posts/{post.Id}", new { post.Id, post.Title });
        }).WithName("CreateSpacePost").WithTags("Memberships");

        app.MapGet("/api/memberships/posts/{postId}/comments", [Authorize] async (Guid postId, HttpContext http, CreatorOsContext db) =>
        {
            var comments = await db.CommunityComments.Where(c => c.PostId == postId)
                .OrderBy(c => c.CreatedAt).Take(100)
                .Select(c => new { c.Id, c.AuthorId, c.Content, c.CreatedAt })
                .ToListAsync();
            return Results.Ok(comments);
        }).WithName("GetPostComments").WithTags("Memberships");

        app.MapPost("/api/memberships/posts/{postId}/comments", [Authorize] async (Guid postId, CreateCommentRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var comment = new CommunityComment
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, PostId = postId,
                AuthorId = userId, Content = req.Content, CreatedAt = DateTime.UtcNow
            };
            db.CommunityComments.Add(comment);
            var post = await db.CommunityPosts.FindAsync(postId);
            if (post != null) post.CommentCount += 1;
            await db.SaveChangesAsync();
            return Results.Created($"/api/memberships/comments/{comment.Id}", new { comment.Id });
        }).WithName("CreatePostComment").WithTags("Memberships");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateTierRequest(string Name, string? Description, long PriceCents, string? BillingInterval, string? Features);
public record UpdateTierRequest(string? Name, string? Description, long? PriceCents);
public record CreateSpaceRequest(string Name, string? Description, string? SpaceType);
public record CreatePostRequest(string Title, string Content);
public record CreateCommentRequest(string Content);
