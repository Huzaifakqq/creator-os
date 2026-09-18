using CreatorOS.Domain;
using CreatorOS.Services;
using CreatorOS.Endpoints;
using CreatorOS.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<CreatorOsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();

// Services
builder.Services.AddScoped<AuthService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Middleware
app.UseMiddleware<TenantMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

// Health check
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

// Endpoints
app.MapAuthEndpoints();
app.MapTenantEndpoints();
app.MapProductEndpoints();
app.MapOrderEndpoints();
app.MapAppEndpoints();
app.MapPageEndpoints();
app.MapSubscriptionEndpoints();
app.MapCouponEndpoints();
app.MapMarketplaceListEndpoints();
app.MapMarketplaceListingEndpoints();
app.MapReviewEndpoints();
app.MapCategoryEndpoints();
app.MapAgentEndpoints();
app.MapContactEndpoints();
app.MapCampaignEndpoints();
app.MapAnalyticsEndpoints();
app.MapAuthEnhancedEndpoints();
app.MapBuilderEndpoints();
app.MapCommerceEnhancedEndpoints();
app.MapMarketplaceEnhancedEndpoints();
app.MapAgentEnhancedEndpoints();
app.MapAdminEndpoints();
app.MapTeamEndpoints();
app.MapCourseEndpoints();
app.MapMembershipEndpoints();
app.MapCoachingEndpoints();
app.MapAffiliateEndpoints();
app.MapEmailEnhancedEndpoints();
app.MapAuditLogEndpoints();
app.MapWorkspaceEndpoints();

app.Run();
