using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Tenant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? LogoUrl { get; set; }

    public string? PrimaryColor { get; set; }

    public string? ContactEmail { get; set; }

    public string Plan { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? TrialEndsAt { get; set; }

    public string? Settings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AbTest> AbTests { get; set; } = new List<AbTest>();

    public virtual ICollection<AffiliateConversion> AffiliateConversions { get; set; } = new List<AffiliateConversion>();

    public virtual ICollection<AffiliateLink> AffiliateLinks { get; set; } = new List<AffiliateLink>();

    public virtual ICollection<AffiliateMember> AffiliateMembers { get; set; } = new List<AffiliateMember>();

    public virtual ICollection<AffiliatePayout> AffiliatePayouts { get; set; } = new List<AffiliatePayout>();

    public virtual ICollection<AffiliateProgram> AffiliatePrograms { get; set; } = new List<AffiliateProgram>();

    public virtual ICollection<AiAgentAuditLog> AiAgentAuditLogs { get; set; } = new List<AiAgentAuditLog>();

    public virtual ICollection<AiAgentTask> AiAgentTasks { get; set; } = new List<AiAgentTask>();

    public virtual ICollection<AiAgent> AiAgents { get; set; } = new List<AiAgent>();

    public virtual ICollection<AiGenerationSession> AiGenerationSessions { get; set; } = new List<AiGenerationSession>();

    public virtual ICollection<AnalyticsEvent> AnalyticsEvents { get; set; } = new List<AnalyticsEvent>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<AppComponent> AppComponents { get; set; } = new List<AppComponent>();

    public virtual ICollection<AppDatabase> AppDatabases { get; set; } = new List<AppDatabase>();

    public virtual ICollection<AppDeployment> AppDeployments { get; set; } = new List<AppDeployment>();

    public virtual ICollection<AppFeature> AppFeatures { get; set; } = new List<AppFeature>();

    public virtual ICollection<AppLog> AppLogs { get; set; } = new List<AppLog>();

    public virtual ICollection<App> Apps { get; set; } = new List<App>();

    public virtual ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();

    public virtual ICollection<Attribution> Attributions { get; set; } = new List<Attribution>();

    public virtual ICollection<CoachingBooking> CoachingBookings { get; set; } = new List<CoachingBooking>();

    public virtual ICollection<CoachingSessionNote> CoachingSessionNotes { get; set; } = new List<CoachingSessionNote>();

    public virtual ICollection<CoachingSlot> CoachingSlots { get; set; } = new List<CoachingSlot>();

    public virtual ICollection<CohortGradingSheet> CohortGradingSheets { get; set; } = new List<CohortGradingSheet>();

    public virtual ICollection<CommunityComment> CommunityComments { get; set; } = new List<CommunityComment>();

    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();

    public virtual ICollection<CommunitySpace> CommunitySpaces { get; set; } = new List<CommunitySpace>();

    public virtual ICollection<ContentDraft> ContentDrafts { get; set; } = new List<ContentDraft>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseLesson> CourseLessons { get; set; } = new List<CourseLesson>();

    public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

    public virtual ICollection<CourseQuiz> CourseQuizzes { get; set; } = new List<CourseQuiz>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<CrmActivity> CrmActivities { get; set; } = new List<CrmActivity>();

    public virtual ICollection<CrmContact> CrmContacts { get; set; } = new List<CrmContact>();

    public virtual ICollection<CrmSegment> CrmSegments { get; set; } = new List<CrmSegment>();

    public virtual ICollection<CrmTag> CrmTags { get; set; } = new List<CrmTag>();

    public virtual ICollection<CustomReport> CustomReports { get; set; } = new List<CustomReport>();

    public virtual ICollection<DealPipeline> DealPipelines { get; set; } = new List<DealPipeline>();

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual ICollection<DigitalAsset> DigitalAssets { get; set; } = new List<DigitalAsset>();

    public virtual ICollection<DirectMessage> DirectMessages { get; set; } = new List<DirectMessage>();

    public virtual ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();

    public virtual ICollection<DomainSetting> DomainSettings { get; set; } = new List<DomainSetting>();

    public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; } = new List<EmailCampaign>();

    public virtual ICollection<EmailEvent> EmailEvents { get; set; } = new List<EmailEvent>();

    public virtual ICollection<EmailList> EmailLists { get; set; } = new List<EmailList>();

    public virtual ICollection<EmailSequenceEnrollment> EmailSequenceEnrollments { get; set; } = new List<EmailSequenceEnrollment>();

    public virtual ICollection<EmailSequence> EmailSequences { get; set; } = new List<EmailSequence>();

    public virtual ICollection<EmailSubscriber> EmailSubscribers { get; set; } = new List<EmailSubscriber>();

    public virtual ICollection<EmailSuppressionList> EmailSuppressionLists { get; set; } = new List<EmailSuppressionList>();

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<LeadMagnet> LeadMagnets { get; set; } = new List<LeadMagnet>();

    public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

    public virtual ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();

    public virtual ICollection<MarketplaceListing> MarketplaceListings { get; set; } = new List<MarketplaceListing>();

    public virtual ICollection<MarketplaceReview> MarketplaceReviews { get; set; } = new List<MarketplaceReview>();

    public virtual ICollection<MemberSubscription> MemberSubscriptions { get; set; } = new List<MemberSubscription>();

    public virtual ICollection<MembershipTier> MembershipTiers { get; set; } = new List<MembershipTier>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PageBlock> PageBlocks { get; set; } = new List<PageBlock>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PlatformSubscription> PlatformSubscriptions { get; set; } = new List<PlatformSubscription>();

    public virtual ICollection<PricingCalculation> PricingCalculations { get; set; } = new List<PricingCalculation>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<SecurityAuditLog> SecurityAuditLogs { get; set; } = new List<SecurityAuditLog>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<TeamInvite> TeamInvites { get; set; } = new List<TeamInvite>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<TemplateRemix> TemplateRemixes { get; set; } = new List<TemplateRemix>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<UsageAlert> UsageAlerts { get; set; } = new List<UsageAlert>();

    public virtual ICollection<UsageMeter> UsageMeters { get; set; } = new List<UsageMeter>();

    public virtual ICollection<UserConsent> UserConsents { get; set; } = new List<UserConsent>();

    public virtual ICollection<UserTenantMembership> UserTenantMemberships { get; set; } = new List<UserTenantMembership>();

    public virtual ICollection<WebhookDelivery> WebhookDeliveries { get; set; } = new List<WebhookDelivery>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
}
