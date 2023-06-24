using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class CreatePageRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? TaxonomyId { get; set; }
}

public class CreatePageRequestHandler : IRequestHandler<CreatePageRequest, Guid>
{
    private readonly IRepository<Page> _repository;
    private readonly IRepository<Taxonomy> _taxonomyRepository;

    public CreatePageRequestHandler(IRepository<Page> repository, IRepository<Taxonomy> taxonomyRepository)
    {
        _repository = repository;
        _taxonomyRepository = taxonomyRepository;
    }

    public async Task<Guid> Handle(CreatePageRequest request, CancellationToken cancellationToken)
    {
        var page = new Page(request.Title, request.Description, true);

        if (request.TaxonomyId != null)
        {
            var taxonomy = await _taxonomyRepository.GetByIdAsync(request.TaxonomyId, cancellationToken);
            page.AddTaxonomy(taxonomy);
        }

        // Add Domain Events to be raised after the commit
        page.DomainEvents.Add(EntityCreatedEvent.WithEntity(page));

        await _repository.AddAsync(page, cancellationToken);

        return page.Id;
    }
}