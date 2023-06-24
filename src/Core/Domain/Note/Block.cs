using WebApi.Domain.Catalog;
using WebApi.Domain.Common.Events;

namespace WebApi.Domain.Note;

public class Block : AuditableEntity, IAggregateRoot
{
    public short Index { get; private set; }
    public int BlockTypeId { get; private set; }
    public BlockType BlockType { get; private set; }
    public Guid PageId { get; private set; }
    public virtual ICollection<Field> Fields { get; }

    public Block(short index, BlockType blockType)
    {
        UpdateIndex(index);
        BlockTypeId = blockType.Id;
        Fields = new List<Field>();
    }

    public static Block CreateBlock(Guid id, short index, BlockType blockType, Guid pageId)
    {
        var block = new Block(index, blockType);
        block.Id = id;
        block.PageId = pageId;
        return block;
    }

    /// <summary>
    /// EF constructor
    /// </summary>
    private Block()
    {
    }

    public Block UpdateIndex(short index)
    {
        if (index < 0)
            throw new ArgumentException("Unknown Block Type name!");
        Index = index;
        return this;
    }

    public void AddField(Field field)
    {
        if (field == null)
            throw new ArgumentNullException("Field cannot be null!");

        var avalableBlockFields = BlockType.GetAvailableFieldTypesById(BlockTypeId);

        // Можно ли добавить данную ячейку в данный блок
        if (avalableBlockFields.FirstOrDefault(x => x.Id == field.FieldTypeId) is null)
            throw new ArgumentNullException("Field cannot be added to this block!");

        // Есть ли уже в данном блоке это поле
        if (Fields.FirstOrDefault(x => x.FieldTypeId == field.FieldTypeId) is not null)
            throw new ArgumentNullException("Current field already exists in this block!");

        Fields.Add(field);
        DomainEvents.Add(EntityCreatedEvent.WithEntity(field));
    }
}