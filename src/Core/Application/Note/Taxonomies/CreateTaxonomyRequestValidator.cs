using WebApi.Domain.Note;
namespace WebApi.Application.Note.Taxonomies;

public class CreateTaxonomyRequestValidator : CustomValidator<CreateTaxonomyRequest>
{
    public CreateTaxonomyRequestValidator(IReadRepository<Taxonomy> taxonomyRepo, IStringLocalizer<CreateTaxonomyRequestValidator> T)
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(p => p.ParentId)
            .MustAsync(async (id, ct) => await taxonomyRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Taxonomy {0} Not Found.", id])
            .When(p => p.ParentId.HasValue);
    }
}