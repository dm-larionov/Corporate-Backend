using WebApi.Domain.Note;

namespace WebApi.Application.Note.Taxonomies;

public class TaxonomyByIdFullDetailsSpec : Specification<Taxonomy, TaxonomyDetailsDto>, ISingleResultSpecification
{
    public TaxonomyByIdFullDetailsSpec(Guid id) =>
        Query
            .Where(p => p.Id == id);
}