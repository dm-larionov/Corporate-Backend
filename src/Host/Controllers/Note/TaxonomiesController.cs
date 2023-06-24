using WebApi.Application.Note.Taxonomies;

namespace WebApi.Host.Controllers.Note;

public class TaxonomiesController : VersionedApiController
{
    [HttpPost("all")]
    [MustHavePermission(FSHAction.Search, FSHResource.Taxonomies)]
    [OpenApiOperation("Search all taxonomies using available filters.", "")]
    public Task<ICollection<TaxonomyDto>> SearchAllAsync(SearchAllTaxonomiesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Taxonomies)]
    [OpenApiOperation("Search taxonomies using available filters.", "")]
    public Task<ICollection<TaxonomyDto>> SearchAsync(SearchTaxonomiesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Taxonomies)]
    [OpenApiOperation("Get taxonomy details.", "")]
    public Task<TaxonomyDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTaxonomyRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Taxonomies)]
    [OpenApiOperation("Create a new taxonomy.", "")]
    public Task<Guid> CreateAsync(CreateTaxonomyRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Taxonomies)]
    [OpenApiOperation("Update a taxonomy.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTaxonomyRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Taxonomies)]
    [OpenApiOperation("Delete a taxonomy.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTaxonomyRequest(id));
    }
}