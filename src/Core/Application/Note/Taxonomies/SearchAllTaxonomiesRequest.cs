using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class SearchAllTaxonomiesRequest : BaseFilter, IRequest<ICollection<TaxonomyDto>>
{
}

public class SearchAllTaxonomiesRequestHandler : IRequestHandler<SearchAllTaxonomiesRequest, ICollection<TaxonomyDto>>
{
    private readonly IReadRepository<Taxonomy> _repository;

    public SearchAllTaxonomiesRequestHandler(IReadRepository<Taxonomy> repository) => _repository = repository;

    public async Task<ICollection<TaxonomyDto>> Handle(SearchAllTaxonomiesRequest request, CancellationToken cancellationToken)
    {
        var spec = new AllTaxonomiesSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}