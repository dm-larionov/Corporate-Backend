using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class DeleteTaxonomyRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteTaxonomyRequest(Guid id) => Id = id;
}

public class DeleteTaxonomyRequestHandler : IRequestHandler<DeleteTaxonomyRequest, Guid>
{
    private readonly IRepository<Taxonomy> _repository;
    private readonly IStringLocalizer _t;

    public DeleteTaxonomyRequestHandler(IRepository<Taxonomy> repository, IStringLocalizer<DeleteTaxonomyRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteTaxonomyRequest request, CancellationToken cancellationToken)
    {
        var taxonomy = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = taxonomy ?? throw new NotFoundException(_t["Taxonomy {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        taxonomy.DomainEvents.Add(EntityDeletedEvent.WithEntity(taxonomy));

        await _repository.DeleteAsync(taxonomy, cancellationToken);

        return request.Id;
    }
}