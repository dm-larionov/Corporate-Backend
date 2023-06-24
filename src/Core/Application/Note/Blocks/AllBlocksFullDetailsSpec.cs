using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class AllBlocksFullDetailsSpec : EntitiesByBaseFilterSpec<Block, BlockDto>
{
    public AllBlocksFullDetailsSpec(SearchAllBlocksRequest request)
        : base(request) =>
        Query
            .Include(p => p.Fields);
}