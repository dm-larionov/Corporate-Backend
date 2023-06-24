using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class TaxonomyDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid CreatedBy { get; set; }
}