using WebApi.Application.Note.Pages;

namespace WebApi.Host.Controllers.Note;

public class PagesController : VersionedApiController
{
    [HttpPost("all")]
    [MustHavePermission(FSHAction.Search, FSHResource.Pages)]
    [OpenApiOperation("Search all pages using available filters.", "")]
    public Task<ICollection<PageDto>> SearchAllAsync(SearchAllPagesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("{taxonomyId:guid}")]
    [MustHavePermission(FSHAction.Search, FSHResource.Pages)]
    [OpenApiOperation("Search pages using available filters.", "")]
    public async Task<ActionResult<ICollection<PageDto>>> SearchAsync(SearchPagesRequest request, Guid taxonomyId)
    {
        return taxonomyId != request.TaxonomyId
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Pages)]
    [OpenApiOperation("Get page details.", "")]
    public Task<PageDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetPageRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Pages)]
    [OpenApiOperation("Create a new Page.", "")]
    public Task<Guid> CreateAsync(CreatePageRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("{id:guid}/block/create")]
    [MustHavePermission(FSHAction.Update, FSHResource.Pages)]
    [OpenApiOperation("Create block into a Page.", "")]
    public async Task<ActionResult<PageDetailsDto>> CreatePageBlockAsync(CreatePageBlockRequest request, Guid id)
    {
        return id != request.PageId
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Pages)]
    [OpenApiOperation("Update a page.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdatePageRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Pages)]
    [OpenApiOperation("Delete a page.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeletePageRequest(id));
    }
}