using System.Collections.ObjectModel;
namespace WebApi.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);

    public const string Pages = nameof(Pages);
    public const string Taxonomies = nameof(Taxonomies);
    public const string Blocks = nameof(Blocks);
    public const string Fields = nameof(Fields);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),
        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),
        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true),

        new("View Pages", FSHAction.View, FSHResource.Pages, IsBasic: true),
        new("Search Pages", FSHAction.Search, FSHResource.Pages, IsBasic: true),
        new("Create Pages", FSHAction.Create, FSHResource.Pages),
        new("Update Pages", FSHAction.Update, FSHResource.Pages),
        new("Delete Pages", FSHAction.Delete, FSHResource.Pages),
        new("Export Pages", FSHAction.Export, FSHResource.Pages),

        new("View Taxonomies", FSHAction.View, FSHResource.Taxonomies, IsBasic: true),
        new("Search Taxonomies", FSHAction.Search, FSHResource.Taxonomies, IsBasic: true),
        new("Create Taxonomies", FSHAction.Create, FSHResource.Taxonomies),
        new("Update Taxonomies", FSHAction.Update, FSHResource.Taxonomies),
        new("Delete Taxonomies", FSHAction.Delete, FSHResource.Taxonomies),
        new("Export Taxonomies", FSHAction.Export, FSHResource.Taxonomies),

        new("View Blocks", FSHAction.View, FSHResource.Blocks, IsBasic: true),
        new("Search Blocks", FSHAction.Search, FSHResource.Blocks, IsBasic: true),
        new("Create Blocks", FSHAction.Create, FSHResource.Blocks),
        new("Update Blocks", FSHAction.Update, FSHResource.Blocks),
        new("Delete Blocks", FSHAction.Delete, FSHResource.Blocks),
        new("Export Blocks", FSHAction.Export, FSHResource.Blocks),

        new("View Fields", FSHAction.View, FSHResource.Fields, IsBasic: true),
        new("Search Fields", FSHAction.Search, FSHResource.Fields, IsBasic: true),
        new("Create Fields", FSHAction.Create, FSHResource.Fields),
        new("Update Fields", FSHAction.Update, FSHResource.Fields),
        new("Delete Fields", FSHAction.Delete, FSHResource.Fields),
        new("Export Fields", FSHAction.Export, FSHResource.Fields),

    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
