using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class CreateTaxonomyRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
}

public class CreateTaxonomyRequestHandler : IRequestHandler<CreateTaxonomyRequest, Guid>
{
    private readonly IRepository<Taxonomy> _repository;

    public CreateTaxonomyRequestHandler(IRepository<Taxonomy> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTaxonomyRequest request, CancellationToken cancellationToken)
    {
        Taxonomy? parent = null;
        if (request.ParentId != null)
        {
            parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        }

        var taxonomy = new Taxonomy(request.Title, request.Description, parent);

        // Add Domain Events to be raised after the commit
        taxonomy.DomainEvents.Add(EntityCreatedEvent.WithEntity(taxonomy));

        await _repository.AddAsync(taxonomy, cancellationToken);

        return taxonomy.Id;
    }
}