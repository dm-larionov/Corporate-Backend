using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class GetBlockRequest : IRequest<BlockDto>
{
    public Guid Id { get; set; }

    public GetBlockRequest(Guid id) => Id = id;
}

public class GetBlockRequestHandler : IRequestHandler<GetBlockRequest, BlockDto>
{
    private readonly IRepository<Block> _repository;
    private readonly IStringLocalizer _t;

    public GetBlockRequestHandler(IRepository<Block> repository, IStringLocalizer<GetBlockRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<BlockDto> Handle(GetBlockRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Block, BlockDto>)new BlockByIdFullDetailsSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Block {0} Not Found.", request.Id]);
}