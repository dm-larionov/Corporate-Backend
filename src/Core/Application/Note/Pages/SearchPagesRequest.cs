using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class SearchPagesRequest : BaseFilter, IRequest<ICollection<PageDto>>
{
    public Guid TaxonomyId { get; set; }
    public Guid? UserId { get; set; }
}

public class SearchPagesRequestHandler : IRequestHandler<SearchPagesRequest, ICollection<PageDto>>
{
    private readonly IReadRepository<Page> _repository;

    public SearchPagesRequestHandler(IReadRepository<Page> repository) => _repository = repository;

    public async Task<ICollection<PageDto>> Handle(SearchPagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new PagesByTaxonomySpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}