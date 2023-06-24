using WebApi.Domain.Catalog;
using WebApi.Domain.Common.Events;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WebApi.Domain.Note;

public class Page : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsDraft { get; private set; }
    //public new Guid CreatedBy { get; private set; }
    public List<Block> Blocks { get; }
    public List<Taxonomy> Taxonomies { get; }

    public Page(string title, string? description, bool isDraft)
    {
        Title = title;
        Description = description;
        IsDraft = isDraft;
        Blocks = new List<Block>();
        Taxonomies = new List<Taxonomy>();
    }

    public static Page CreatePage(Guid id, string title, string? description)
    {
        var result = new Page(title, description, true);
        result.Id = id;
        return result;
    }

    public void ChangeTitle(string title)
    {
        //if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("Title is empty");
        Title = title;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    private void RefreshBlockIndexes()
    {
        for (short i = 0; i < Blocks.Count; i++)
        {
            Blocks[i].UpdateIndex(i);
            DomainEvents.Add(EntityUpdatedEvent.WithEntity(Blocks[i]));
        }
    }

    public void AddBlock(Block block)
    {
        Blocks.Add(block);
        DomainEvents.Add(EntityCreatedEvent.WithEntity(block));
        //RefreshBlockIndexes();
    }

    public void RemoveBlock(short index)
    {
        if (index < 0 || index >= Blocks.Count) throw new ArgumentException("Incorrect index value");
        Blocks.RemoveAt(index);
        RefreshBlockIndexes();
    }

    //public void MoveBlock(int oldIndex, int newIndex)
    //{
    //    Blocks.Move(oldIndex, newIndex);
    //    RefreshBlockIndexes();
    //}

    public void AddTaxonomy(Taxonomy taxonomy)
    {
        Taxonomies.Add(taxonomy);
    }

    public void RemoveTaxonomy(int index)
    {
        if (index < 0 || index >= Blocks.Count) throw new ArgumentException("Incorrect index value");
        Blocks.RemoveAt(index);
    }
}