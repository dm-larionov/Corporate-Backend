using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class AllTaxonomiesSpec : EntitiesByBaseFilterSpec<Taxonomy, TaxonomyDto>
{
    public AllTaxonomiesSpec(SearchAllTaxonomiesRequest request)
        : base(request) =>
        Query.OrderBy(o => o.CreatedBy);
}