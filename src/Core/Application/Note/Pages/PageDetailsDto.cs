using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class PageDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsDraft { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public ICollection<BlockDto> Blocks { get; set; } = default!;
}