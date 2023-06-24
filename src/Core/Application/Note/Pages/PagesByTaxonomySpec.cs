using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class PagesByTaxonomySpec : EntitiesByBaseFilterSpec<Page, PageDto>
{
    public PagesByTaxonomySpec(SearchPagesRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.CreatedOn)
            .Where(p => p.Taxonomies.Any(x => x.Id.Equals(request.TaxonomyId)) || (p.Taxonomies.Any(x => x.Id.Equals(request.TaxonomyId)) && p.CreatedBy.Equals(request.UserId)));
}