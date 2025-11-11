-- 1️⃣ Ensure Admin role exists
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE NormalizedName = 'ADMIN')
BEGIN
    INSERT INTO AspNetRoles (Id, [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID());
END;

-- 2️⃣ Insert the user
INSERT INTO AspNetUsers (
    Id,
    UserName,
    NormalizedUserName,
    Email,
    NormalizedEmail,
    EmailConfirmed,
    PasswordHash,
    SecurityStamp,
    ConcurrencyStamp,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
    LockoutEnabled,
    AccessFailedCount,
    JoinDate,
    IsFromUAE,
    HasControlSystemAccess,
    IsDeleted,
    SalaryValue,
    SalaryType,
    CommissionRate,
    StaffVisaCount,
    HotelId,
    EmployeeAddedId,
    LocationId
)
VALUES (
    'a2323ccb-48f8-417c-9b3d-f5901d9de354', -- user id
    'adminuser',                            -- username
    'ADMINUSER',
    'admin@example.com',
    'ADMIN@EXAMPLE.COM',
    1,                                      -- EmailConfirmed = true
    'AQAAAAIAAYagAAAAEK/pvRj7wQb4p7mx6zA6PUbFAZb7h1rZcMJrTRXvm5e4h7ka3FzX3vJzlw9h2HT6Sw==', -- hashed 'Admin@123'
    NEWID(),
    NEWID(),
    0, 0, 0, 0,                             -- phone/twofactor/lockout defaults
    GETDATE(),                              -- JoinDate
    0, 0, 0,                                -- IsFromUAE, HasControlSystemAccess, IsDeleted
    0, 0, 0, 0,                             -- Salary/Commission etc.
    NULL, NULL, NULL                        -- Foreign keys
);

-- 3️⃣ Link user to Admin role
DECLARE @RoleId NVARCHAR(450);
SELECT @RoleId = Id FROM AspNetRoles WHERE NormalizedName = 'ADMIN';

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('a2323ccb-48f8-417c-9b3d-f5901d9de354', @RoleId);