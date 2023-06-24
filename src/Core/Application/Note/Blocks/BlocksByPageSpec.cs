using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class BlocksByPageSpec : EntitiesByBaseFilterSpec<Block, BlockDto>
{
    public BlocksByPageSpec(SearchBlocksRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.Index)
            .Where(p => p.PageId == request.PageId);
}