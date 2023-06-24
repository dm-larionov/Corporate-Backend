using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class AllPagesSpec : EntitiesByBaseFilterSpec<Page, PageDto>
{
    public AllPagesSpec(SearchAllPagesRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.CreatedOn);
}