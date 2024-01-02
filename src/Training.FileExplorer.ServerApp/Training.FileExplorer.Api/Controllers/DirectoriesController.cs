using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectoriesController(
    IDirectoryProcessingService directoryProcessingService,
    IMapper mapper
    ) : ControllerBase
{
    [HttpGet("root/entries")]
    public async ValueTask<IActionResult> GetRootEntriesAsync(
        [FromQuery] StorageDirectoryEntryFilterModel filterModel,
        [FromServices] IWebHostEnvironment environment
    )
    {
        var data = await directoryProcessingService.GetEntriesAsync(environment.WebRootPath, filterModel);
        return data.Any() ? Ok(data) : NoContent();
    }

    [HttpGet("{directoryPath}/entries")]
    public async ValueTask<IActionResult> GetDirectoryEntriesByPathAsync(
        [FromRoute] string directoryPath,
        [FromQuery] StorageDirectoryEntryFilterModel filterModel
    )
    {
        var data = await directoryProcessingService.GetEntriesAsync(directoryPath, filterModel);
        return data.Any() ? Ok(data) : NoContent();
    }
}