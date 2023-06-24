using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class PageDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsDraft { get; set; }
    public Guid CreatedBy { get; set; }
}