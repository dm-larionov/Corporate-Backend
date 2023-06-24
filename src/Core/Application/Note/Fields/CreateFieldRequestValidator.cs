using WebApi.Domain.Note;
namespace WebApi.Application.Note.Fields;

public class CreateFieldRequestValidator : CustomValidator<CreateFieldRequest>
{
    public CreateFieldRequestValidator(IReadRepository<Block> blockRepo, IStringLocalizer<CreateFieldRequestValidator> T)
    {
        RuleFor(p => p.Value)
            .NotEmpty();
    }
}