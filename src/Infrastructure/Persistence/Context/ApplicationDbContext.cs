using WebApi.Application.Common.Events;
using WebApi.Application.Common.Interfaces;
using WebApi.Domain.Catalog;
using WebApi.Domain.Note;
using WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();

    public DbSet<FieldType> FieldTypes => Set<FieldType>();
    public DbSet<BlockType> BlockTypes => Set<BlockType>();
    public DbSet<Field> Fields => Set<Field>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<Taxonomy> Taxonomies => Set<Taxonomy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FieldType>(entity =>
        {
            entity.Property(b => b.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BlockType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Taxonomy>(entity =>
        {
            entity.HasOne(t => t.ParentTaxonomy)
            .WithMany()
            .HasForeignKey(d => d.ParentTaxonomyId);
        });

        modelBuilder.HasDefaultSchema(SchemaNames.Note);
    }
}