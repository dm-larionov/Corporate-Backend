using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class TaxonomyDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentTaxonomyId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}