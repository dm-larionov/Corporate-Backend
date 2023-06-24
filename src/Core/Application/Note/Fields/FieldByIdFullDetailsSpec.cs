using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class FieldByIdFullDetailsSpec : Specification<Field, FieldDto>, ISingleResultSpecification
{
    public FieldByIdFullDetailsSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.FieldType);
}