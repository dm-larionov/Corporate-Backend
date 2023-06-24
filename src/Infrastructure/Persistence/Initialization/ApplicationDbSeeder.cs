using WebApi.Infrastructure.Identity;
using WebApi.Infrastructure.Persistence.Context;
using WebApi.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApi.Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext)
    {
        foreach (string roleName in FSHRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
            {
                // Create the role
                _logger.LogInformation("Seeding {role} Role", roleName);
                role = new ApplicationRole(roleName, $"{roleName} Role");
                await _roleManager.CreateAsync(role);
            }

            // Assign permissions
            if (roleName == FSHRoles.Basic)
            {
                await AssignPermissionsToRoleAsync(dbContext, FSHPermissions.Basic, role);
            }
            else if (roleName == FSHRoles.Admin)
            {
                await AssignPermissionsToRoleAsync(dbContext, FSHPermissions.Admin, role);
                await AssignPermissionsToRoleAsync(dbContext, FSHPermissions.Root, role);
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<FSHPermission> permissions, ApplicationRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(c => c.Type == FSHClaims.Permission && c.Value == permission.Name))
            {
                _logger.LogInformation("Seeding {role} Permission '{permission}'", role.Name, permission.Name);
                dbContext.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = FSHClaims.Permission,
                    ClaimValue = permission.Name,
                    CreatedBy = "ApplicationDbSeeder"
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {

        string adminUserName = $"garab1k".ToLowerInvariant();
        ApplicationUser adminUser = new ApplicationUser
        {
            FirstName = "Вадим",
            LastName = "Гараев",
            Email = "dfybi@mail.ru",
            UserName = adminUserName,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            NormalizedEmail = "dfybi@mail.ru".ToUpperInvariant(),
            NormalizedUserName = adminUserName.ToUpperInvariant(),
            IsActive = true
        };

        _logger.LogInformation("Seeding Default Admin User");
        var password = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = password.HashPassword(adminUser, "123Pa$$word!");
        await _userManager.CreateAsync(adminUser);

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(adminUser, FSHRoles.Admin))
        {
            _logger.LogInformation("Assigning Admin Role to Admin User");
            await _userManager.AddToRoleAsync(adminUser, FSHRoles.Admin);
        }
    }
}