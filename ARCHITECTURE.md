# Creator OS — Full Architecture Plan

## 1. High-Level System Architecture

```
┌─────────────────────────────────────────────────────────────────────┐
│                        CLIENT LAYER                                 │
├──────────────────┬──────────────────┬───────────────────────────────┤
│   React.js Web   │  Flutter Mobile  │   Creator-Facing Web Apps     │
│   (Admin Panel,  │   (iOS/Android)  │   (Published by creators)     │
│    Dashboard)    │                  │                               │
└────────┬─────────┴────────┬─────────┴───────────────┬───────────────┘
         │                  │                         │
         ▼                  ▼                         ▼
┌─────────────────────────────────────────────────────────────────────┐
│                      API GATEWAY / BFF                              │
│           (ASP.NET Core — Rate Limiting, Auth, Routing)            │
└────────────────────────┬────────────────────────────────────────────┘
                         │
         ┌───────────────┼───────────────┬──────────────┐
         ▼               ▼               ▼              ▼
┌──────────────┐ ┌──────────────┐ ┌───────────┐ ┌──────────────┐
│  Identity &  │ │  Core API    │ │  AI Svc   │ │  Commerce &  │
│  Auth Svc    │ │  (CRUD, Mgmt)│ │ (Agents,  │ │  Marketplace │
│              │ │              │ │  Gen)     │ │  Svc         │
└──────┬───────┘ └──────┬───────┘ └─────┬─────┘ └──────┬───────┘
       │                │               │              │
       ▼                ▼               ▼              ▼
┌─────────────────────────────────────────────────────────────────────┐
│                     SHARED INFRASTRUCTURE                           │
├──────────────┬──────────────┬────────────┬─────────────┬───────────┤
│  SQL Server  │  Redis Cache │  Azure Blob│  RabbitMQ / │  SignalR  │
│  (Primary DB)│              │  Storage   │  Hangfire   │  (Realtime)│
└──────────────┴──────────────┴────────────┴─────────────┴───────────┘
```

### Core Principles
- **Multi-tenant from Day 1** — every table has `TenantId`, every query is scoped
- **Vertical Slice Architecture** — features are organized by business domain, not technical layers
- **Shared Kernel** — common infrastructure (auth, billing, events, tenant) lives in shared projects
- **CQRS where needed** — separate read/write models for high-traffic domains (analytics, marketplace)
- **Event-driven** — domain events via message bus for decoupling services

---

## 2. ASP.NET Backend Architecture

### Solution Structure

```
CreatorOS/
├── src/
│   ├── CreatorOS.Api/                    # Entry point, controllers, middleware
│   ├── CreatorOS.Api.Admin/              # Admin-only API endpoints
│   ├── CreatorOS.Api.Public/             # Public marketplace/consumer API
│   │
│   ├── CreatorOS.Domain/                 # Domain entities, value objects, domain events
│   │   ├── Identity/
│   │   ├── Workspace/
│   │   ├── Website/
│   │   ├── AppBuilder/
│   │   ├── Commerce/
│   │   ├── Marketplace/
│   │   ├── Courses/
│   │   ├── Memberships/
│   │   ├── CRM/
│   │   ├── Analytics/
│   │   ├── AI/
│   │   ├── Billing/
│   │   └── Shared/
│   │
│   ├── CreatorOS.Application/            # CQRS handlers, validators, DTOs, services
│   │   ├── Identity/
│   │   ├── Workspace/
│   │   ├── Website/
│   │   ├── AppBuilder/
│   │   ├── Commerce/
│   │   ├── Marketplace/
│   │   ├── Courses/
│   │   ├── Memberships/
│   │   ├── CRM/
│   │   ├── Analytics/
│   │   ├── AI/
│   │   └── Billing/
│   │
│   ├── CreatorOS.Infrastructure/         # EF Core, external services, repository impls
│   │   ├── Persistence/
│   │   ├── Identity/
│   │   ├── Storage/
│   │   ├── Email/
│   │   ├── Payments/
│   │   ├── AI/
│   │   ├── Caching/
│   │   └── Messaging/
│   │
│   ├── CreatorOS.Infrastructure.AI/      # AI agents, model routing, generation
│   ├── CreatorOS.Infrastructure.Marketplace/  # Marketplace-specific logic
│   ├── CreatorOS.Infrastructure.Realtime/    # SignalR hubs
│   │
│   └── CreatorOS.Shared/                 # Cross-cutting: Tenant, Auth, Events, Exceptions
│       ├── Tenant/
│       ├── Auth/
│       ├── Events/
│       ├── Extensions/
│       └── Middleware/
│
├── tests/
│   ├── CreatorOS.UnitTests/
│   ├── CreatorOS.IntegrationTests/
│   └── CreatorOS.ArchTests/
│
└── CreatorOS.sln
```

### API Design Patterns

```
REST API Naming Convention:
─────────────────────────────
GET    /api/v1/workspaces                    → List workspaces
POST   /api/v1/workspaces                    → Create workspace
GET    /api/v1/workspaces/{id}               → Get workspace
PUT    /api/v1/workspaces/{id}               → Update workspace
DELETE /api/v1/workspaces/{id}               → Delete workspace

GET    /api/v1/workspaces/{id}/apps          → List apps in workspace
POST   /api/v1/workspaces/{id}/apps          → Create app in workspace
POST   /api/v1/workspaces/{id}/apps/generate → AI generate app

GET    /api/v1/marketplace/products          → Browse marketplace
POST   /api/v1/marketplace/products/{id}/purchase → Purchase product

GET    /api/v1/creator/dashboard             → Creator dashboard data
GET    /api/v1/creator/analytics             → Creator analytics

Admin:
GET    /api/admin/v1/platform/stats
GET    /api/admin/v1/workspaces
POST   /api/admin/v1/marketplace/{id}/approve
```

### Key Middleware Pipeline

```
Request → RateLimiting → CORS → Auth → TenantResolution → 
Localization → Logging → ExceptionHandling → Controller
```

### Background Jobs (Hangfire)

| Job | Schedule | Purpose |
|-----|----------|---------|
| `AnalyticsAggregationJob` | Every 15 min | Aggregate revenue, traffic, conversion data |
| `AIContentGenerationJob` | On-demand | Generate content, emails, campaigns |
| `MarketplaceReindexJob` | On-demand | Reindex search after listing changes |
| `EmailSequenceProcessor` | Every 1 min | Process queued email sequences |
| `UsageMeteringJob` | Every 5 min | Meter AI, storage, email usage |
| `PayoutProcessorJob` | Daily | Process creator payouts via Stripe Connect |
| `AffiliateAttributionJob` | Real-time + batch | Track and attribute affiliate sales |
| `MarketplaceRankingJob` | Every hour | Update trending, featured rankings |
| `HealthCheckJob` | Every 5 min | Monitor platform health, error rates |

---

## 3. SQL Server Database Design

### Multi-Tenancy Strategy

**Approach: Shared Database + TenantId Column (Discriminator)**

```sql
-- Every table has TenantId
CREATE TABLE Tenants (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NOT NULL UNIQUE,
    Plan NVARCHAR(50) NOT NULL DEFAULT 'free',
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    Settings NVARCHAR(MAX),  -- JSON
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

-- Tenant-scoped query filter in EF Core
modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == _tenantId);
```

### Core Schema

```sql
-- ═══════════════════════════════════════
-- IDENTITY & AUTH
-- ═══════════════════════════════════════
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Email NVARCHAR(320) NOT NULL UNIQUE,
    DisplayName NVARCHAR(200),
    AvatarUrl NVARCHAR(2000),
    EmailConfirmed BIT NOT NULL DEFAULT 0,
    LastLoginAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE UserTenantMemberships (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL REFERENCES Tenants(Id),
    Role NVARCHAR(50) NOT NULL, -- owner, admin, editor, support, finance, analyst
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    CreatedAt DATETIME2 NOT NULL,
    UNIQUE(UserId, TenantId)
);

CREATE TABLE RefreshTokens (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Token NVARCHAR(500) NOT NULL UNIQUE,
    ExpiresAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    RevokedAt DATETIME2
);

-- ═══════════════════════════════════════
-- WORKSPACE & APPS
-- ═══════════════════════════════════════
CREATE TABLE Workspaces (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL REFERENCES Tenants(Id),
    Name NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NOT NULL,
    CustomDomain NVARCHAR(500),
    Settings NVARCHAR(MAX),  -- JSON
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, Slug)
);

CREATE TABLE Apps (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL REFERENCES Tenants(Id),
    WorkspaceId UNIQUEIDENTIFIER NOT NULL REFERENCES Workspaces(Id),
    Name NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NOT NULL,
    Description NVARCHAR(2000),
    Type NVARCHAR(50) NOT NULL, -- website, webapp, storefront, course, membership
    Status NVARCHAR(50) NOT NULL DEFAULT 'draft', -- draft, published, archived
    Config NVARCHAR(MAX),  -- JSON: theme, pages, components, settings
    CustomDomain NVARCHAR(500),
    PublishedVersion INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, Slug)
);

CREATE TABLE AppVersions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AppId UNIQUEIDENTIFIER NOT NULL REFERENCES Apps(Id),
    VersionNumber INT NOT NULL,
    Config NVARCHAR(MAX) NOT NULL,  -- Full snapshot
    Changelog NVARCHAR(2000),
    CreatedBy UNIQUEIDENTIFIER REFERENCES Users(Id),
    CreatedAt DATETIME2 NOT NULL,
    UNIQUE(AppId, VersionNumber)
);

-- ═══════════════════════════════════════
-- WEBSITE BUILDER
-- ═══════════════════════════════════════
CREATE TABLE Pages (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AppId UNIQUEIDENTIFIER NOT NULL REFERENCES Apps(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Slug NVARCHAR(200) NOT NULL,
    PageType NVARCHAR(50) NOT NULL, -- home, landing, sales, about, blog, product, custom
    Blocks NVARCHAR(MAX),  -- JSON: ordered array of blocks
    SEO NVARCHAR(MAX),     -- JSON: meta title, description, og image
    IsPublished BIT NOT NULL DEFAULT 0,
    SortOrder INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    UNIQUE(AppId, Slug)
);

CREATE TABLE Blocks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    PageId UNIQUEIDENTIFIER NOT NULL REFERENCES Pages(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    BlockType NVARCHAR(100) NOT NULL, -- hero, text, image, form, pricing, testimonial, etc.
    Config NVARCHAR(MAX) NOT NULL,  -- JSON: block-specific config
    SortOrder INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- COMMERCE
-- ═══════════════════════════════════════
CREATE TABLE Products (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    AppId UNIQUEIDENTIFIER NOT NULL REFERENCES Apps(Id),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(4000),
    ProductType NVARCHAR(50) NOT NULL, -- digital, course, membership, coaching, subscription, app
    PriceCents BIGINT NOT NULL DEFAULT 0,
    Currency NVARCHAR(3) NOT NULL DEFAULT 'USD',
    BillingType NVARCHAR(50) NOT NULL, -- one_time, recurring, usage, freemium
    RecurringInterval NVARCHAR(50), -- monthly, yearly
    TrialDays INT DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    ThumbnailUrl NVARCHAR(2000),
    DigitalAssets NVARCHAR(MAX),  -- JSON: download links, file references
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE TABLE Orders (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    CustomerId UNIQUEIDENTIFIER REFERENCES Contacts(Id),
    StripeSessionId NVARCHAR(500),
    StripePaymentIntentId NVARCHAR(500),
    Status NVARCHAR(50) NOT NULL, -- pending, completed, failed, refunded, disputed
    SubtotalCents BIGINT NOT NULL,
    DiscountCents BIGINT NOT NULL DEFAULT 0,
    TotalCents BIGINT NOT NULL,
    Currency NVARCHAR(3) NOT NULL DEFAULT 'USD',
    AffiliateId UNIQUEIDENTIFIER,
    Source NVARCHAR(50) NOT NULL, -- storefront, marketplace, affiliate, direct
    Metadata NVARCHAR(MAX),  -- JSON
    CreatedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2
);

CREATE TABLE OrderItems (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    OrderId UNIQUEIDENTIFIER NOT NULL REFERENCES Orders(Id),
    ProductId UNIQUEIDENTIFIER NOT NULL REFERENCES Products(Id),
    Quantity INT NOT NULL DEFAULT 1,
    UnitPriceCents BIGINT NOT NULL,
    TotalCents BIGINT NOT NULL,
    SubscriptionId UNIQUEIDENTIFIER
);

CREATE TABLE Subscriptions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL REFERENCES Contacts(Id),
    ProductId UNIQUEIDENTIFIER NOT NULL REFERENCES Products(Id),
    StripeSubscriptionId NVARCHAR(500),
    Status NVARCHAR(50) NOT NULL, -- active, past_due, canceled, trialing, paused
    CurrentPeriodStart DATETIME2,
    CurrentPeriodEnd DATETIME2,
    CanceledAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE Coupons (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Code NVARCHAR(100) NOT NULL,
    DiscountType NVARCHAR(50) NOT NULL, -- percentage, fixed
    DiscountValueCents BIGINT NOT NULL,
    MaxUses INT,
    UsesCount INT NOT NULL DEFAULT 0,
    ValidFrom DATETIME2 NOT NULL,
    ValidUntil DATETIME2,
    ApplicableProductIds NVARCHAR(MAX),  -- JSON array
    CreatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, Code)
);

-- ═══════════════════════════════════════
-- MARKETPLACE
-- ═══════════════════════════════════════
CREATE TABLE MarketplaceListings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL REFERENCES Products(Id),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(4000),
    Category NVARCHAR(100) NOT NULL,
    Tags NVARCHAR(MAX),  -- JSON array
    Status NVARCHAR(50) NOT NULL, -- pending, approved, rejected, suspended
    ApprovalNote NVARCHAR(2000),
    CommissionRatePercent DECIMAL(5,2) NOT NULL DEFAULT 15.00,
    FeaturedAt DATETIME2,
    TrendingScore DECIMAL(10,4) DEFAULT 0,
    ViewCount INT NOT NULL DEFAULT 0,
    PurchaseCount INT NOT NULL DEFAULT 0,
    Rating DECIMAL(3,2),
    ReviewCount INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    UNIQUE(ProductId)
);

CREATE TABLE MarketplaceReviews (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ListingId UNIQUEIDENTIFIER NOT NULL REFERENCES MarketplaceListings(Id),
    BuyerId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Rating INT NOT NULL, -- 1-5
    Title NVARCHAR(200),
    Body NVARCHAR(4000),
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- CRM & CONTACTS
-- ═══════════════════════════════════════
CREATE TABLE Contacts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Email NVARCHAR(320) NOT NULL,
    DisplayName NVARCHAR(200),
    Phone NVARCHAR(50),
    AvatarUrl NVARCHAR(2000),
    Tags NVARCHAR(MAX),  -- JSON array
    CustomFields NVARCHAR(MAX),  -- JSON
    SegmentIds NVARCHAR(MAX),   -- JSON array
    Status NVARCHAR(50) NOT NULL DEFAULT 'lead', -- lead, subscriber, customer, churned
    TotalSpentCents BIGINT NOT NULL DEFAULT 0,
    OrderCount INT NOT NULL DEFAULT 0,
    LastActivityAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, Email)
);

CREATE TABLE ContactSegments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    FilterRules NVARCHAR(MAX),  -- JSON: conditions for auto-segmentation
    ContactCount INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- COURSES
-- ═══════════════════════════════════════
CREATE TABLE Courses (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER REFERENCES Products(Id),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(4000),
    ThumbnailUrl NVARCHAR(2000),
    Status NVARCHAR(50) NOT NULL DEFAULT 'draft',
    Settings NVARCHAR(MAX),  -- JSON: enrollment, completion rules
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE TABLE CourseModules (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CourseId UNIQUEIDENTIFIER NOT NULL REFERENCES Courses(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE CourseLessons (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ModuleId UNIQUEIDENTIFIER NOT NULL REFERENCES CourseModules(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    ContentType NVARCHAR(50) NOT NULL, -- video, text, quiz, download
    Content NVARCHAR(MAX),  -- JSON: text content, video URL, quiz questions
    DurationSeconds INT,
    SortOrder INT NOT NULL DEFAULT 0,
    IsFreePreview BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE CourseEnrollments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CourseId UNIQUEIDENTIFIER NOT NULL REFERENCES Courses(Id),
    ContactId UNIQUEIDENTIFIER NOT NULL REFERENCES Contacts(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ProgressPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
    CompletedAt DATETIME2,
    EnrolledAt DATETIME2 NOT NULL
);

CREATE TABLE LessonProgress (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    EnrollmentId UNIQUEIDENTIFIER NOT NULL REFERENCES CourseEnrollments(Id),
    LessonId UNIQUEIDENTIFIER NOT NULL REFERENCES CourseLessons(Id),
    IsCompleted BIT NOT NULL DEFAULT 0,
    CompletedAt DATETIME2,
    LastAccessedAt DATETIME2
);

-- ═══════════════════════════════════════
-- MEMBERSHIPS & COMMUNITY
-- ═══════════════════════════════════════
CREATE TABLE Memberships (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER REFERENCES Products(Id),
    Name NVARCHAR(200) NOT NULL,
    Tiers NVARCHAR(MAX),  -- JSON: [{name, price, benefits}]
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE CommunityPosts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    MembershipId UNIQUEIDENTIFIER REFERENCES Memberships(Id),
    AuthorId UNIQUEIDENTIFIER NOT NULL REFERENCES Contacts(Id),
    ParentPostId UNIQUEIDENTIFIER REFERENCES CommunityPosts(Id),
    Title NVARCHAR(200),
    Body NVARCHAR(MAX),
    PostType NVARCHAR(50) NOT NULL, -- post, comment, announcement
    IsPinned BIT NOT NULL DEFAULT 0,
    LikeCount INT NOT NULL DEFAULT 0,
    CommentCount INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- COACHING
-- ═══════════════════════════════════════
CREATE TABLE CoachingSlots (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER REFERENCES Products(Id),
    Name NVARCHAR(200) NOT NULL,
    DurationMinutes INT NOT NULL,
    PriceCents BIGINT NOT NULL,
    AvailabilityRules NVARCHAR(MAX),  -- JSON: weekly schedule
    TimeZone NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE CoachingBookings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SlotId UNIQUEIDENTIFIER NOT NULL REFERENCES CoachingSlots(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ClientId UNIQUEIDENTIFIER NOT NULL REFERENCES Contacts(Id),
    StartAt DATETIME2 NOT NULL,
    EndAt DATETIME2 NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- confirmed, completed, canceled, no_show
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- EMAIL & MARKETING
-- ═══════════════════════════════════════
CREATE TABLE EmailCampaigns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Subject NVARCHAR(500),
    BodyHtml NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL, -- draft, scheduled, sending, sent, failed
    SegmentId UNIQUEIDENTIFIER REFERENCES ContactSegments(Id),
    ScheduledAt DATETIME2,
    SentAt DATETIME2,
    Stats NVARCHAR(MAX),  -- JSON: {sent, delivered, opened, clicked, bounced}
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE EmailSequences (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    TriggerType NVARCHAR(50) NOT NULL, -- signup, purchase, custom
    TriggerConfig NVARCHAR(MAX),  -- JSON
    Status NVARCHAR(50) NOT NULL DEFAULT 'draft',
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE EmailSequenceSteps (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SequenceId UNIQUEIDENTIFIER NOT NULL REFERENCES EmailSequences(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    DelayDays INT NOT NULL DEFAULT 0,
    DelayHours INT NOT NULL DEFAULT 0,
    Subject NVARCHAR(500),
    BodyHtml NVARCHAR(MAX),
    SortOrder INT NOT NULL DEFAULT 0
);

-- ═══════════════════════════════════════
-- AI WORKFORCE
-- ═══════════════════════════════════════
CREATE TABLE AIAgents (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    AgentType NVARCHAR(50) NOT NULL, -- ceo, growth, content, email, support, product, analytics, affiliate
    Name NVARCHAR(200) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    Config NVARCHAR(MAX),  -- JSON: agent-specific settings
    LastRunAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE AITasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    AgentId UNIQUEIDENTIFIER NOT NULL REFERENCES AIAgents(Id),
    TaskType NVARCHAR(100) NOT NULL,
    Input NVARCHAR(MAX),  -- JSON
    Output NVARCHAR(MAX), -- JSON
    Status NVARCHAR(50) NOT NULL, -- pending, running, completed, failed, needs_approval
    RequiresApproval BIT NOT NULL DEFAULT 0,
    ApprovedBy UNIQUEIDENTIFIER REFERENCES Users(Id),
    ApprovedAt DATETIME2,
    TokensUsed INT DEFAULT 0,
    CostCents INT DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2
);

CREATE TABLE AIUsageLog (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER REFERENCES Users(Id),
    Feature NVARCHAR(100) NOT NULL, -- app_generation, content, email, agent_action
    Model NVARCHAR(100) NOT NULL,
    InputTokens INT NOT NULL DEFAULT 0,
    OutputTokens INT NOT NULL DEFAULT 0,
    CostCents INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- BILLING & USAGE
-- ═══════════════════════════════════════
CREATE TABLE PlatformSubscriptions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL REFERENCES Tenants(Id),
    Plan NVARCHAR(50) NOT NULL, -- starter, pro, business, enterprise
    StripeSubscriptionId NVARCHAR(500),
    Status NVARCHAR(50) NOT NULL,
    CurrentPeriodStart DATETIME2,
    CurrentPeriodEnd DATETIME2,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE UsageMeters (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    MeterType NVARCHAR(100) NOT NULL, -- ai_generations, storage_mb, emails_sent, bandwidth_gb
    Quantity DECIMAL(18,4) NOT NULL DEFAULT 0,
    PeriodStart DATETIME2 NOT NULL,
    PeriodEnd DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE Invoices (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    StripeInvoiceId NVARCHAR(500),
    AmountCents BIGINT NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- draft, open, paid, void,uncollectible
    LineItems NVARCHAR(MAX),  -- JSON
    PeriodStart DATETIME2,
    PeriodEnd DATETIME2,
    PaidAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- AFFILIATES & REFERRALS
-- ═══════════════════════════════════════
CREATE TABLE Affiliates (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    ContactId UNIQUEIDENTIFIER NOT NULL REFERENCES Contacts(Id),
    Code NVARCHAR(100) NOT NULL,
    CommissionType NVARCHAR(50) NOT NULL, -- percentage, fixed
    CommissionValueCents BIGINT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'active',
    TotalEarningsCents BIGINT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, Code)
);

CREATE TABLE AffiliateSales (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AffiliateId UNIQUEIDENTIFIER NOT NULL REFERENCES Affiliates(Id),
    OrderId UNIQUEIDENTIFIER NOT NULL REFERENCES Orders(Id),
    CommissionCents BIGINT NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- pending, approved, paid
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- TEMPLATES & REMIX
-- ═══════════════════════════════════════
CREATE TABLE Templates (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    CreatorId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(2000),
    Category NVARCHAR(100),
    TemplateType NVARCHAR(50) NOT NULL, -- app, website, product, workflow
    ConfigSnapshot NVARCHAR(MAX) NOT NULL,  -- JSON: full config
    IsRemixable BIT NOT NULL DEFAULT 1,
    RemixCount INT NOT NULL DEFAULT 0,
    CommissionPercent DECIMAL(5,2) DEFAULT 0,
    PriceCents BIGINT DEFAULT 0,
    Status NVARCHAR(50) NOT NULL, -- draft, published
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- ANALYTICS
-- ═══════════════════════════════════════
CREATE TABLE AnalyticsEvents (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    AppId UNIQUEIDENTIFIER,
    EventType NVARCHAR(100) NOT NULL, -- page_view, purchase, signup, app_launch, etc.
    EntityId UNIQUEIDENTIFIER,
    EntityType NVARCHAR(100),
    UserId UNIQUEIDENTIFIER,
    SessionId UNIQUEIDENTIFIER,
    Metadata NVARCHAR(MAX),  -- JSON
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE RevenueSnapshots (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    SnapshotDate DATE NOT NULL,
    MRR DECIMAL(18,2) NOT NULL DEFAULT 0,
    ARR DECIMAL(18,2) NOT NULL DEFAULT 0,
    GMV DECIMAL(18,2) NOT NULL DEFAULT 0,
    MarketplaceGMV DECIMAL(18,2) NOT NULL DEFAULT 0,
    NewSubscriptions INT NOT NULL DEFAULT 0,
    CanceledSubscriptions INT NOT NULL DEFAULT 0,
    ChurnRate DECIMAL(5,4),
    LTV DECIMAL(18,2),
    RefundAmountCents BIGINT NOT NULL DEFAULT 0,
    AffiliatePayoutsCents BIGINT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UNIQUE(TenantId, SnapshotDate)
);

-- ═══════════════════════════════════════
-- AUDIT LOG
-- ═══════════════════════════════════════
CREATE TABLE AuditLogs (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER,
    Action NVARCHAR(100) NOT NULL, -- publish, delete, refund, payout, permission_change
    EntityType NVARCHAR(100) NOT NULL,
    EntityId UNIQUEIDENTIFIER NOT NULL,
    OldValues NVARCHAR(MAX),  -- JSON
    NewValues NVARCHAR(MAX),  -- JSON
    IpAddress NVARCHAR(45),
    CreatedAt DATETIME2 NOT NULL
);

-- ═══════════════════════════════════════
-- INDEXES (Critical for multi-tenant performance)
-- ═══════════════════════════════════════
CREATE INDEX IX_Products_TenantId ON Products(TenantId);
CREATE INDEX IX_Products_TenantId_AppId ON Products(TenantId, AppId);
CREATE INDEX IX_Orders_TenantId ON Orders(TenantId);
CREATE INDEX IX_Orders_TenantId_CreatedAt ON Orders(TenantId, CreatedAt DESC);
CREATE INDEX IX_Contacts_TenantId ON Contacts(TenantId);
CREATE INDEX IX_Contacts_TenantId_Email ON Contacts(TenantId, Email);
CREATE INDEX IX_MarketplaceListings_TenantId ON MarketplaceListings(TenantId);
CREATE INDEX IX_MarketplaceListings_Category_Status ON MarketplaceListings(Category, Status);
CREATE INDEX IX_MarketplaceListings_TrendingScore ON MarketplaceListings(TrendingScore DESC);
CREATE INDEX IX_AnalyticsEvents_TenantId_CreatedAt ON AnalyticsEvents(TenantId, CreatedAt);
CREATE INDEX IX_AnalyticsEvents_TenantId_EventType ON AnalyticsEvents(TenantId, EventType);
CREATE INDEX IX_AuditLogs_TenantId_CreatedAt ON AuditLogs(TenantId, CreatedAt DESC);
CREATE INDEX IX_AIUsageLog_TenantId_CreatedAt ON AIUsageLog(TenantId, CreatedAt);
```

---

## 4. React.js Frontend Architecture

### Project Structure

```
creator-os-web/
├── public/
├── src/
│   ├── app/                          # App shell, routing, layout
│   │   ├── App.tsx
│   │   ├── routes.tsx
│   │   ├── layouts/
│   │   │   ├── DashboardLayout.tsx
│   │   │   ├── PublicLayout.tsx
│   │   │   └── AuthLayout.tsx
│   │   └── providers/
│   │       ├── AuthProvider.tsx
│   │       ├── TenantProvider.tsx
│   │       └── QueryProvider.tsx
│   │
│   ├── features/                     # Feature modules (vertical slices)
│   │   ├── auth/
│   │   │   ├── components/
│   │   │   ├── hooks/
│   │   │   ├── pages/
│   │   │   ├── api/
│   │   │   └── types/
│   │   ├── workspace/
│   │   ├── website-builder/
│   │   │   ├── components/
│   │   │   │   ├── BlockEditor.tsx
│   │   │   │   ├── BlockPalette.tsx
│   │   │   │   ├── PropertyPanel.tsx
│   │   │   │   ├── Canvas.tsx
│   │   │   │   └── blocks/
│   │   │   │       ├── HeroBlock.tsx
│   │   │   │       ├── TextBlock.tsx
│   │   │   │       ├── ImageBlock.tsx
│   │   │   │       ├── FormBlock.tsx
│   │   │   │       ├── PricingBlock.tsx
│   │   │   │       ├── TestimonialBlock.tsx
│   │   │   │       └── ... (all block types)
│   │   │   ├── hooks/
│   │   │   ├── pages/
│   │   │   ├── api/
│   │   │   └── types/
│   │   ├── app-builder/
│   │   │   ├── components/
│   │   │   │   ├── PromptInput.tsx
│   │   │   │   ├── AppPreview.tsx
│   │   │   │   ├── FeatureTree.tsx
│   │   │   │   ├── ConversationalEditor.tsx
│   │   │   │   └── VersionManager.tsx
│   │   │   ├── hooks/
│   │   │   ├── pages/
│   │   │   ├── api/
│   │   │   └── types/
│   │   ├── commerce/
│   │   ├── marketplace/
│   │   ├── courses/
│   │   ├── memberships/
│   │   ├── coaching/
│   │   ├── crm/
│   │   ├── email/
│   │   ├── analytics/
│   │   ├── ai-agents/
│   │   ├── billing/
│   │   ├── settings/
│   │   └── admin/
│   │
│   ├── shared/                       # Shared UI components & utilities
│   │   ├── components/
│   │   │   ├── ui/                   # Design system (Button, Input, Modal, etc.)
│   │   │   ├── layout/
│   │   │   ├── charts/
│   │   │   └── forms/
│   │   ├── hooks/
│   │   ├── lib/
│   │   │   ├── api-client.ts         # Axios instance with interceptors
│   │   │   ├── auth.ts
│   │   │   ├── tenant.ts
│   │   │   └── utils.ts
│   │   ├── types/
│   │   └── constants/
│   │
│   └── styles/
│       ├── globals.css
│       └── tailwind.config.ts
│
├── package.json
├── tsconfig.json
├── vite.config.ts
├── tailwind.config.ts
└── .env.local
```

### Key Libraries

| Purpose | Library |
|---------|---------|
| Build | Vite |
| Styling | Tailwind CSS + shadcn/ui |
| State | TanStack Query (server), Zustand (client) |
| Forms | React Hook Form + Zod |
| Tables | TanStack Table |
| Charts | Recharts |
| Drag & Drop | dnd-kit |
| Rich Text | TipTap |
| Code Editor | Monaco Editor |
| Realtime | SignalR client |
| Routing | React Router v6 |
| Auth | Custom (JWT + refresh tokens) |

---

## 5. Flutter Mobile App Architecture

### Project Structure

```
creator_os_mobile/
├── lib/
│   ├── app/
│   │   ├── app.dart
│   │   ├── routes.dart
│   │   └── theme.dart
│   │
│   ├── core/
│   │   ├── api/
│   │   │   ├── api_client.dart
│   │   │   ├── interceptors/
│   │   │   └── endpoints.dart
│   │   ├── auth/
│   │   │   ├── auth_service.dart
│   │   │   └── auth_guard.dart
│   │   ├── storage/
│   │   └── utils/
│   │
│   ├── features/
│   │   ├── auth/
│   │   │   ├── data/
│   │   │   ├── domain/
│   │   │   └── presentation/
│   │   ├── dashboard/
│   │   ├── website_builder/
│   │   ├── app_builder/
│   │   ├── commerce/
│   │   ├── marketplace/
│   │   ├── courses/
│   │   ├── crm/
│   │   ├── analytics/
│   │   └── settings/
│   │
│   └── shared/
│       ├── widgets/
│       ├── models/
│       └── services/
│
├── pubspec.yaml
└── analysis_options.yaml
```

### Key Flutter Libraries

| Purpose | Library |
|---------|---------|
| State | Riverpod |
| HTTP | Dio + Retrofit |
| Navigation | GoRouter |
| Storage | Flutter Secure Storage + Hive |
| UI | Flutter Material 3 + Custom widgets |
| Charts | fl_chart |
| Forms | Flutter Form Builder |
| Auth | biometric_storage |
| Realtime | signalr_core |
| Image | cached_network_image |
| PDF | syncfusion_flutter_pdfviewer |

---

## 6. Shared Services Architecture

### Authentication Flow

```
┌─────────┐     ┌──────────┐     ┌──────────┐
│  Client  │────▶│  API     │────▶│ Identity │
│          │◀────│ Gateway  │◀────│ Service  │
└─────────┘     └──────────┘     └──────────┘

1. User logs in → POST /auth/login {email, password}
2. Returns {accessToken (15min), refreshToken (30d)}
3. Client stores refreshToken securely (HttpOnly cookie / Secure Storage)
4. API uses accessToken in Authorization header
5. On 401 → client calls POST /auth/refresh with refreshToken
6. On refresh failure → redirect to login
```

```
JWT Token Structure:
{
  "sub": "user-guid",
  "email": "user@example.com",
  "tenants": [
    {"id": "tenant-guid", "role": "owner", "name": "My Business"}
  ],
  "activeTenant": "tenant-guid",
  "iat": 1690000000,
  "exp": 1690000900
}
```

### AI Model Routing

```
┌──────────────┐
│  AI Gateway  │  (Routes requests based on task type, cost, quality)
├──────────────┤
│              │──── GPT-4o      (complex generation, app building)
│              │──── Claude 3.5  (content, long-form writing)
│              │──── GPT-4o-mini (simple tasks, support agent)
│              │──── Whisper     (audio transcription)
│              │──── DALL-E 3    (image generation)
│              │──── Custom      (domain-specific fine-tuned models)
└──────────────┘
```

### Payment Flow (Stripe Connect)

```
1. Creator onboards → Stripe Connect account
2. Customer purchases → Stripe Checkout Session
3. Stripe handles payment → webhook to platform
4. Platform splits: Platform fee + Creator payout
5. Creator payout → Stripe Connect automatic transfer

Marketplace sale: Platform takes higher commission (e.g., 15-20%)
Direct sale: Platform takes SaaS fee only (e.g., 3-5%)
Affiliate sale: Additional commission split to affiliate
```

---

## 7. DevOps & Deployment

### Azure Infrastructure

```
┌─────────────────────────────────────────────────────┐
│                    Azure                             │
├─────────────────────────────────────────────────────┤
│  App Service (Linux) — ASP.NET Core API             │
│  ├── API App (scale: 2-10 instances)                │
│  ├── Admin API (scale: 1-3 instances)               │
│  └── Background Workers (scale: 1-3 instances)      │
│                                                     │
│  Azure SQL Server (General Purpose tier)            │
│  ├── Primary DB                                    │
│  └── Read Replica (analytics queries)              │
│                                                     │
│  Azure Cache for Redis                              │
│  └── Session, cache, rate limiting                  │
│                                                     │
│  Azure Blob Storage                                 │
│  ├── Creator uploads (CDN-fronted)                  │
│  ├── Product digital assets                         │
│  └── Backups                                        │
│                                                     │
│  Azure SignalR Service                              │
│  └── Realtime notifications, live updates           │
│                                                     │
│  Azure Service Bus / RabbitMQ                       │
│  └── Domain events, background job queues           │
│                                                     │
│  Azure CDN                                          │
│  └── Static assets, creator websites, images        │
│                                                     │
│  Azure Front Door                                   │
│  └── Global load balancing, WAF, SSL termination    │
│                                                     │
│  Azure Cognitive Search (optional)                  │
│  └── Marketplace search, recommendations            │
│                                                     │
│  Azure Monitor + Application Insights               │
│  └── Logging, metrics, traces, alerts               │
└─────────────────────────────────────────────────────┘
```

### CI/CD Pipeline

```
GitHub Actions:
──────────────
1. Push to main
2. Build → Test → Lint → Security scan
3. Build Docker image → Push to Azure Container Registry
4. Deploy to staging → Run integration tests
5. Approve → Deploy to production (blue-green)
6. Monitor → Rollback if errors spike

Mobile (Flutter):
─────────────────
1. Push to main
2. Build iOS (ipa) + Android (apk/aab)
3. Upload to TestFlight / Internal Testing
4. Promote to production stores
```

### Environment Config

```
├── .env.development          # Local dev
├── .env.staging              # Staging
├── .env.production           # Production
├── docker-compose.yml        # Local dev services
├── docker-compose.staging.yml
└── Dockerfile
```

---

## 8. Implementation Phases

### Phase 1 — Foundation (Weeks 1-6)
- [ ] Solution structure, shared kernel, multi-tenancy
- [ ] Identity & Auth (JWT, roles, tenant membership)
- [ ] Database schema (core tables, migrations)
- [ ] React app shell + auth flow
- [ ] Flutter app shell + auth flow
- [ ] CI/CD pipeline
- [ ] Azure infrastructure setup

### Phase 2 — Creation Tools (Weeks 7-12)
- [ ] Website builder (blocks, drag-drop, themes)
- [ ] No-code page editor
- [ ] AI app generation (prompt → working app)
- [ ] App versioning, preview, publish
- [ ] Conversational editing flow
- [ ] Custom domain + SSL

### Phase 3 — Commerce (Weeks 13-18)
- [ ] Product catalog (digital, courses, memberships, coaching)
- [ ] Stripe Connect integration
- [ ] Checkout flow
- [ ] Subscription management
- [ ] Coupons, bundles, upsells
- [ ] Creator CRM

### Phase 4 — Marketplace (Weeks 19-22)
- [ ] Marketplace listing & approval
- [ ] Search, categories, filters
- [ ] Reviews & ratings
- [ ] Featured/trending rankings
- [ ] Attribution tracking
- [ ] Remix/template marketplace

### Phase 5 — AI & Growth (Weeks 23-28)
- [ ] AI agent system (8 agents)
- [ ] AI model routing & cost controls
- [ ] Email campaigns & sequences
- [ ] Analytics dashboard
- [ ] Affiliate system
- [ ] Audience-to-business intelligence

### Phase 6 — Polish & Scale (Weeks 29-32)
- [ ] Performance optimization
- [ ] Mobile app polish
- [ ] PWA support
- [ ] iOS/Android packaging workflow
- [ ] Admin dashboard
- [ ] Load testing & security audit
- [ ] Launch prep

---

## 9. Key Architecture Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Multi-tenancy | Shared DB + TenantId | Cost-effective, sufficient for SaaS scale |
| API Style | REST + CQRS | Simplicity with separation of read/write |
| Real-time | SignalR | Native ASP.NET, handles reconnection |
| Background jobs | Hangfire + SQL | Simple, reliable, dashboard included |
| Caching | Redis | Session, rate limiting, hot data |
| Search | SQL FTS + Elastic (marketplace) | Start simple, scale when needed |
| File storage | Azure Blob + CDN | Scalable, cost-effective |
| Auth | Custom JWT | Full control, no external dependency |
| Payments | Stripe Connect | Industry standard for marketplaces |
| AI routing | Custom gateway | Cost control, model flexibility |
| Mobile state | Riverpod | Modern, testable, scalable |
| Web state | TanStack Query + Zustand | Server state + client state separation |

---

## 10. Industrial-Grade Resilience Patterns

### 10.1 Circuit Breaker Pattern

```csharp
// Infrastructure/Resilience/CircuitBreakerPolicy.cs
public static class CircuitBreakerPolicy
{
    public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (result, duration) =>
                {
                    Log.Warning("Circuit OPEN for {Duration}s after {Exceptions}",
                        duration.TotalSeconds, result.Exception?.Message ?? result.Result.StatusCode.ToString());
                },
                onReset: () => Log.Information("Circuit CLOSED"),
                onHalfOpen: () => Log.Information("Circuit HALF-OPEN — testing"));
    }
}

// Registration in DI
services.AddHttpClient("StripeService")
    .AddPolicyHandler(CircuitBreakerPolicy.GetPolicy())
    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>()
        .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));

services.AddHttpClient("AIService")
    .AddPolicyHandler(CircuitBreakerPolicy.GetPolicy())
    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>()
        .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));
```

### 10.2 Idempotency Keys (Critical for Payments)

```sql
-- New table for idempotency
CREATE TABLE IdempotencyKeys (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Key NVARCHAR(200) NOT NULL UNIQUE,
    UserId UNIQUEIDENTIFIER NOT NULL,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    RequestPath NVARCHAR(500) NOT NULL,
    ResponseBody NVARCHAR(MAX),
    StatusCode INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ExpiresAt DATETIME2 NOT NULL  -- Auto-cleanup after 48h
);

CREATE INDEX IX_IdempotencyKeys_Key ON IdempotencyKeys(Key) WHERE ExpiresAt > GETUTCDATE();
```

```csharp
// Middleware/IdempotencyMiddleware.cs
public class IdempotencyMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var idempotencyKey = context.Request.Headers["Idempotency-Key"].FirstOrDefault();

        if (context.Request.Method == "POST" && !string.IsNullOrEmpty(idempotencyKey))
        {
            var existing = await _db.IdempotencyKeys
                .FirstOrDefaultAsync(k => k.Key == idempotencyKey && k.ExpiresAt > DateTime.UtcNow);

            if (existing != null)
            {
                context.Response.StatusCode = existing.StatusCode;
                await context.Response.WriteAsync(existing.ResponseBody);
                return;
            }

            // Capture response, store for future identical requests
            var originalBody = context.Response.Body;
            using var buffer = new MemoryStream();
            context.Response.Body = buffer;

            await _next(context);

            buffer.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(buffer).ReadToEndAsync();

            _db.IdempotencyKeys.Add(new IdempotencyKey
            {
                Key = idempotencyKey,
                UserId = context.User.GetUserId(),
                TenantId = context.GetTenantId(),
                RequestPath = context.Request.Path,
                ResponseBody = responseBody,
                StatusCode = context.Response.StatusCode,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(48)
            });
            await _db.SaveChangesAsync();

            buffer.Seek(0, SeekOrigin.Begin);
            await buffer.CopyToAsync(originalBody);
        }
        else
        {
            await _next(context);
        }
    }
}
```

### 10.3 Outbox Pattern (Reliable Event Publishing)

```sql
-- Outbox table for reliable event publishing
CREATE TABLE OutboxMessages (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    AggregateType NVARCHAR(200) NOT NULL,
    EventType NVARCHAR(200) NOT NULL,
    Payload NVARCHAR(MAX) NOT NULL,  -- JSON
    CreatedAt DATETIME2 NOT NULL,
    ProcessedAt DATETIME2 NULL,
    Error NVARCHAR(MAX) NULL,
    RetryCount INT NOT NULL DEFAULT 0
);

CREATE INDEX IX_OutboxMessages_Unprocessed ON OutboxMessages(CreatedAt)
    WHERE ProcessedAt IS NULL;
```

```csharp
// Usage in domain handler — write to outbox in same transaction as business data
public async Task Handle(CreateOrderCommand command)
{
    using var transaction = await _db.Database.BeginTransactionAsync();

    var order = new Order { /* ... */ };
    _db.Orders.Add(order);

    // Domain event written to outbox — guaranteed delivery
    _db.OutboxMessages.Add(new OutboxMessage
    {
        AggregateId = order.Id,
        AggregateType = "Order",
        EventType = "OrderCreated",
        Payload = JsonSerializer.Serialize(new OrderCreatedEvent(order)),
        CreatedAt = DateTime.UtcNow
    });

    await _db.SaveChangesAsync();
    await transaction.CommitAsync();
}

// Background processor picks up outbox messages
public class OutboxProcessor : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await _db.OutboxMessages
                .Where(m => m.ProcessedAt == null && m.RetryCount < 5)
                .OrderBy(m => m.CreatedAt)
                .Take(50)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {
                try
                {
                    await _eventBus.Publish(message.EventType, message.Payload);
                    message.ProcessedAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    message.RetryCount++;
                    message.Error = ex.Message;
                }
            }
            await _db.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
```

### 10.4 Tenant Isolation Enforcement

```csharp
// Every query is automatically scoped — NO bypass
public class TenantQueryFilterInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return ValueTask.FromResult(result);

        foreach (var entry in context.ChangeTracker.Entries<ITenantEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantContext.TenantId;
            }
            else if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                // BLOCK cross-tenant modifications
                if (entry.Entity.TenantId != _tenantContext.TenantId)
                {
                    throw new ForbiddenException("Cross-tenant access denied");
                }
            }
        }

        return ValueTask.FromResult(result);
    }
}

// Hard tenant guard — every API endpoint
[ServiceFilter(typeof(TenantGuardFilter))]
public class TenantGuardFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tenantId = context.HttpContext.GetTenantId();
        if (tenantId == null || tenantId == Guid.Empty)
        {
            context.Result = new BadRequestObjectResult("Tenant context required");
            return;
        }

        // Verify user belongs to this tenant
        var userId = context.HttpContext.GetUserId();
        var membership = await _db.UserTenantMemberships
            .FirstOrDefaultAsync(m => m.UserId == userId && m.TenantId == tenantId && m.Status == "active");

        if (membership == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        context.HttpContext.Items["UserRole"] = membership.Role;
        await next();
    }
}
```

### 10.5 API Rate Limiting (Per-Tenant)

```csharp
// Program.cs — comprehensive rate limiting
builder.Services.AddRateLimiter(options =>
{
    // Global: 1000 requests per minute per IP
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1000,
                Window = TimeSpan.FromMinutes(1)
            }));

    // AI endpoints: 20 requests per minute per tenant (expensive)
    options.AddPolicy("ai", context =>
        RateLimitPartition.GetTokenBucketLimiter(
            context.GetTenantId()?.ToString() ?? "unknown",
            _ => new TokenBucketRateLimiterOptions
            {
                TokenLimit = 20,
                ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                TokensPerPeriod = 20,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 5
            }));

    // Checkout: 10 requests per minute per tenant
    options.AddPolicy("checkout", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.GetTenantId()?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));

    // Marketplace browsing: 200 requests per minute per IP
    options.AddPolicy("marketplace", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 200,
                Window = TimeSpan.FromMinutes(1)
            }));
});
```

### 10.6 OpenTelemetry (Distributed Tracing)

```csharp
// Program.cs
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddSource("CreatorOS.AI")
            .AddSource("CreatorOS.Commerce")
            .AddSource("CreatorOS.Marketplace")
            .AddOtlpExporter(opts =>
            {
                opts.Endpoint = new Uri(builder.Configuration["Otlp:Endpoint"]!);
            });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddMeter("CreatorOS.Business")
            .AddOtlpExporter(opts =>
            {
                opts.Endpoint = new Uri(builder.Configuration["Otlp:Endpoint"]!);
            });
    });

// Custom business metrics
public static class BusinessMetrics
{
    private static readonly Meter Meter = new("CreatorOS.Business", "1.0");

    public static readonly Counter<long> OrdersCreated =
        Meter.CreateCounter<long>("creator.orders.created", "orders");

    public static readonly Counter<long> RevenueCents =
        Meter.CreateCounter<long>("creator.revenue.cents", "cents");

    public static readonly Histogram<double> AIResponseTime =
        Meter.CreateHistogram<double>("creator.ai.response.time", "ms");

    public static readonly UpDownCounter<long> ActiveTenants =
        Meter.CreateUpDownCounter<long>("creator.tenants.active");
}

// Usage
BusinessMetrics.OrdersCreated.Add(1, new("tenant.id", tenantId), new("source", "marketplace"));
BusinessMetrics.AIResponseTime.Record(elapsedMs, new("model", "gpt-4o"), new("task", "app_generation"));
```

### 10.7 Secrets Management (Azure Key Vault)

```csharp
// Program.cs
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{builder.Configuration["KeyVault:Name"]}.vault.azure.net/"),
    new DefaultAzureCredential());

// Key Vault references in appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=DB-Connection)",
    "Redis": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=Redis-Connection)"
  },
  "Stripe": {
    "SecretKey": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=Stripe-SecretKey)",
    "WebhookSecret": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=Stripe-WebhookSecret)"
  },
  "AI": {
    "OpenAIKey": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=OpenAI-Key)",
    "AnthropicKey": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=Anthropic-Key)"
  },
  "Jwt": {
    "SecretKey": "@Microsoft.KeyVault(VaultName=creatoros-vault;SecretName=JWT-Secret)"
  }
}
```

### 10.8 GDPR / Compliance Features

```sql
-- ═══════════════════════════════════════
-- GDPR & COMPLIANCE
-- ═══════════════════════════════════════
CREATE TABLE DataExportRequests (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- pending, processing, completed, failed
    ExportFormat NVARCHAR(20) NOT NULL DEFAULT 'json',
    DownloadUrl NVARCHAR(2000),
    ExpiresAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2
);

CREATE TABLE DataDeletionRequests (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    TenantId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- pending, processing, completed, failed
    Reason NVARCHAR(2000),
    ScheduledDeletionAt DATETIME2 NOT NULL,  -- 30-day grace period
    CreatedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2
);

CREATE TABLE ConsentRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    ConsentType NVARCHAR(100) NOT NULL, -- marketing_email, analytics, third_party_sharing
    Granted BIT NOT NULL,
    IpAddress NVARCHAR(45),
    UserAgent NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE AuditLogs (
    -- (already exists — add RetentionDays column)
    RetentionDays INT NOT NULL DEFAULT 2555  -- 7 years default
);

CREATE TABLE DataRetentionConfig (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TenantId UNIQUEIDENTIFIER NOT NULL,
    DataType NVARCHAR(100) NOT NULL,
    RetentionDays INT NOT NULL,
    AutoDelete BIT NOT NULL DEFAULT 0,
    LastCleanupAt DATETIME2,
    UNIQUE(TenantId, DataType)
);
```

```csharp
// GDPR Services
public interface IGdprService
{
    Task<DataExportResult> ExportUserDataAsync(Guid userId, Guid tenantId);
    Task RequestAccountDeletionAsync(Guid userId, Guid tenantId, string reason);
    Task RecordConsentAsync(Guid userId, string consentType, bool granted, string ip, string userAgent);
    Task<bool> HasConsentAsync(Guid userId, string consentType);
}

// Data export — collects all user data across all tables
public async Task<DataExportResult> ExportUserDataAsync(Guid userId, Guid tenantId)
{
    var export = new DataExportResult
    {
        User = await _db.Users.FindAsync(userId),
        Memberships = await _db.UserTenantMemberships.Where(m => m.UserId == userId).ToListAsync(),
        Orders = await _db.Orders.Where(o => o.CustomerId == userId && o.TenantId == tenantId).ToListAsync(),
        Contacts = await _db.Contacts.Where(c => c.Id == userId && c.TenantId == tenantId).ToListAsync(),
        Enrollments = await _db.CourseEnrollments.Where(e => e.ContactId == userId && e.TenantId == tenantId).ToListAsync(),
        Subscriptions = await _db.Subscriptions.Where(s => s.CustomerId == userId && s.TenantId == tenantId).ToListAsync(),
        ConsentRecords = await _db.ConsentRecords.Where(c => c.UserId == userId).ToListAsync(),
        // ... all other user data
    };

    return export;
}

// Automatic data cleanup job
public class DataRetentionCleanupJob : IJob
{
    public async Task Execute(IJobCancellationToken cancellationToken)
    {
        var configs = await _db.DataRetentionConfig.Where(c => c.AutoDelete).ToListAsync();

        foreach (var config in configs)
        {
            var cutoff = DateTime.UtcNow.AddDays(-config.RetentionDays);

            // Delete old analytics events
            if (config.DataType == "analytics_events")
            {
                await _db.AnalyticsEvents
                    .Where(e => e.TenantId == config.TenantId && e.CreatedAt < cutoff)
                    .ExecuteDeleteAsync(cancellationToken.CancellationToken);
            }

            // Delete old audit logs
            if (config.DataType == "audit_logs")
            {
                await _db.AuditLogs
                    .Where(l => l.TenantId == config.TenantId && l.CreatedAt < cutoff)
                    .ExecuteDeleteAsync(cancellationToken.CancellationToken);
            }

            config.LastCleanupAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync(cancellationToken.CancellationToken);
    }
}
```

### 10.9 Horizontal Auto-Scaling

```yaml
# Azure App Service auto-scale rules
# scale-settings.yml
autoScale:
  default:
    minInstances: 2
    maxInstances: 20
    coolDownMinutes: 5
    rules:
      - metric: CpuPercentage
        operator: GreaterThan
        threshold: 70
        scale:
          instances: 2
          direction: Increase
      - metric: CpuPercentage
        operator: LessThan
        threshold: 30
        scale:
          instances: 1
          direction: Decrease
      - metric: HttpQueueLength
        operator: GreaterThan
        threshold: 50
        scale:
          instances: 3
          direction: Increase
      - metric: MemoryPercentage
        operator: GreaterThan
        threshold: 80
        scale:
          instances: 2
          direction: Increase

  # AI workers — separate scaling
  ai-workers:
    minInstances: 1
    maxInstances: 10
    coolDownMinutes: 10
    rules:
      - metric: QueueDepth
        operator: GreaterThan
        threshold: 100
        scale:
          instances: 2
          direction: Increase
```

### 10.10 Feature Flags

```csharp
// Infrastructure/FeatureFlags/FeatureFlagService.cs
public interface IFeatureFlagService
{
    Task<bool> IsEnabledAsync(string flagName, Guid? tenantId = null);
    Task<T> GetValueAsync<T>(string flagName, T defaultValue, Guid? tenantId = null);
}

public class FeatureFlagService : IFeatureFlagService
{
    private readonly IConfiguration _config;
    private readonly IDistributedCache _cache;

    // Flags stored in Azure App Configuration
    public async Task<bool> IsEnabledAsync(string flagName, Guid? tenantId = null)
    {
        // Check tenant-specific override first
        if (tenantId.HasValue)
        {
            var tenantFlag = await _cache.GetStringAsync($"ff:{tenantId}:{flagName}");
            if (tenantFlag != null) return bool.Parse(tenantFlag);
        }

        // Fall back to global flag
        return _config.GetValue<bool>($"FeatureFlags:{flagName}");
    }
}

// Usage in controllers
[ApiController]
[Route("api/v1/apps")]
public class AppsController : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateApp([FromBody] GenerateAppRequest request)
    {
        if (!await _featureFlags.IsEnabledAsync("AI.AppGeneration", GetTenantId()))
        {
            return BadRequest(new { error = "AI App Generation is not enabled for your plan" });
        }

        // ...
    }
}
```

### 10.11 Canary Deployments

```yaml
# Azure Deployment Slots
deploymentSlots:
  production:
    traffic: 90%
  canary:
    traffic: 10%
    healthCheck:
      path: /health
      interval: 30s
      failureThreshold: 3
    promotion:
      automaticAfter: 1h
      metric: errorRate
      threshold: 1%  # Promote only if error rate < 1%
    rollback:
      automaticOnErrorSpike: true
      threshold: 5%  # Auto-rollback if error rate > 5%
```

### 10.12 Disaster Recovery

```yaml
disasterRecovery:
  database:
    type: Azure SQL Active Geo-Replication
    primaryRegion: "East US"
    secondaryRegion: "West US 2"
    rpo: "5 seconds"  # Max data loss
    rto: "30 seconds"  # Max downtime
    failoverGroup: "creatoros-failover"

  blobStorage:
    type: GRS (Geo-Redundant Storage)
    primaryRegion: "East US"
    secondaryRegion: "West US 2"

  redis:
    type: Azure Cache for Redis (Premium with clustering)
    geoReplication: true

  backups:
    database:
      frequency: "Every 5 minutes"
      retention: "35 days"
      pointInTimeRestore: true
    blobStorage:
      softDelete: 14 days
      versioning: true

  runbook:
    title: "Disaster Recovery Runbook"
    steps:
      - name: "Detect failure"
        tool: "Azure Monitor alert"
      - name: "Verify secondary region health"
        tool: "Health check API"
      - name: "Trigger failover"
        tool: "Azure SQL failover group"
      - name: "Update DNS (Front Door)"
        tool: "Azure CLI / ARM template"
      - name: "Verify services"
        tool: "Synthetic monitoring"
      - name: "Notify team"
        tool: "PagerDuty + Slack"
```

---

## 11. Security Hardening

### 11.1 Security Headers Middleware

```csharp
public class SecurityHeadersMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        context.Response.Headers.Append("Permissions-Policy", "camera=(), microphone=(), geolocation=()");
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'");

        await _next(context);
    }
}
```

### 11.2 Input Validation & SQL Injection Prevention

```csharp
// FluentValidation for all inputs
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(200)
            .Matches(@"^[a-zA-Z0-9\s\-\.\,\!\?\&\'\"]+$")
            .WithMessage("Name contains invalid characters");

        RuleFor(x => x.PriceCents)
            .InclusiveBetween(0, 1_000_000_00) // Max $1M
            .WithMessage("Price must be between $0 and $1,000,000");

        RuleFor(x => x.Description)
            .MaximumLength(4000)
            .Must(desc => !desc.Contains("<script"))
            .WithMessage("HTML tags not allowed in description");
    }
}

// EF Core parameterized queries (prevents SQL injection by default)
// NEVER use raw SQL with string interpolation
var products = await _db.Products
    .Where(p => p.TenantId == tenantId && p.Name.Contains(searchTerm))
    .ToListAsync(); // Parameterized automatically

// When raw SQL is required:
var results = await _db.Database
    .SqlQueryRaw<ProductSearchResult>(
        "SELECT * FROM Products WHERE TenantId = @tenantId AND Name LIKE @search",
        new SqlParameter("@tenantId", tenantId),
        new SqlParameter("@search", $"%{searchTerm}%"))
    .ToListAsync();
```

### 11.3 Webhook Signature Verification

```csharp
// Stripe webhook verification
[HttpPost("webhook/stripe")]
[AllowAnonymous]
public async Task<IActionResult> HandleStripeWebhook()
{
    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    var stripeEvent = EventUtility.ConstructEvent(
        json,
        Request.Headers["Stripe-Signature"],
        _config["Stripe:WebhookSecret"],
        throwOnApiVersionMismatch: true);

    switch (stripeEvent.Type)
    {
        case Events.CheckoutSessionCompleted:
            var session = stripeEvent.Data.Object as Session;
            await _orderService.HandleCheckoutCompletedAsync(session);
            break;
        case Events.CustomerSubscriptionUpdated:
            // ...
            break;
        case Events.PaymentIntentFailed:
            // ...
            break;
    }

    return Ok();
}
```

---

## 12. Observability & Monitoring

### 12.1 Health Checks

```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "sql")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!, name: "redis")
    .AddAzureServiceBusTopic(builder.Configuration["ServiceBus:ConnectionString"]!, "events", name: "servicebus")
    .AddAzureBlobStorage(builder.Configuration["Azure:StorageConnectionString"]!, name: "blobstorage")
    .AddCheck<StripeHealthCheck>("stripe")
    .AddCheck<AIServiceHealthCheck>("ai");

// Health check endpoints
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = _ => true
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false // Always returns healthy for liveness
});
```

### 12.2 Alert Rules

```yaml
alerts:
  - name: "High Error Rate"
    condition: "sum(rate(http_requests_total{status=~'5..'}[5m])) / sum(rate(http_requests_total[5m])) > 0.05"
    severity: "critical"
    notify: ["pagerduty", "slack"]

  - name: "High Response Time (P99)"
    condition: "histogram_quantile(0.99, sum(rate(http_request_duration_seconds_bucket[5m])) by (le)) > 5"
    severity: "warning"
    notify: ["slack"]

  - name: "Database Connection Pool Exhaustion"
    condition: "sql_active_connections > 80"
    severity: "critical"
    notify: ["pagerduty", "slack"]

  - name: "AI Cost Spike"
    condition: "sum(rate(creator_ai_cost_cents[1h])) > 10000"
    severity: "warning"
    notify: ["slack"]

  - name: "Marketplace Payment Failure Rate"
    condition: "sum(rate(creator_orders_failed{source='marketplace'}[1h])) / sum(rate(creator_orders_total{source='marketplace'}[1h])) > 0.1"
    severity: "critical"
    notify: ["pagerduty", "slack"]

  - name: "Tenant Data Export Pending"
    condition: "count(creator_data_export_requests{status='pending'}) > 10"
    severity: "info"
    notify: ["slack"]

  - name: "Circuit Breaker Open"
    condition: "circuit_breaker_state{state='open'} > 0"
    severity: "warning"
    notify: ["slack"]
```

### 12.3 Centralized Logging

```csharp
// Structured logging throughout the application
public class OrderService : IOrderService
{
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        using var activity = _tracer.StartActivity("CreateOrder");
        activity?.SetTag("tenant.id", request.TenantId);
        activity?.SetTag("customer.id", request.CustomerId);
        activity?.SetTag("order.source", request.Source);

        _logger.LogInformation("Creating order for tenant {TenantId}, customer {CustomerId}, total {TotalCents} cents",
            request.TenantId, request.CustomerId, request.TotalCents);

        var order = new Order { /* ... */ };

        _logger.LogInformation("Order {OrderId} created successfully for tenant {TenantId}",
            order.Id, request.TenantId);

        BusinessMetrics.OrdersCreated.Add(1,
            new("tenant.id", request.TenantId.ToString()),
            new("source", request.Source),
            new("product.type", order.ProductType));

        return order;
    }
}
```

---

## 13. Load Testing Strategy

```yaml
loadTests:
  tool: "k6 (Grafana k6)"

  scenarios:
    marketplace_browse:
      vus: 500
      duration: "10m"
      script: |
        import http from 'k6/http';
        import { check } from 'k6';

        export const options = {
          thresholds: {
            http_req_duration: ['p(95)<500'],
            http_req_failed: ['rate<0.01'],
          },
        };

        export default function () {
          const res = http.get('https://api.staging.creatoros.com/api/v1/marketplace/products');
          check(res, { 'status is 200': (r) => r.status === 200 });
        }

    creator_dashboard:
      vus: 100
      duration: "5m"
      script: |
        # Authenticated requests — load creator dashboard + analytics

    checkout_flow:
      vus: 50
      duration: "5m"
      script: |
        # Full purchase flow: browse → add to cart → checkout → payment

    ai_generation:
      vus: 20
      duration: "10m"
      script: |
        # AI app generation — most expensive operation

    concurrent_editing:
      vus: 30
      duration: "5m"
      script: |
        # Multiple users editing same workspace simultaneously

  passCriteria:
    maxErrorRate: "1%"
    p95ResponseTime: "500ms"
    p99ResponseTime: "2000ms"
    throughput: "1000 req/s minimum"
```

---

## 14. Updated Implementation Phases

### Phase 1 — Foundation (Weeks 1-6)
- [ ] Solution structure, shared kernel, multi-tenancy
- [ ] Identity & Auth (JWT, roles, tenant membership)
- [ ] Database schema (core tables, migrations)
- [ ] React app shell + auth flow
- [ ] Flutter app shell + auth flow
- [ ] CI/CD pipeline
- [ ] Azure infrastructure setup
- [ ] **OpenTelemetry + structured logging**
- [ ] **Health check endpoints**
- [ ] **Azure Key Vault for secrets**
- [ ] **Security headers middleware**

### Phase 2 — Creation Tools (Weeks 7-12)
- [ ] Website builder (blocks, drag-drop, themes)
- [ ] No-code page editor
- [ ] AI app generation (prompt → working app)
- [ ] App versioning, preview, publish
- [ ] Conversational editing flow
- [ ] Custom domain + SSL
- [ ] **Circuit breakers for AI calls**
- [ ] **Idempotency for app generation**
- [ ] **Feature flags for AI features**

### Phase 3 — Commerce (Weeks 13-18)
- [ ] Product catalog (digital, courses, memberships, coaching)
- [ ] Stripe Connect integration
- [ ] Checkout flow
- [ ] Subscription management
- [ ] Coupons, bundles, upsells
- [ ] Creator CRM
- [ ] **Outbox pattern for order events**
- [ ] **Idempotency for payments**
- [ ] **Webhook signature verification**
- [ ] **Rate limiting on checkout**

### Phase 4 — Marketplace (Weeks 19-22)
- [ ] Marketplace listing & approval
- [ ] Search, categories, filters
- [ ] Reviews & ratings
- [ ] Featured/trending rankings
- [ ] Attribution tracking
- [ ] Remix/template marketplace
- [ ] **Marketplace-specific rate limiting**
- [ ] **Moderation pipeline with audit logs**

### Phase 5 — AI & Growth (Weeks 23-28)
- [ ] AI agent system (8 agents)
- [ ] AI model routing & cost controls
- [ ] Email campaigns & sequences
- [ ] Analytics dashboard
- [ ] Affiliate system
- [ ] Audience-to-business intelligence
- [ ] **AI usage metering + budget caps**
- [ ] **AI cost monitoring + alerts**

### Phase 6 — Compliance & Security (Weeks 29-32)
- [ ] GDPR data export/deletion
- [ ] Consent management
- [ ] Data retention automation
- [ ] Security audit + penetration testing
- [ ] Load testing (prove 1000+ concurrent users)
- [ ] DR failover testing
- [ ] SOC2 readiness assessment

### Phase 7 — Polish & Scale (Weeks 33-36)
- [ ] Performance optimization
- [ ] Mobile app polish
- [ ] PWA support
- [ ] iOS/Android packaging workflow
- [ ] Admin dashboard
- [ ] Canary deployment setup
- [ ] Auto-scaling rules
- [ ] Launch prep
