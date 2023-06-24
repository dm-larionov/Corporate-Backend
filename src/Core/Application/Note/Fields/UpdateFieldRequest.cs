using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class UpdateFieldRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Value { get; set; } = default!;
}

public class UpdateFieldRequestHandler : IRequestHandler<UpdateFieldRequest, Guid>
{
    private readonly IRepository<Field> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateFieldRequestHandler(IRepository<Field> repository, IStringLocalizer<UpdateFieldRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateFieldRequest request, CancellationToken cancellationToken)
    {
        var field = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = field ?? throw new NotFoundException(_t["Field {0} Not Found.", request.Id]);

        field.ChangeValue(request.Value);

        // Add Domain Events to be raised after the commit
        field.DomainEvents.Add(EntityUpdatedEvent.WithEntity(field));

        await _repository.UpdateAsync(field, cancellationToken);

        return request.Id;
    }
}