using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class GetTaxonomyRequest : IRequest<TaxonomyDetailsDto>
{
    public Guid Id { get; set; }

    public GetTaxonomyRequest(Guid id) => Id = id;
}

public class GetTaxonomyRequestHandler : IRequestHandler<GetTaxonomyRequest, TaxonomyDetailsDto>
{
    private readonly IRepository<Taxonomy> _repository;
    private readonly IStringLocalizer _t;

    public GetTaxonomyRequestHandler(IRepository<Taxonomy> repository, IStringLocalizer<GetTaxonomyRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<TaxonomyDetailsDto> Handle(GetTaxonomyRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Taxonomy, TaxonomyDetailsDto>)new TaxonomyByIdFullDetailsSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Taxonomy {0} Not Found.", request.Id]);
}