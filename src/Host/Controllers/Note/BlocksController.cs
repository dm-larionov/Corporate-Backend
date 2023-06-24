using WebApi.Application.Note.Blocks;

namespace WebApi.Host.Controllers.Note;

public class BlocksController : VersionedApiController
{
    [HttpPost("all")]
    [MustHavePermission(FSHAction.Search, FSHResource.Blocks)]
    [OpenApiOperation("get all blocks using available filters.", "")]
    public Task<ICollection<BlockDto>> SearchAllAsync(SearchAllBlocksRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Blocks)]
    [OpenApiOperation("Search taxonomies using available filters.", "")]
    public Task<ICollection<BlockDto>> SearchAsync(SearchBlocksRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Blocks)]
    [OpenApiOperation("Get taxonomy details.", "")]
    public Task<BlockDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBlockRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Blocks)]
    [OpenApiOperation("Create a new taxonomy.", "")]
    public Task<Guid> CreateAsync(CreateBlockRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Blocks)]
    [OpenApiOperation("Delete a taxonomy.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBlockRequest(id));
    }
}