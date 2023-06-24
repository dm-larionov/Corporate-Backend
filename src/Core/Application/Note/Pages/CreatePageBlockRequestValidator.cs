using WebApi.Domain.Note;
namespace WebApi.Application.Note.Pages;

public class CreatePageBlockRequestValidator : CustomValidator<CreatePageBlockRequest>
{
    public CreatePageBlockRequestValidator(IReadRepository<Page> pageRepo, IStringLocalizer<CreatePageBlockRequestValidator> T)
    {
        RuleFor(p => (int)p.Index)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.PageId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await pageRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Page {0} Not Found.", id]);
    }
}