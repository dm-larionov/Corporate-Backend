using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class PageByIdFullDetailsSpec : Specification<Page, PageDetailsDto>, ISingleResultSpecification
{
    public PageByIdFullDetailsSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Blocks)
            .ThenInclude(p => p.BlockType)
            .Include(p => p.Blocks)
            .ThenInclude(p => p.Fields)
            .ThenInclude(p => p.FieldType);
}