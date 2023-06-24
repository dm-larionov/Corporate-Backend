using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class BlockField
{
    public string Value { get; set; }
    public int FieldTypeId { get; set; }
}

public class CreateBlockRequest : IRequest<Guid>
{
    public Guid PageId { get; set; }
    public short Index { get; set; }
    public int BlockTypeId { get; set; }
    public ICollection<BlockField>? Fields { get; set; }
}

public class CreateBlockRequestHandler : IRequestHandler<CreateBlockRequest, Guid>
{
    private readonly IRepository<Block> _repository;
    private readonly IRepository<Taxonomy> _taxonomyRepository;

    public CreateBlockRequestHandler(IRepository<Block> repository, IRepository<Taxonomy> taxonomyRepository)
    {
        _repository = repository;
        _taxonomyRepository = taxonomyRepository;
    }

    public async Task<Guid> Handle(CreateBlockRequest request, CancellationToken cancellationToken)
    {
        var block = Block.CreateBlock(Guid.NewGuid(), request.Index, BlockType.GetById(request.BlockTypeId), request.PageId);

        if (request.Fields != null && request.Fields.Count > 0)
        {
            foreach (var requestField in request.Fields)
            {
                var field = Field.CreateField(Guid.NewGuid(), requestField.Value, FieldType.GetById(requestField.FieldTypeId), block.Id);
                block.AddField(field);
            }
        }

        // Add Domain Events to be raised after the commit
        block.DomainEvents.Add(EntityCreatedEvent.WithEntity(block));

        await _repository.AddAsync(block, cancellationToken);

        return block.Id;
    }
}