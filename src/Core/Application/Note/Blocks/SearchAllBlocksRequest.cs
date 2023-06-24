using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class SearchAllBlocksRequest : BaseFilter, IRequest<ICollection<BlockDto>>
{
}

public class SearchAllBlocksRequestHandler : IRequestHandler<SearchAllBlocksRequest, ICollection<BlockDto>>
{
    private readonly IReadRepository<Block> _repository;

    public SearchAllBlocksRequestHandler(IReadRepository<Block> repository) => _repository = repository;

    public async Task<ICollection<BlockDto>> Handle(SearchAllBlocksRequest request, CancellationToken cancellationToken)
    {
        var spec = new AllBlocksFullDetailsSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}