using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class SearchTaxonomiesRequest : BaseFilter, IRequest<ICollection<TaxonomyDto>>
{
    public Guid? TaxonomyId { get; set; }
}

public class SearchTaxonomysRequestHandler : IRequestHandler<SearchTaxonomiesRequest, ICollection<TaxonomyDto>>
{
    private readonly IReadRepository<Taxonomy> _repository;

    public SearchTaxonomysRequestHandler(IReadRepository<Taxonomy> repository) => _repository = repository;

    public async Task<ICollection<TaxonomyDto>> Handle(SearchTaxonomiesRequest request, CancellationToken cancellationToken)
    {
        var spec = new TaxonomiesSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}