using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class FieldsByPageSpec : EntitiesByBaseFilterSpec<Field, FieldDto>
{
    public FieldsByPageSpec(SearchFieldsRequest request)
        : base(request) =>
        Query
            .Where(p => p.BlockId == request.BlockId);
}