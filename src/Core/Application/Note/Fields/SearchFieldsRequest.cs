using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class SearchFieldsRequest : BaseFilter, IRequest<ICollection<FieldDto>>
{
    public Guid BlockId { get; set; }
}

public class SearchFieldsRequestHandler : IRequestHandler<SearchFieldsRequest, ICollection<FieldDto>>
{
    private readonly IReadRepository<Field> _repository;

    public SearchFieldsRequestHandler(IReadRepository<Field> repository) => _repository = repository;

    public async Task<ICollection<FieldDto>> Handle(SearchFieldsRequest request, CancellationToken cancellationToken)
    {
        var spec = new FieldsByPageSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}