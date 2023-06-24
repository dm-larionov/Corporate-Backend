using WebApi.Domain.Note;

namespace WebApi.Application.Note.Fields;

public class GetFieldRequest : IRequest<FieldDto>
{
    public Guid Id { get; set; }

    public GetFieldRequest(Guid id) => Id = id;
}

public class GetFieldRequestHandler : IRequestHandler<GetFieldRequest, FieldDto>
{
    private readonly IRepository<Field> _repository;
    private readonly IStringLocalizer _t;

    public GetFieldRequestHandler(IRepository<Field> repository, IStringLocalizer<GetFieldRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<FieldDto> Handle(GetFieldRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Field, FieldDto>)new FieldByIdFullDetailsSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Field {0} Not Found.", request.Id]);
}