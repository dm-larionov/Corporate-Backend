using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class DeleteBlockRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBlockRequest(Guid id) => Id = id;
}

public class DeleteBlockRequestHandler : IRequestHandler<DeleteBlockRequest, Guid>
{
    private readonly IRepository<Block> _repository;
    private readonly IStringLocalizer _t;

    public DeleteBlockRequestHandler(IRepository<Block> repository, IStringLocalizer<DeleteBlockRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteBlockRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Block {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));

        await _repository.DeleteAsync(product, cancellationToken);

        return request.Id;
    }
}