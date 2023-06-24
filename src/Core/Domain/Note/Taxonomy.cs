using WebApi.Domain.Catalog;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WebApi.Domain.Note;

public class Taxonomy : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid? ParentTaxonomyId { get; private set; }
    public Taxonomy? ParentTaxonomy { get; }
    public ObservableCollection<Page> Pages { get; private set; }

    /// <summary>
    /// ef-constructor
    /// </summary>
    private Taxonomy() { }

    public Taxonomy(string title, string? description, Taxonomy? parentTaxonomy)
    {
        Title = title;
        Description = description;
        ParentTaxonomy = parentTaxonomy;
    }

    public static Taxonomy CreatePage(Guid id, string title, string? description, Taxonomy? parentTaxonomy)
    {
        var result = new Taxonomy(title, description, parentTaxonomy);
        result.Id = id;
        return result;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("Title is empty");
        Title = title;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }
}