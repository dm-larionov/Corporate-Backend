using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class DeleteFieldRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteFieldRequest(Guid id) => Id = id;
}

public class DeleteFieldRequestHandler : IRequestHandler<DeleteFieldRequest, Guid>
{
    private readonly IRepository<Field> _repository;
    private readonly IStringLocalizer _t;

    public DeleteFieldRequestHandler(IRepository<Field> repository, IStringLocalizer<DeleteFieldRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteFieldRequest request, CancellationToken cancellationToken)
    {
        var field = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = field ?? throw new NotFoundException(_t["Field {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        field.DomainEvents.Add(EntityDeletedEvent.WithEntity(field));

        await _repository.DeleteAsync(field, cancellationToken);

        return request.Id;
    }
}