using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class CreateFieldRequest : IRequest<Guid>
{
    public Guid BlockId { get; set; }
    public string Value { get; set; } = default!;
    public int FieldTypeId { get; set; }
}

public class CreateFieldRequestHandler : IRequestHandler<CreateFieldRequest, Guid>
{
    private readonly IRepository<Field> _repository;
    private readonly IRepository<Taxonomy> _taxonomyRepository;

    public CreateFieldRequestHandler(IRepository<Field> repository, IRepository<Taxonomy> taxonomyRepository)
    {
        _repository = repository;
        _taxonomyRepository = taxonomyRepository;
    }

    public async Task<Guid> Handle(CreateFieldRequest request, CancellationToken cancellationToken)
    {
        var block = Field.CreateField(Guid.NewGuid(), request.Value, FieldType.GetById(request.FieldTypeId), request.BlockId);

        // Add Domain Events to be raised after the commit
        block.DomainEvents.Add(EntityCreatedEvent.WithEntity(block));

        await _repository.AddAsync(block, cancellationToken);

        return block.Id;
    }
}