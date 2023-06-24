namespace WebApi.Application.Note.Pages;

public class FieldDto : IDto
{
    public Guid Id { get; set; }
    public string Value { get; set; } = default!;
    public Guid BlockId { get; set; }
    public int FieldTypeId { get; set; }
    public string FieldTypeName { get; set; } = default!;
}