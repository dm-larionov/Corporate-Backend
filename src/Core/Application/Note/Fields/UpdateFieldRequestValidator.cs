namespace WebApi.Application.Note.Fields;

public class UpdatePageRequestValidator : CustomValidator<UpdateFieldRequest>
{
    public UpdatePageRequestValidator()
    {
        RuleFor(p => p.Value)
            .NotEmpty()
            .MaximumLength(75);
    }
}