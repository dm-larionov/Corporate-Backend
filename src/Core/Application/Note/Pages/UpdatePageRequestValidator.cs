namespace WebApi.Application.Note.Pages;

public class UpdatePageRequestValidator : CustomValidator<UpdatePageRequest>
{
    public UpdatePageRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75);
    }
}