-- Test Data for CreatorOS
-- Run this in SSMS or sqlcmd

-- 1. Create a test user (password: Test1234!)
DECLARE @UserId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Users (Id, Email, DisplayName, PasswordHash, EmailConfirmed, CreatedAt)
VALUES (@UserId, 'demo@creatoros.com', 'Demo User', '$2a$11$placeholder', 1, GETUTCDATE());

-- 2. Create a tenant/workspace
DECLARE @TenantId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Tenants (Id, Name, Slug, [Plan], Status, CreatedAt)
VALUES (@TenantId, 'My Creative Studio', 'my-creative-studio', 'pro', 'active', GETUTCDATE());

-- 3. Link user to tenant as owner
INSERT INTO UserTenantMemberships (Id, TenantId, UserId, Role, Status, CreatedAt)
VALUES (NEWID(), @TenantId, @UserId, 'owner', 'active', GETUTCDATE());

-- 4. Create products
DECLARE @Product1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Product2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Product3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Products (Id, TenantId, Name, Description, ProductType, PriceCents, Currency, IsActive, IsPublished, CreatedAt)
VALUES
(@Product1, @TenantId, 'Social Media Templates Pack', '50+ ready-to-use Instagram, TikTok, and Twitter templates', 'digital', 2999, 'USD', 1, 1, GETUTCDATE()),
(@Product2, @TenantId, 'Creator Course: Grow to 10K', 'Step-by-step course to grow your following to 10,000', 'course', 4999, 'USD', 1, 1, GETUTCDATE()),
(@Product3, @TenantId, '1-on-1 Coaching Session', '60-minute strategy call with a growth expert', 'coaching', 9999, 'USD', 1, 1, GETUTCDATE());

-- 5. Create orders
DECLARE @Order1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Order2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Orders (Id, TenantId, Status, TotalCents, Currency, Source, CreatedAt, CompletedAt)
VALUES
(@Order1, @TenantId, 'completed', 2999, 'USD', 'storefront', GETUTCDATE(), GETUTCDATE()),
(@Order2, @TenantId, 'completed', 4999, 'USD', 'storefront', DATEADD(DAY, -2, GETUTCDATE()), DATEADD(DAY, -2, GETUTCDATE()));

INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPriceCents, TotalCents, CreatedAt)
VALUES
(NEWID(), @Order1, @Product1, 1, 2999, 2999, GETUTCDATE()),
(NEWID(), @Order2, @Product2, 1, 4999, 4999, DATEADD(DAY, -2, GETUTCDATE()));

-- 6. Create more orders for analytics
DECLARE @i INT = 0;
WHILE @i < 20
BEGIN
    DECLARE @OrderId UNIQUEIDENTIFIER = NEWID();
    DECLARE @DaysAgo INT = ABS(CHECKSUM(NEWID()) % 30);
    DECLARE @Amount BIGINT = (ABS(CHECKSUM(NEWID()) % 50000) + 1000);

    INSERT INTO Orders (Id, TenantId, Status, TotalCents, Currency, Source, CreatedAt, CompletedAt)
    VALUES (@OrderId, @TenantId, 'completed', @Amount, 'USD', 'storefront', DATEADD(DAY, -@DaysAgo, GETUTCDATE()), DATEADD(DAY, -@DaysAgo, GETUTCDATE()));

    INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPriceCents, TotalCents, CreatedAt)
    VALUES (NEWID(), @OrderId, @Product1, 1, @Amount, @Amount, DATEADD(DAY, -@DaysAgo, GETUTCDATE()));

    SET @i = @i + 1;
END;

PRINT 'Test data inserted successfully!';
PRINT 'Login with: demo@creatoros.com / Test1234!';
