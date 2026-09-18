using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Domain;

public partial class CreatorOsContext : DbContext
{
    public CreatorOsContext()
    {
    }

    public CreatorOsContext(DbContextOptions<CreatorOsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbTest> AbTests { get; set; }

    public virtual DbSet<AffiliateConversion> AffiliateConversions { get; set; }

    public virtual DbSet<AffiliateLink> AffiliateLinks { get; set; }

    public virtual DbSet<AffiliateMember> AffiliateMembers { get; set; }

    public virtual DbSet<AffiliatePayout> AffiliatePayouts { get; set; }

    public virtual DbSet<AffiliateProgram> AffiliatePrograms { get; set; }

    public virtual DbSet<AiAgent> AiAgents { get; set; }

    public virtual DbSet<AiAgentAuditLog> AiAgentAuditLogs { get; set; }

    public virtual DbSet<AiAgentPermission> AiAgentPermissions { get; set; }

    public virtual DbSet<AiAgentTask> AiAgentTasks { get; set; }

    public virtual DbSet<AiGenerationSession> AiGenerationSessions { get; set; }

    public virtual DbSet<AnalyticsEvent> AnalyticsEvents { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AppComponent> AppComponents { get; set; }

    public virtual DbSet<AppDatabase> AppDatabases { get; set; }

    public virtual DbSet<AppDeployment> AppDeployments { get; set; }

    public virtual DbSet<AppFeature> AppFeatures { get; set; }

    public virtual DbSet<AppLog> AppLogs { get; set; }

    public virtual DbSet<AppVersion> AppVersions { get; set; }

    public virtual DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

    public virtual DbSet<Attribution> Attributions { get; set; }

    public virtual DbSet<CoachingBooking> CoachingBookings { get; set; }

    public virtual DbSet<CoachingSessionNote> CoachingSessionNotes { get; set; }

    public virtual DbSet<CoachingSlot> CoachingSlots { get; set; }

    public virtual DbSet<CohortGradingSheet> CohortGradingSheets { get; set; }

    public virtual DbSet<CommunityComment> CommunityComments { get; set; }

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; }

    public virtual DbSet<CommunitySpace> CommunitySpaces { get; set; }

    public virtual DbSet<ContentDraft> ContentDrafts { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<CourseLesson> CourseLessons { get; set; }

    public virtual DbSet<CourseModule> CourseModules { get; set; }

    public virtual DbSet<CourseQuiz> CourseQuizzes { get; set; }

    public virtual DbSet<CrmActivity> CrmActivities { get; set; }

    public virtual DbSet<CrmContact> CrmContacts { get; set; }

    public virtual DbSet<CrmSegment> CrmSegments { get; set; }

    public virtual DbSet<CrmTag> CrmTags { get; set; }

    public virtual DbSet<CustomReport> CustomReports { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<DealPipeline> DealPipelines { get; set; }

    public virtual DbSet<DigitalAsset> DigitalAssets { get; set; }

    public virtual DbSet<DirectMessage> DirectMessages { get; set; }

    public virtual DbSet<Dispute> Disputes { get; set; }

    public virtual DbSet<DomainSetting> DomainSettings { get; set; }

    public virtual DbSet<EmailCampaign> EmailCampaigns { get; set; }

    public virtual DbSet<EmailEvent> EmailEvents { get; set; }

    public virtual DbSet<EmailList> EmailLists { get; set; }

    public virtual DbSet<EmailSequence> EmailSequences { get; set; }

    public virtual DbSet<EmailSequenceEnrollment> EmailSequenceEnrollments { get; set; }

    public virtual DbSet<EmailSubscriber> EmailSubscribers { get; set; }

    public virtual DbSet<EmailSuppressionList> EmailSuppressionLists { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<LeadMagnet> LeadMagnets { get; set; }

    public virtual DbSet<LessonProgress> LessonProgresses { get; set; }

    public virtual DbSet<LiveSession> LiveSessions { get; set; }

    public virtual DbSet<MarketplaceCategory> MarketplaceCategories { get; set; }

    public virtual DbSet<MarketplaceListing> MarketplaceListings { get; set; }

    public virtual DbSet<MarketplaceListingMetric> MarketplaceListingMetrics { get; set; }

    public virtual DbSet<MarketplaceReview> MarketplaceReviews { get; set; }

    public virtual DbSet<MemberSubscription> MemberSubscriptions { get; set; }

    public virtual DbSet<MembershipTier> MembershipTiers { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationPreference> NotificationPreferences { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PageBlock> PageBlocks { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PlatformSetting> PlatformSettings { get; set; }

    public virtual DbSet<PlatformSubscription> PlatformSubscriptions { get; set; }

    public virtual DbSet<PricingCalculation> PricingCalculations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SecurityAuditLog> SecurityAuditLogs { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<TeamInvite> TeamInvites { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<TemplateRemix> TemplateRemixes { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<UsageAlert> UsageAlerts { get; set; }

    public virtual DbSet<UsageMeter> UsageMeters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserConsent> UserConsents { get; set; }

    public virtual DbSet<UserMfaSetting> UserMfaSettings { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<UserTenantMembership> UserTenantMemberships { get; set; }

    public virtual DbSet<Webhook> Webhooks { get; set; }

    public virtual DbSet<WebhookDelivery> WebhookDeliveries { get; set; }

    public virtual DbSet<Workspace> Workspaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HAFSAAHMED1903;Database=CreatorOS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AbTests__3214EC075DAD33CC");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.TargetEntity).HasMaxLength(100);
            entity.Property(e => e.TestType).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AbTests)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AbTests_Tenant");
        });

        modelBuilder.Entity<AffiliateConversion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Affiliat__3214EC07271A8A94");

            entity.HasIndex(e => e.LinkId, "IX_AffiliateConversions_LinkId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Link).WithMany(p => p.AffiliateConversions)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateConversions_Link");

            entity.HasOne(d => d.Order).WithMany(p => p.AffiliateConversions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateConversions_Order");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AffiliateConversions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateConversions_Tenant");
        });

        modelBuilder.Entity<AffiliateLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Affiliat__3214EC07556B960F");

            entity.HasIndex(e => e.Code, "IX_AffiliateLinks_Code");

            entity.HasIndex(e => e.Code, "UQ_AffiliateLinks_Code").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TargetUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Member).WithMany(p => p.AffiliateLinks)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateLinks_Member");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AffiliateLinks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateLinks_Tenant");
        });

        modelBuilder.Entity<AffiliateMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Affiliat__3214EC07928E72F1");

            entity.HasIndex(e => new { e.ProgramId, e.AffiliateId }, "UQ_AffiliateMembers").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CustomCommissionRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.Tier)
                .HasMaxLength(50)
                .HasDefaultValue("standard");

            entity.HasOne(d => d.Affiliate).WithMany(p => p.AffiliateMembers)
                .HasForeignKey(d => d.AffiliateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateMembers_Affiliate");

            entity.HasOne(d => d.Program).WithMany(p => p.AffiliateMembers)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateMembers_Program");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AffiliateMembers)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliateMembers_Tenant");
        });

        modelBuilder.Entity<AffiliatePayout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Affiliat__3214EC07095F4230");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.PayoutMethod).HasMaxLength(50);
            entity.Property(e => e.PayoutReference).HasMaxLength(256);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Member).WithMany(p => p.AffiliatePayouts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliatePayouts_Member");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AffiliatePayouts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliatePayouts_Tenant");
        });

        modelBuilder.Entity<AffiliateProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Affiliat__3214EC07C48D4770");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AllowedPromotionMethods).HasMaxLength(500);
            entity.Property(e => e.CommissionType)
                .HasMaxLength(50)
                .HasDefaultValue("percentage");
            entity.Property(e => e.CommissionValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CookieDays).HasDefaultValue(30);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.MinimumPayoutCents).HasDefaultValue(5000L);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.TermsUrl).HasMaxLength(500);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AffiliatePrograms)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AffiliatePrograms_Tenant");
        });

        modelBuilder.Entity<AiAgent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AiAgents__3214EC079C7052DC");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AgentType).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .HasDefaultValue("gpt-4o");
            entity.Property(e => e.ModelProvider)
                .HasMaxLength(100)
                .HasDefaultValue("openai");
            entity.Property(e => e.MonthlyCreditLimit).HasDefaultValue(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AiAgents)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiAgents_Tenant");
        });

        modelBuilder.Entity<AiAgentAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AiAgentA__3214EC07ADC1F411");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Action).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EntityType).HasMaxLength(100);

            entity.HasOne(d => d.Agent).WithMany(p => p.AiAgentAuditLogs)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiAgentAuditLogs_Agent");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AiAgentAuditLogs)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiAgentAuditLogs_Tenant");
        });

        modelBuilder.Entity<AiAgentPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AiAgentP__3214EC07CEB76786");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.PermissionType).HasMaxLength(100);
            entity.Property(e => e.RequiresApproval).HasDefaultValue(true);

            entity.HasOne(d => d.Agent).WithMany(p => p.AiAgentPermissions)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_AiAgentPermissions_Agent");
        });

        modelBuilder.Entity<AiAgentTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AiAgentT__3214EC07070EF543");

            entity.HasIndex(e => e.AgentId, "IX_AiAgentTasks_AgentId");

            entity.HasIndex(e => e.Status, "IX_AiAgentTasks_Status");

            entity.HasIndex(e => e.TenantId, "IX_AiAgentTasks_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ErrorMessage).HasMaxLength(2000);
            entity.Property(e => e.RejectionReason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");
            entity.Property(e => e.TaskType).HasMaxLength(100);

            entity.HasOne(d => d.Agent).WithMany(p => p.AiAgentTasks)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiAgentTasks_Agent");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AiAgentTasks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiAgentTasks_Tenant");
        });

        modelBuilder.Entity<AiGenerationSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AiGenera__3214EC075CF69335");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ErrorMessage).HasMaxLength(2000);
            entity.Property(e => e.ModelUsed).HasMaxLength(100);
            entity.Property(e => e.Prompt).HasMaxLength(4000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("completed");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AiGenerationSessions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiGenSessions_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.AiGenerationSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AiGenSessions_User");
        });

        modelBuilder.Entity<AnalyticsEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Analytic__3214EC077BE9FEB8");

            entity.HasIndex(e => e.CreatedAt, "IX_AnalyticsEvents_CreatedAt");

            entity.HasIndex(e => e.EventType, "IX_AnalyticsEvents_EventType");

            entity.HasIndex(e => e.TenantId, "IX_AnalyticsEvents_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CountryCode).HasMaxLength(2);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DeviceType).HasMaxLength(50);
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.EventType).HasMaxLength(100);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.Source).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AnalyticsEvents)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnalyticsEvents_Tenant");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApiKeys__3214EC07904EFC4B");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.KeyHash).HasMaxLength(256);
            entity.Property(e => e.KeyPrefix).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.RateLimit).HasDefaultValue(1000);

            entity.HasOne(d => d.Tenant).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiKeys_Tenant");
        });

        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Apps__3214EC07E228F959");

            entity.HasIndex(e => e.Status, "IX_Apps_Status");

            entity.HasIndex(e => e.TenantId, "IX_Apps_TenantId");

            entity.HasIndex(e => e.WorkspaceId, "IX_Apps_WorkspaceId");

            entity.HasIndex(e => new { e.WorkspaceId, e.Slug }, "UQ_Apps_WorkspaceSlug").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CustomDomain).HasMaxLength(253);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValue("website");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Apps)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Apps_Tenant");

            entity.HasOne(d => d.Workspace).WithMany(p => p.Apps)
                .HasForeignKey(d => d.WorkspaceId)
                .HasConstraintName("FK_Apps_Workspace");
        });

        modelBuilder.Entity<AppComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppCompo__3214EC07798518A6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ComponentType).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.App).WithMany(p => p.AppComponents)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_AppComponents_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppComponents)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppComponents_Tenant");
        });

        modelBuilder.Entity<AppDatabase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppDatab__3214EC07CB6A798A");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.TableName).HasMaxLength(200);

            entity.HasOne(d => d.App).WithMany(p => p.AppDatabases)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_AppDatabases_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppDatabases)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppDatabases_Tenant");
        });

        modelBuilder.Entity<AppDeployment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppDeplo__3214EC07357E6442");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DeploymentUrl).HasMaxLength(1000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.App).WithMany(p => p.AppDeployments)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppDeployments_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppDeployments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppDeployments_Tenant");

            entity.HasOne(d => d.Version).WithMany(p => p.AppDeployments)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppDeployments_Version");
        });

        modelBuilder.Entity<AppFeature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppFeatu__3214EC079D472611");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.FeatureName).HasMaxLength(200);
            entity.Property(e => e.FeatureType).HasMaxLength(50);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);

            entity.HasOne(d => d.App).WithMany(p => p.AppFeatures)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_AppFeatures_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppFeatures)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppFeatures_Tenant");
        });

        modelBuilder.Entity<AppLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppLogs__3214EC07D0C53004");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.LogLevel)
                .HasMaxLength(50)
                .HasDefaultValue("info");
            entity.Property(e => e.Message).HasMaxLength(4000);
            entity.Property(e => e.Source).HasMaxLength(200);

            entity.HasOne(d => d.App).WithMany(p => p.AppLogs)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_AppLogs_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppLogs)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppLogs_Tenant");
        });

        modelBuilder.Entity<AppVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppVersi__3214EC07B1371651");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Changelog).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.App).WithMany(p => p.AppVersions)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_AppVersions_App");
        });

        modelBuilder.Entity<AssignmentSubmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignme__3214EC07F1C75E0E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Feedback).HasMaxLength(4000);
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("submitted");
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Assignment).WithMany(p => p.AssignmentSubmissions)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignmentSubmissions_Assignment");

            entity.HasOne(d => d.Student).WithMany(p => p.AssignmentSubmissions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignmentSubmissions_Student");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AssignmentSubmissions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignmentSubmissions_Tenant");
        });

        modelBuilder.Entity<Attribution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attribut__3214EC07FE696F5E");

            entity.ToTable("Attribution");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ReferralCode).HasMaxLength(100);
            entity.Property(e => e.Source).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Attributions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attribution_Order");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Attributions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attribution_Tenant");
        });

        modelBuilder.Entity<CoachingBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coaching__3214EC07812FB9D1");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CancellationReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MeetingUrl).HasMaxLength(1000);
            entity.Property(e => e.Notes).HasMaxLength(4000);
            entity.Property(e => e.SessionTime).HasMaxLength(10);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("confirmed");

            entity.HasOne(d => d.Client).WithMany(p => p.CoachingBookings)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingBookings_Client");

            entity.HasOne(d => d.Slot).WithMany(p => p.CoachingBookings)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingBookings_Slot");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CoachingBookings)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingBookings_Tenant");
        });

        modelBuilder.Entity<CoachingSessionNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coaching__3214EC0797C040A2");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.NoteType)
                .HasMaxLength(50)
                .HasDefaultValue("general");

            entity.HasOne(d => d.Booking).WithMany(p => p.CoachingSessionNotes)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_CoachingNotes_Booking");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CoachingSessionNotes)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingNotes_Tenant");
        });

        modelBuilder.Entity<CoachingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coaching__3214EC0716B733DE");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvailableDays).HasMaxLength(100);
            entity.Property(e => e.BufferMinutes).HasDefaultValue(15);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DurationMinutes).HasDefaultValue(60);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxParticipants).HasDefaultValue(1);
            entity.Property(e => e.RecurringPattern).HasMaxLength(50);
            entity.Property(e => e.SessionType)
                .HasMaxLength(50)
                .HasDefaultValue("video");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(100)
                .HasDefaultValue("UTC");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachingSlots)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingSlots_Coach");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CoachingSlots)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoachingSlots_Tenant");
        });

        modelBuilder.Entity<CohortGradingSheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CohortGr__3214EC07021A5EED");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Course).WithMany(p => p.CohortGradingSheets)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CohortGrading_Course");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CohortGradingSheets)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CohortGrading_Tenant");
        });

        modelBuilder.Entity<CommunityComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC076CA235C4");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Author).WithMany(p => p.CommunityComments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunityComments_Author");

            entity.HasOne(d => d.Post).WithMany(p => p.CommunityComments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_CommunityComments_Post");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CommunityComments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunityComments_Tenant");
        });

        modelBuilder.Entity<CommunityPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC07E97E425E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.PostType)
                .HasMaxLength(50)
                .HasDefaultValue("text");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Author).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunityPosts_Author");

            entity.HasOne(d => d.Space).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_CommunityPosts_Space");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunityPosts_Tenant");
        });

        modelBuilder.Entity<CommunitySpace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC072E54F2B3");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IconUrl).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.SpaceType)
                .HasMaxLength(50)
                .HasDefaultValue("feed");

            entity.HasOne(d => d.RequiredTier).WithMany(p => p.CommunitySpaces)
                .HasForeignKey(d => d.RequiredTierId)
                .HasConstraintName("FK_CommunitySpaces_Tier");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CommunitySpaces)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommunitySpaces_Tenant");
        });

        modelBuilder.Entity<ContentDraft>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContentD__3214EC072A459F0A");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AiModelUsed).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DraftType).HasMaxLength(100);
            entity.Property(e => e.Platform).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Tenant).WithMany(p => p.ContentDrafts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentDrafts_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.ContentDrafts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentDrafts_User");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coupons__3214EC07E1C096B0");

            entity.HasIndex(e => new { e.TenantId, e.Code }, "UQ_Coupons_TenantCode").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AppliesTo)
                .HasMaxLength(50)
                .HasDefaultValue("all");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountType).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PerUserLimit).HasDefaultValue(1);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coupons_Tenant");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC0759DFE8E6");

            entity.HasIndex(e => e.TenantId, "IX_Courses_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AverageRating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CompletionRate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Difficulty)
                .HasMaxLength(50)
                .HasDefaultValue("beginner");
            entity.Property(e => e.EstimatedHours).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Tenant");
        });

        modelBuilder.Entity<CourseAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CourseAs__3214EC075745C7C3");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Instructions).HasMaxLength(4000);
            entity.Property(e => e.LatePenaltyPercent).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MaxFileSizeMb)
                .HasDefaultValue(50)
                .HasColumnName("MaxFileSizeMB");
            entity.Property(e => e.MaxScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SubmissionTypes).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Lesson).WithMany(p => p.CourseAssignments)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseAssignments_Lesson");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CourseAssignments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseAssignments_Tenant");
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CourseEn__3214EC0713445730");

            entity.HasIndex(e => e.CourseId, "IX_CourseEnrollments_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_CourseEnrollments_StudentId");

            entity.HasIndex(e => new { e.CourseId, e.StudentId }, "UQ_CourseEnrollments").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CertificateUrl).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.ProgressPercent).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollments_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollments_Student");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollments_Tenant");
        });

        modelBuilder.Entity<CourseLesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CourseLe__3214EC07ED1A3777");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.LessonType).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.VideoUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Module).WithMany(p => p.CourseLessons)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_CourseLessons_Module");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CourseLessons)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseLessons_Tenant");
        });

        modelBuilder.Entity<CourseModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CourseMo__3214EC07605F6B24");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseModules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_CourseModules_Course");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CourseModules)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseModules_Tenant");
        });

        modelBuilder.Entity<CourseQuiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CourseQu__3214EC07D5094EFE");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MaxAttempts).HasDefaultValue(3);
            entity.Property(e => e.PassingScore)
                .HasDefaultValue(60m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Lesson).WithMany(p => p.CourseQuizzes)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseQuizzes_Lesson");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CourseQuizzes)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseQuizzes_Tenant");
        });

        modelBuilder.Entity<CrmActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CrmActiv__3214EC0712D35566");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActivityType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Direction).HasMaxLength(20);
            entity.Property(e => e.Subject).HasMaxLength(300);

            entity.HasOne(d => d.Contact).WithMany(p => p.CrmActivities)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_CrmActivities_Contact");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CrmActivities)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrmActivities_Tenant");
        });

        modelBuilder.Entity<CrmContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CrmConta__3214EC0773848172");

            entity.HasIndex(e => e.Email, "IX_CrmContacts_Email");

            entity.HasIndex(e => e.Status, "IX_CrmContacts_Status");

            entity.HasIndex(e => e.TenantId, "IX_CrmContacts_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.Company).HasMaxLength(200);
            entity.Property(e => e.ContactType)
                .HasMaxLength(50)
                .HasDefaultValue("lead");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("new");

            entity.HasOne(d => d.Tenant).WithMany(p => p.CrmContacts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrmContacts_Tenant");

            entity.HasMany(d => d.Tags).WithMany(p => p.Contacts)
                .UsingEntity<Dictionary<string, object>>(
                    "CrmContactTag",
                    r => r.HasOne<CrmTag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_CrmContactTags_Tag"),
                    l => l.HasOne<CrmContact>().WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK_CrmContactTags_Contact"),
                    j =>
                    {
                        j.HasKey("ContactId", "TagId");
                        j.ToTable("CrmContactTags");
                    });
        });

        modelBuilder.Entity<CrmSegment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CrmSegme__3214EC078D59A101");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDynamic).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.CrmSegments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrmSegments_Tenant");
        });

        modelBuilder.Entity<CrmTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CrmTags__3214EC07B45A9C44");

            entity.HasIndex(e => new { e.TenantId, e.Name }, "UQ_CrmTags").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Color).HasMaxLength(7);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.CrmTags)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrmTags_Tenant");
        });

        modelBuilder.Entity<CustomReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CustomRe__3214EC0789D15EC6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ReportType).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.CustomReports)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomReports_Tenant");
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deals__3214EC07F539BC6F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.Notes).HasMaxLength(4000);
            entity.Property(e => e.Stage).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("open");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Contact).WithMany(p => p.Deals)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_Deals_Contact");

            entity.HasOne(d => d.Pipeline).WithMany(p => p.Deals)
                .HasForeignKey(d => d.PipelineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deals_Pipeline");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Deals)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deals_Tenant");
        });

        modelBuilder.Entity<DealPipeline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DealPipe__3214EC07EDA4D7B7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.DealPipelines)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DealPipelines_Tenant");
        });

        modelBuilder.Entity<DigitalAsset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DigitalA__3214EC07624EF2E7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccessLevel)
                .HasMaxLength(50)
                .HasDefaultValue("purchased");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FileType).HasMaxLength(100);
            entity.Property(e => e.FileUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Product).WithMany(p => p.DigitalAssetsNavigation)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_DigitalAssets_Product");

            entity.HasOne(d => d.Tenant).WithMany(p => p.DigitalAssets)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DigitalAssets_Tenant");
        });

        modelBuilder.Entity<DirectMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DirectMe__3214EC07BD810366");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MediaType).HasMaxLength(50);
            entity.Property(e => e.MediaUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Receiver).WithMany(p => p.DirectMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DirectMessages_Receiver");

            entity.HasOne(d => d.Sender).WithMany(p => p.DirectMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DirectMessages_Sender");

            entity.HasOne(d => d.Tenant).WithMany(p => p.DirectMessages)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DirectMessages_Tenant");
        });

        modelBuilder.Entity<Dispute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Disputes__3214EC07DF118CDD");

            entity.HasIndex(e => e.Status, "IX_Disputes_Status");

            entity.HasIndex(e => e.TenantId, "IX_Disputes_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Resolution).HasMaxLength(1000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("open");

            entity.HasOne(d => d.Order).WithMany(p => p.Disputes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disputes_Order");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Disputes)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disputes_Tenant");
        });

        modelBuilder.Entity<DomainSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DomainSe__3214EC07A5260E77");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DnsVerificationToken).HasMaxLength(128);
            entity.Property(e => e.Domain).HasMaxLength(253);
            entity.Property(e => e.SslStatus)
                .HasMaxLength(50)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Tenant).WithMany(p => p.DomainSettings)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DomainSettings_Tenant");
        });

        modelBuilder.Entity<EmailCampaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailCam__3214EC07053B0EC8");

            entity.HasIndex(e => e.Status, "IX_EmailCampaigns_Status");

            entity.HasIndex(e => e.TenantId, "IX_EmailCampaigns_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CampaignType)
                .HasMaxLength(50)
                .HasDefaultValue("broadcast");
            entity.Property(e => e.ClickRate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.OpenRate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PreviewText).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.Subject).HasMaxLength(300);

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailCampaigns)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailCampaigns_Tenant");
        });

        modelBuilder.Entity<EmailEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailEve__3214EC074B0E0CD5");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EventType).HasMaxLength(50);

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailEvents)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailEvents_Tenant");
        });

        modelBuilder.Entity<EmailList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailLis__3214EC073497392F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailLists)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailLists_Tenant");
        });

        modelBuilder.Entity<EmailSequence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSeq__3214EC0711827C0E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.TriggerType).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailSequences)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailSequences_Tenant");
        });

        modelBuilder.Entity<EmailSequenceEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSeq__3214EC0707D014EF");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Contact).WithMany(p => p.EmailSequenceEnrollments)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailSeqEnrollments_Contact");

            entity.HasOne(d => d.Sequence).WithMany(p => p.EmailSequenceEnrollments)
                .HasForeignKey(d => d.SequenceId)
                .HasConstraintName("FK_EmailSeqEnrollments_Sequence");

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailSequenceEnrollments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailSeqEnrollments_Tenant");
        });

        modelBuilder.Entity<EmailSubscriber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSub__3214EC07667CB410");

            entity.HasIndex(e => new { e.ListId, e.Email }, "UQ_EmailSubscribers").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.SubscribedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.List).WithMany(p => p.EmailSubscribers)
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK_EmailSubscribers_List");

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailSubscribers)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailSubscribers_Tenant");
        });

        modelBuilder.Entity<EmailSuppressionList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailSup__3214EC07E7213F57");

            entity.ToTable("EmailSuppressionList");

            entity.HasIndex(e => new { e.TenantId, e.Email }, "UQ_EmailSuppression").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.SuppressedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailSuppressionLists)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailSuppression_Tenant");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3214EC077685C5B2");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);

            entity.HasOne(d => d.Tenant).WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_EmailTemplates_Tenant");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Integrat__3214EC073BB0A73F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccessToken).HasMaxLength(2000);
            entity.Property(e => e.ApiKey).HasMaxLength(500);
            entity.Property(e => e.AuthType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.RefreshToken).HasMaxLength(2000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Integrations_Tenant");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoices__3214EC07CDCAF7AA");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.CustomerEmail).HasMaxLength(320);
            entity.Property(e => e.CustomerName).HasMaxLength(200);
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.PdfUrl).HasMaxLength(1000);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.StripeInvoiceId).HasMaxLength(256);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Tenant");
        });

        modelBuilder.Entity<LeadMagnet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeadMagn__3214EC076E5E9378");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AssetType).HasMaxLength(50);
            entity.Property(e => e.AssetUrl).HasMaxLength(1000);
            entity.Property(e => e.ConversionRate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.LeadMagnets)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeadMagnets_Tenant");
        });

        modelBuilder.Entity<LessonProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LessonPr__3214EC07CCB4DB0A");

            entity.ToTable("LessonProgress");

            entity.HasIndex(e => new { e.EnrollmentId, e.LessonId }, "UQ_LessonProgress").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("not_started");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.EnrollmentId)
                .HasConstraintName("FK_LessonProgress_Enrollment");

            entity.HasOne(d => d.Lesson).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonProgress_Lesson");

            entity.HasOne(d => d.Tenant).WithMany(p => p.LessonProgresses)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonProgress_Tenant");
        });

        modelBuilder.Entity<LiveSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LiveSess__3214EC07A6EB1E49");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChatEnabled).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.MaxViewers).HasDefaultValue(100);
            entity.Property(e => e.RecordingUrl).HasMaxLength(1000);
            entity.Property(e => e.SessionType).HasMaxLength(50);
            entity.Property(e => e.StreamKey).HasMaxLength(256);
            entity.Property(e => e.StreamUrl).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Host).WithMany(p => p.LiveSessions)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LiveSessions_Host");

            entity.HasOne(d => d.Tenant).WithMany(p => p.LiveSessions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LiveSessions_Tenant");
        });

        modelBuilder.Entity<MarketplaceCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marketpl__3214EC07C5ED33D0");

            entity.HasIndex(e => e.Slug, "UQ_Categories_Slug").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(100);
        });

        modelBuilder.Entity<MarketplaceListing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marketpl__3214EC0730F2CC2F");

            entity.HasIndex(e => e.Category, "IX_Listings_Category");

            entity.HasIndex(e => e.Rating, "IX_Listings_Rating");

            entity.HasIndex(e => e.Status, "IX_Listings_Status");

            entity.HasIndex(e => e.TenantId, "IX_Listings_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .HasDefaultValue("standard");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
            entity.Property(e => e.RejectionReason).HasMaxLength(1000);
            entity.Property(e => e.ShortDescription).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("draft");
            entity.Property(e => e.Tags).HasMaxLength(1000);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasDefaultValue("1.0");

            entity.HasOne(d => d.Product).WithMany(p => p.MarketplaceListings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Listings_Product");

            entity.HasOne(d => d.Tenant).WithMany(p => p.MarketplaceListings)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Listings_Tenant");
        });

        modelBuilder.Entity<MarketplaceListingMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marketpl__3214EC0773A5D5D1");

            entity.HasIndex(e => new { e.ListingId, e.Date, e.Source }, "UQ_ListingMetrics").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasDefaultValue("direct");

            entity.HasOne(d => d.Listing).WithMany(p => p.MarketplaceListingMetrics)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("FK_ListingMetrics_Listing");
        });

        modelBuilder.Entity<MarketplaceReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marketpl__3214EC07EC6DE123");

            entity.HasIndex(e => new { e.ListingId, e.CustomerId }, "UQ_Reviews_ListingCustomer").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsVisible).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Listing).WithMany(p => p.MarketplaceReviews)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("FK_Reviews_Listing");

            entity.HasOne(d => d.Tenant).WithMany(p => p.MarketplaceReviews)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Tenant");
        });

        modelBuilder.Entity<MemberSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberSu__3214EC07634BAB03");

            entity.HasIndex(e => new { e.TierId, e.MemberId }, "UQ_MemberSubs").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberSubscriptions)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberSubscriptions_Member");

            entity.HasOne(d => d.Tenant).WithMany(p => p.MemberSubscriptions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberSubscriptions_Tenant");

            entity.HasOne(d => d.Tier).WithMany(p => p.MemberSubscriptions)
                .HasForeignKey(d => d.TierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberSubscriptions_Tier");
        });

        modelBuilder.Entity<MembershipTier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Membersh__3214EC07AA5DBEC5");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BillingInterval)
                .HasMaxLength(50)
                .HasDefaultValue("monthly");
            entity.Property(e => e.Color).HasMaxLength(7);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IconUrl).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.MembershipTiers)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MembershipTiers_Tenant");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07EFEBE3AA");

            entity.HasIndex(e => e.IsRead, "IX_Notifications_IsRead");

            entity.HasIndex(e => e.UserId, "IX_Notifications_UserId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActionUrl).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.NotificationType).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(300);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_User");
        });

        modelBuilder.Entity<NotificationPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07C792FC11");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AgentActivity).HasDefaultValue(true);
            entity.Property(e => e.CommunityUpdates).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EmailEnabled).HasDefaultValue(true);
            entity.Property(e => e.InAppEnabled).HasDefaultValue(true);
            entity.Property(e => e.OrderUpdates).HasDefaultValue(true);
            entity.Property(e => e.PushEnabled).HasDefaultValue(true);
            entity.Property(e => e.SubscriptionUpdates).HasDefaultValue(true);
            entity.Property(e => e.WeeklyDigest).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.NotificationPreferences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_NotifPrefs_User");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07C677631D");

            entity.HasIndex(e => e.CreatedAt, "IX_Orders_CreatedAt");

            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasIndex(e => e.Status, "IX_Orders_Status");

            entity.HasIndex(e => e.TenantId, "IX_Orders_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasDefaultValue("storefront");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");
            entity.Property(e => e.StripePaymentIntentId).HasMaxLength(256);
            entity.Property(e => e.StripeSessionId).HasMaxLength(256);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Tenant");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07BB854BEA");

            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_Product");
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC078B0E9A19");

            entity.ToTable("OrderStatusHistory");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.FromStatus).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.ToStatus).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderStatusHistory_Order");

            entity.HasOne(d => d.Tenant).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderStatusHistory_Tenant");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pages__3214EC07241296C2");

            entity.HasIndex(e => e.AppId, "IX_Pages_AppId");

            entity.HasIndex(e => e.TenantId, "IX_Pages_TenantId");

            entity.HasIndex(e => new { e.AppId, e.Slug }, "UQ_Pages_AppSlug").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.PageType)
                .HasMaxLength(50)
                .HasDefaultValue("custom");
            entity.Property(e => e.SeoDescription).HasMaxLength(500);
            entity.Property(e => e.SeoImage).HasMaxLength(500);
            entity.Property(e => e.SeoTitle).HasMaxLength(200);
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.App).WithMany(p => p.Pages)
                .HasForeignKey(d => d.AppId)
                .HasConstraintName("FK_Pages_App");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Pages)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pages_Tenant");
        });

        modelBuilder.Entity<PageBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PageBloc__3214EC0763983292");

            entity.HasIndex(e => e.PageId, "IX_PageBlocks_PageId");

            entity.HasIndex(e => e.TenantId, "IX_PageBlocks_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BlockType).HasMaxLength(100);
            entity.Property(e => e.ColumnSpan).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsVisible).HasDefaultValue(true);

            entity.HasOne(d => d.Page).WithMany(p => p.PageBlocks)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK_PageBlocks_Page");

            entity.HasOne(d => d.Tenant).WithMany(p => p.PageBlocks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageBlocks_Tenant");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07FA8EA023");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.FailureReason).HasMaxLength(500);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");
            entity.Property(e => e.StripeChargeId).HasMaxLength(256);
            entity.Property(e => e.StripePaymentIntentId).HasMaxLength(256);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Order");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Tenant");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC0795086075");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Last4).HasMaxLength(4);
            entity.Property(e => e.StripePaymentMethodId).HasMaxLength(256);

            entity.HasOne(d => d.Tenant).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethods_Tenant");
        });

        modelBuilder.Entity<PlatformSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC074C9A7DBB");

            entity.HasIndex(e => e.SettingKey, "UQ_PlatformSettings_Key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.SettingKey).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<PlatformSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC07F6770CB6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AiCreditsMonthly).HasDefaultValue(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EmailSendsMonthly).HasDefaultValue(500);
            entity.Property(e => e.MarketplaceFeePercent)
                .HasDefaultValue(15.0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.StorageGbMonthly).HasDefaultValue(1);
            entity.Property(e => e.StripeCustomerId).HasMaxLength(256);
            entity.Property(e => e.StripeSubscriptionId).HasMaxLength(256);
            entity.Property(e => e.TransactionFeePercent)
                .HasDefaultValue(5.0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Tenant).WithMany(p => p.PlatformSubscriptions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlatformSubs_Tenant");
        });

        modelBuilder.Entity<PricingCalculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PricingC__3214EC07580E75A7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.PricingCalculations)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricingCalc_Tenant");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC070C20B1A3");

            entity.HasIndex(e => e.IsPublished, "IX_Products_IsPublished");

            entity.HasIndex(e => e.ProductType, "IX_Products_ProductType");

            entity.HasIndex(e => e.TenantId, "IX_Products_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BillingType)
                .HasMaxLength(50)
                .HasDefaultValue("one_time");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ProductType).HasMaxLength(50);
            entity.Property(e => e.RecurringInterval).HasMaxLength(50);
            entity.Property(e => e.SeoDescription).HasMaxLength(500);
            entity.Property(e => e.SeoTitle).HasMaxLength(200);
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Products)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Tenant");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductV__3214EC07C60A4243");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BillingType)
                .HasMaxLength(50)
                .HasDefaultValue("one_time");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RecurringInterval).HasMaxLength(50);
            entity.Property(e => e.VariantName).HasMaxLength(200);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVariants_Product");

            entity.HasOne(d => d.Tenant).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariants_Tenant");
        });

        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuizAtte__3214EC07E8014F43");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttemptNumber).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Quiz");

            entity.HasOne(d => d.Student).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Student");

            entity.HasOne(d => d.Tenant).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Tenant");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC0720717820");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_User");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Refunds__3214EC07BA8B2C16");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("pending");
            entity.Property(e => e.StripeRefundId).HasMaxLength(256);

            entity.HasOne(d => d.Order).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Refunds_Order");

            entity.HasOne(d => d.Payment).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_Refunds_Payment");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Refunds_Tenant");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0717150F21");

            entity.HasIndex(e => new { e.TenantId, e.Name }, "UQ_Roles_TenantName").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Roles)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_Roles_Tenant");
        });

        modelBuilder.Entity<SecurityAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Security__3214EC0740A7ED57");

            entity.HasIndex(e => e.CreatedAt, "IX_SecurityAuditLogs_CreatedAt");

            entity.HasIndex(e => e.TenantId, "IX_SecurityAuditLogs_TenantId");

            entity.HasIndex(e => e.UserId, "IX_SecurityAuditLogs_UserId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Action).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.RiskLevel)
                .HasMaxLength(50)
                .HasDefaultValue("low");
            entity.Property(e => e.UserAgent).HasMaxLength(1000);

            entity.HasOne(d => d.Tenant).WithMany(p => p.SecurityAuditLogs)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SecurityAuditLogs_Tenant");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07FB182881");

            entity.HasIndex(e => e.CustomerId, "IX_Subscriptions_CustomerId");

            entity.HasIndex(e => e.Status, "IX_Subscriptions_Status");

            entity.HasIndex(e => e.TenantId, "IX_Subscriptions_TenantId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
            entity.Property(e => e.StripeSubscriptionId).HasMaxLength(256);

            entity.HasOne(d => d.Product).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscriptions_Product");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscriptions_Tenant");
        });

        modelBuilder.Entity<TeamInvite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamInvi__3214EC078983E5EF");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Token).HasMaxLength(256);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TeamInvites)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_TeamInvites_Tenant");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamMemb__3214EC0782B12357");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("member");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_TeamMembers_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TeamMembers_User");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Template__3214EC0706C952BD");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Templates)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_Templates_Tenant");
        });

        modelBuilder.Entity<TemplateRemix>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Template__3214EC07E0A74AD2");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(50)
                .HasDefaultValue("standard");
            entity.Property(e => e.RoyaltyPercent).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.OriginalTemplate).WithMany(p => p.TemplateRemixes)
                .HasForeignKey(d => d.OriginalTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TemplateRemixes_Template");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TemplateRemixes)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TemplateRemixes_Tenant");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tenants__3214EC07D5D4D930");

            entity.HasIndex(e => e.Slug, "IX_Tenants_Slug");

            entity.HasIndex(e => e.Status, "IX_Tenants_Status");

            entity.HasIndex(e => e.Slug, "UQ_Tenants_Slug").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ContactEmail).HasMaxLength(320);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Plan)
                .HasMaxLength(50)
                .HasDefaultValue("free");
            entity.Property(e => e.PrimaryColor).HasMaxLength(7);
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");
        });

        modelBuilder.Entity<UsageAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsageAle__3214EC076633BB24");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AlertEmail).HasMaxLength(320);
            entity.Property(e => e.AlertWebhookUrl).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MeterType).HasMaxLength(100);
            entity.Property(e => e.ThresholdPercent).HasDefaultValue(80);

            entity.HasOne(d => d.Tenant).WithMany(p => p.UsageAlerts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsageAlerts_Tenant");
        });

        modelBuilder.Entity<UsageMeter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsageMet__3214EC0700B20F48");

            entity.HasIndex(e => e.TenantId, "IX_UsageMeters_TenantId");

            entity.HasIndex(e => new { e.TenantId, e.MeterType, e.PeriodStart }, "UQ_UsageMeters").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AlertThresholdPercent).HasDefaultValue(80);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MeterType).HasMaxLength(100);

            entity.HasOne(d => d.Tenant).WithMany(p => p.UsageMeters)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsageMeters_Tenant");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070B87CEB8");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Locale)
                .HasMaxLength(10)
                .HasDefaultValue("en");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.TimeZone)
                .HasMaxLength(100)
                .HasDefaultValue("UTC");
        });

        modelBuilder.Entity<UserConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserCons__3214EC073612398E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ConsentText).HasMaxLength(2000);
            entity.Property(e => e.ConsentType).HasMaxLength(100);
            entity.Property(e => e.GrantedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IpAddress).HasMaxLength(45);

            entity.HasOne(d => d.Tenant).WithMany(p => p.UserConsents)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserConsents_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.UserConsents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserConsents_User");
        });

        modelBuilder.Entity<UserMfaSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMfaS__3214EC0792EAB48C");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.MfaType)
                .HasMaxLength(50)
                .HasDefaultValue("totp");
            entity.Property(e => e.RecoveryEmail).HasMaxLength(320);
            entity.Property(e => e.SecretKey).HasMaxLength(256);

            entity.HasOne(d => d.User).WithMany(p => p.UserMfaSettings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserMfaSettings_User");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSess__3214EC0756747A1C");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DeviceInfo).HasMaxLength(500);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.LastActiveAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.UserAgent).HasMaxLength(1000);

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserSessions_User");
        });

        modelBuilder.Entity<UserTenantMembership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTena__3214EC07E91422AD");

            entity.HasIndex(e => e.TenantId, "IX_UserTenantMemberships_TenantId");

            entity.HasIndex(e => e.UserId, "IX_UserTenantMemberships_UserId");

            entity.HasIndex(e => new { e.UserId, e.TenantId }, "UQ_UserTenantMemberships").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("member");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Tenant).WithMany(p => p.UserTenantMemberships)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_UserTenantMemberships_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.UserTenantMemberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTenantMemberships_User");
        });

        modelBuilder.Entity<Webhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Webhooks__3214EC07D7E63C0F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Events).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Secret).HasMaxLength(256);
            entity.Property(e => e.Url).HasMaxLength(1000);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Webhooks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Webhooks_Tenant");
        });

        modelBuilder.Entity<WebhookDelivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WebhookD__3214EC07FED1AC58");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AttemptCount).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Event).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("success");

            entity.HasOne(d => d.Tenant).WithMany(p => p.WebhookDeliveries)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WebhookDeliveries_Tenant");

            entity.HasOne(d => d.Webhook).WithMany(p => p.WebhookDeliveries)
                .HasForeignKey(d => d.WebhookId)
                .HasConstraintName("FK_WebhookDeliveries_Webhook");
        });

        modelBuilder.Entity<Workspace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workspac__3214EC0711805790");

            entity.HasIndex(e => e.TenantId, "IX_Workspaces_TenantId");

            entity.HasIndex(e => new { e.TenantId, e.Slug }, "UQ_Workspaces_TenantSlug").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.CustomDomain).HasMaxLength(253);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Slug).HasMaxLength(200);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Workspaces)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_Workspaces_Tenant");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
