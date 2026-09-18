using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? TimeZone { get; set; }

    public string? Locale { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool OnboardingCompleted { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AffiliateMember> AffiliateMembers { get; set; } = new List<AffiliateMember>();

    public virtual ICollection<AiGenerationSession> AiGenerationSessions { get; set; } = new List<AiGenerationSession>();

    public virtual ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();

    public virtual ICollection<CoachingBooking> CoachingBookings { get; set; } = new List<CoachingBooking>();

    public virtual ICollection<CoachingSlot> CoachingSlots { get; set; } = new List<CoachingSlot>();

    public virtual ICollection<CommunityComment> CommunityComments { get; set; } = new List<CommunityComment>();

    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();

    public virtual ICollection<ContentDraft> ContentDrafts { get; set; } = new List<ContentDraft>();

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<DirectMessage> DirectMessageReceivers { get; set; } = new List<DirectMessage>();

    public virtual ICollection<DirectMessage> DirectMessageSenders { get; set; } = new List<DirectMessage>();

    public virtual ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();

    public virtual ICollection<MemberSubscription> MemberSubscriptions { get; set; } = new List<MemberSubscription>();

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; set; } = new List<NotificationPreference>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<UserConsent> UserConsents { get; set; } = new List<UserConsent>();

    public virtual ICollection<UserMfaSetting> UserMfaSettings { get; set; } = new List<UserMfaSetting>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    public virtual ICollection<UserTenantMembership> UserTenantMemberships { get; set; } = new List<UserTenantMembership>();
}
