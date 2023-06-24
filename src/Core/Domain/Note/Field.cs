namespace WebApi.Domain.Note;

public class Field : AuditableEntity, IAggregateRoot
{
    public string Value { get; private set; } = default!;
    public int FieldTypeId { get; private set; }
    public FieldType FieldType { get; private set; }
    public Guid BlockId { get; private set; }

    public Field(string value, FieldType fieldType)
    {
        ChangeValue(value);
        FieldTypeId = fieldType.Id;
    }

    public static Field CreateField(Guid id, string value, FieldType fieldType, Guid BlockId)
    {
        var field = new Field(value, fieldType);
        field.Id = id;
        field.BlockId = BlockId;
        return field;
    }

    /// <summary>
    /// EF constructor
    /// </summary>
    private Field()
    {
    }

    public void ChangeValue(string value)
    {
        if (value is not null && Value?.Equals(value) is not true) Value = value;
    }
}