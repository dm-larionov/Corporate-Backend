using WebApi.Domain.Note;

namespace WebApi.Application.Note.Blocks;

public class BlockByIdFullDetailsSpec : Specification<Block, BlockDto>, ISingleResultSpecification
{
    public BlockByIdFullDetailsSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.BlockType)
            .Include(p => p.Fields)
            .ThenInclude(p => p.FieldType);
}