namespace WebApi.Domain.Note;

public class FieldType : Enumeration
{
    public static FieldType Text = new(1, nameof(Text));
    public static FieldType Name = new(2, nameof(Name));
    public static FieldType Description = new(3, nameof(Description));
    public static FieldType URL = new(4, nameof(URL));

    public FieldType(int id, string name)
        : base(id, name)
    {
    }

    public static FieldType GetById(int id)
    {
        if (Text.Id == id)
            return Text;
        if (Name.Id == id)
            return Name;
        if (Description.Id == id)
            return Description;
        if (URL.Id == id)
            return URL;

        throw new ArgumentException("Unknown Field Type!");
    }
}