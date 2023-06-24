using WebApi.Domain.Note;
using WebApi.Infrastructure.Persistence.Context;
using WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace WebApi.Infrastructure.Note;

public class BlockTypeSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BlockTypeSeeder> _logger;

    public BlockTypeSeeder(ILogger<BlockTypeSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.BlockTypes.Any())
        {
            _logger.LogInformation("Started to Seed Block Types.");

            var blockTypes = new[] { BlockType.Head1Text, BlockType.Head2Text, BlockType.Text, BlockType.Image, BlockType.Link, BlockType.File, };

            if (blockTypes != null)
            {
                foreach (var blockType in blockTypes)
                {
                    await _db.BlockTypes.AddAsync(blockType, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Block Types.");
        }
    }
}