using System.Reflection;
using DocumentFormat.OpenXml.Math;
using WebApi.Application.Common.Interfaces;
using WebApi.Domain.Catalog;
using WebApi.Domain.Note;
using WebApi.Infrastructure.Persistence.Context;
using WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApi.Infrastructure.Note;

public class TaxonomySeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<TaxonomySeeder> _logger;

    public TaxonomySeeder(ISerializerService serializerService, ILogger<TaxonomySeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Taxonomies.Any() && false)
        {
            _logger.LogInformation("Started to Seed Taxonomies.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string brandData = await File.ReadAllTextAsync(path + "/Note/taxonomies.json", cancellationToken);
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            //var brands = _serializerService.Deserialize<List<Taxonomy>>(brandData);
            var brands = JsonConvert.DeserializeObject<List<Taxonomy>>(brandData, settings);

            if (brands != null)
            {
                foreach (var brand in brands)
                {
                    Taxonomy? parent = _db.Taxonomies.FirstOrDefault(x => x.Id == brand.ParentTaxonomyId);
                    Taxonomy taxonomy = new Taxonomy(brand.Title, brand.Description, parent);
                    await _db.Taxonomies.AddAsync(taxonomy, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Taxonomies.");
        }
    }
}