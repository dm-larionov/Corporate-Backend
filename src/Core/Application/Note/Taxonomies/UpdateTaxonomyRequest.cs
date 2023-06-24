using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class UpdateTaxonomyRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdatePageRequestHandler : IRequestHandler<UpdateTaxonomyRequest, Guid>
{
    private readonly IRepository<Page> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdatePageRequestHandler(IRepository<Page> repository, IStringLocalizer<UpdatePageRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateTaxonomyRequest request, CancellationToken cancellationToken)
    {
        var page = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = page ?? throw new NotFoundException(_t["Page {0} Not Found.", request.Id]);

        page.ChangeTitle(request.Title);
        page.ChangeDescription(request.Description);

        // Add Domain Events to be raised after the commit
        page.DomainEvents.Add(EntityUpdatedEvent.WithEntity(page));

        await _repository.UpdateAsync(page, cancellationToken);

        return request.Id;
    }
}