using WebApi.Domain.Note;
namespace WebApi.Application.Note.Blocks;

public class CreateBlockRequestValidator : CustomValidator<CreateBlockRequest>
{
    public CreateBlockRequestValidator(IReadRepository<Page> pageRepo, IStringLocalizer<CreateBlockRequestValidator> T)
    {
        RuleFor(p => (int)p.Index)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.PageId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await pageRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Page {0} Not Found.", id]);
    }
}