using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class SearchBlocksRequest : BaseFilter, IRequest<ICollection<BlockDto>>
{
    public Guid PageId { get; set; }
}

public class SearchBlocksRequestHandler : IRequestHandler<SearchBlocksRequest, ICollection<BlockDto>>
{
    private readonly IReadRepository<Block> _repository;

    public SearchBlocksRequestHandler(IReadRepository<Block> repository) => _repository = repository;

    public async Task<ICollection<BlockDto>> Handle(SearchBlocksRequest request, CancellationToken cancellationToken)
    {
        var spec = new BlocksByPageSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}