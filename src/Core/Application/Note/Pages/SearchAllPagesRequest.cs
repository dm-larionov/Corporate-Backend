using WebApi.Domain.Note;

namespace WebApi.Application.Note.Pages;

public class SearchAllPagesRequest : BaseFilter, IRequest<ICollection<PageDto>>
{
}

public class SearchAllPagesRequestHandler : IRequestHandler<SearchAllPagesRequest, ICollection<PageDto>>
{
    private readonly IReadRepository<Page> _repository;

    public SearchAllPagesRequestHandler(IReadRepository<Page> repository) => _repository = repository;

    public async Task<ICollection<PageDto>> Handle(SearchAllPagesRequest request, CancellationToken cancellationToken)
    {
        var spec = new AllPagesSpec(request);
        return await _repository.ListAsync(spec, cancellationToken: cancellationToken);
    }
}