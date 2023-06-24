namespace WebApi.Application.Note.Taxonomies;

public class UpdateTaxonomyRequestValidator : CustomValidator<UpdateTaxonomyRequest>
{
    public UpdateTaxonomyRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(75);
    }
}