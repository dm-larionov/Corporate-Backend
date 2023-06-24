namespace WebApi.Application.Note.Blocks;

public class BlockDto : IDto
{
    public Guid Id { get; set; }
    public short Index { get; set; }
    public Guid PageId { get; set; }
    public int BlockTypeId { get; set; }
    public string BlockTypeName { get; set; } = default!;
    public ICollection<FieldDto> Fields { get; set; } = default!;
}