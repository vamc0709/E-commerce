using OnlineShop.DTOs;
using OnlineShop.Models;
using OnlineShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly ITagsRepository _tags;

    public TagsController(ILogger<TagsController> logger,
    ITagsRepository tags)
    {
        _logger = logger;
        _tags = tags;
    }
    [HttpGet("{tag_id}")]
    public async Task<ActionResult<TagsDTO>> GetById([FromRoute] long tag_id)
    {
        var tags = await _tags.GetById(tag_id);

        if (tags is null)
            return NotFound("No tag found with given tag id");
        return Ok(tags.asDto);
    }

    [HttpPost]
    public async Task<ActionResult<TagsDTO>> CreateTags([FromBody] TagsCreateDTO Data)
    {

        var toCreateTags = new Tags
        {
            TagName = Data.TagName.Trim(),
            Description = Data.Description,
            
        };

        var createdTags = await _tags.Create(toCreateTags);

        return StatusCode(StatusCodes.Status201Created, createdTags.asDto);
    }

    [HttpPut("{tag_id}")]
    public async Task<ActionResult> UpdateTags([FromRoute] long tag_id,
    [FromBody] TagsUpdateDTO Data)
    {
        var existing = await _tags.GetById(tag_id);
        if (existing is null)
            return NotFound("No tag found with given tag id");

        var toUpdateTags = existing with
        {
            TagName = Data.TagName?.Trim()?.ToLower() ?? existing.TagName,
            // Description = existing.Description,
            // Price = existing.Price,
            Status = existing.Status,
        };

        var didUpdate = await _tags.Update(toUpdateTags);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update tags");

        return NoContent();
    }

    [HttpDelete("{tag_id}")]
    public async Task<ActionResult> DeleteTags([FromRoute] long tag_id)
    {
        var existing = await _tags.GetById(tag_id);
        if (existing is null)
            return NotFound("No tags found with given tag id");

        var didDelete = await _tags.Delete(tag_id);

        return NoContent();
    }
}
