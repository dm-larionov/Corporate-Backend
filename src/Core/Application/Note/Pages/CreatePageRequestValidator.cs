using WebApi.Domain.Note;
namespace WebApi.Application.Note.Pages;

public class CreatePageRequestValidator : CustomValidator<CreatePageRequest>
{
    public CreatePageRequestValidator(IReadRepository<Taxonomy> taxonomyRepo, IStringLocalizer<CreatePageRequestValidator> T)
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(p => p.TaxonomyId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await taxonomyRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Taxonomy {0} Not Found.", id])
            .When(p => p.TaxonomyId.HasValue);
    }
}