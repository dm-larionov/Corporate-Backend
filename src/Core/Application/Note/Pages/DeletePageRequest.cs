using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class DeletePageRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeletePageRequest(Guid id) => Id = id;
}

public class DeletePageRequestHandler : IRequestHandler<DeletePageRequest, Guid>
{
    private readonly IRepository<Page> _repository;
    private readonly IStringLocalizer _t;

    public DeletePageRequestHandler(IRepository<Page> repository, IStringLocalizer<DeletePageRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeletePageRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Page {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));

        await _repository.DeleteAsync(product, cancellationToken);

        return request.Id;
    }
}