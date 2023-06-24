using WebApi.Application.Note.Fields;

namespace WebApi.Host.Controllers.Note;

public class FieldsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Fields)]
    [OpenApiOperation("Search fields using available filters.", "")]
    public Task<ICollection<FieldDto>> SearchAsync(SearchFieldsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Fields)]
    [OpenApiOperation("Get field details.", "")]
    public Task<FieldDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetFieldRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Fields)]
    [OpenApiOperation("Create a new field.", "")]
    public Task<Guid> CreateAsync(CreateFieldRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Fields)]
    [OpenApiOperation("Update a field.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateFieldRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Fields)]
    [OpenApiOperation("Delete a field.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteFieldRequest(id));
    }
}