using WebApi.Domain.Note;
using WebApi.Infrastructure.Persistence.Context;
using WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace WebApi.Infrastructure.Note;

public class FieldTypeSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<FieldTypeSeeder> _logger;

    public FieldTypeSeeder(ILogger<FieldTypeSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.FieldTypes.Any())
        {
            _logger.LogInformation("Started to Seed Field Types.");

            var fieldTypes = new[] { FieldType.Text, FieldType.Name, FieldType.Description, FieldType.URL };

            if (fieldTypes != null)
            {
                foreach (var fieldType in fieldTypes)
                {
                    await _db.FieldTypes.AddAsync(fieldType, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Field Types.");
        }
    }
}