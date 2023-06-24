namespace WebApi.Domain.Note;

public class BlockType : Enumeration
{
    public static BlockType Head1Text = new(1, nameof(Head1Text));
    public static BlockType Head2Text = new(2, nameof(Head2Text));
    public static BlockType Text = new(3, nameof(Text));
    public static BlockType Image = new(4, nameof(Image));
    public static BlockType Link = new(5, nameof(Link));
    public static BlockType File = new(6, nameof(File));

    public BlockType(int id, string name)
        : base(id, name)
    {
    }

    public static BlockType GetById(int id)
    {
        if (Head1Text.Id == id)
            return Head1Text;
        if (Head2Text.Id == id)
            return Head2Text;
        if (Text.Id == id)
            return Text;
        if (Image.Id == id)
            return Image;
        if (Link.Id == id)
            return Link;
        if (File.Id == id)
            return File;

        throw new ArgumentException("Unknown Block Type name!");
    }

    public static IReadOnlyList<FieldType> GetAvailableFieldTypesById(int id)
    {
        if (Head1Text.Id == id)
            return new[] { FieldType.Text };
        if (Head2Text.Id == id)
            return new[] { FieldType.Text };
        if (Text.Id == id)
            return new[] { FieldType.Text };
        if (Image.Id == id)
            return new[] { FieldType.Name, FieldType.Description, FieldType.URL };
        if (Link.Id == id)
            return new[] { FieldType.Name, FieldType.Description, FieldType.URL };
        if (File.Id == id)
            return new[] { FieldType.Description, FieldType.URL };

        throw new ArgumentException("Unknown Block Type!");
    }
}