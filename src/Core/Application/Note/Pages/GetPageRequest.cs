using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class GetPageRequest : IRequest<PageDetailsDto>
{
    public Guid Id { get; set; }

    public GetPageRequest(Guid id) => Id = id;
}

public class GetPageRequestHandler : IRequestHandler<GetPageRequest, PageDetailsDto>
{
    private readonly IRepository<Page> _repository;
    private readonly IStringLocalizer _t;

    public GetPageRequestHandler(IRepository<Page> repository, IStringLocalizer<GetPageRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<PageDetailsDto> Handle(GetPageRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Page, PageDetailsDto>)new PageByIdFullDetailsSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Page {0} Not Found.", request.Id]);
}