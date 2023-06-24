using WebApi.Domain.Common.Events;
using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class Field
{
    public string Value { get; set; } = default!;
    public int FieldTypeId { get; set; }
}

public class CreatePageBlockRequest : IRequest<PageDetailsDto>
{
    public Guid PageId { get; set; }
    public short Index { get; set; }
    public int BlockTypeId { get; set; }
    public ICollection<Field> Fields { get; set; } = default!;
}

public class CreatePageBlockRequestHandler : IRequestHandler<CreatePageBlockRequest, PageDetailsDto>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Block> _blockRepository;
    private readonly IRepository<Domain.Note.Field> _fieldRepository;

    public CreatePageBlockRequestHandler(IRepository<Page> pageRepository, IRepository<Block> blockRepository, IRepository<Domain.Note.Field> fieldRepository)
    {
        _pageRepository = pageRepository;
        _blockRepository = blockRepository;
        _fieldRepository = fieldRepository;
    }

    public async Task<PageDetailsDto> Handle(CreatePageBlockRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId, cancellationToken);

        var block = Block.CreateBlock(Guid.NewGuid(), request.Index, BlockType.GetById(request.BlockTypeId), page.Id);
        foreach (var requestField in request.Fields)
        {
            var field = Domain.Note.Field.CreateField(Guid.NewGuid(), requestField.Value, FieldType.GetById(requestField.FieldTypeId), block.Id);
            block.AddField(field);
        }
        //page.ChangeDescription("");
        page.AddBlock(block);

        // Add Domain Events to be raised after the commit
        //page.DomainEvents.Add(EntityUpdatedEvent.WithEntity(page));

        //await _repository.SaveChangesAsync();

        //await _pageRepository.UpdateAsync(page, cancellationToken);
        await _blockRepository.AddAsync(block, cancellationToken);

        //return null;
        return await _pageRepository.GetBySpecAsync((ISpecification<Page, PageDetailsDto>)new PageByIdFullDetailsSpec(request.PageId), cancellationToken);
    }
}