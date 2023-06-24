using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class TaxonomiesSpec : EntitiesByBaseFilterSpec<Taxonomy, TaxonomyDto>
{
    public TaxonomiesSpec(SearchTaxonomiesRequest request)
        : base(request) =>
        Query.OrderBy(o => o.CreatedBy)
        .Where(x => x.ParentTaxonomyId == request.TaxonomyId);
}