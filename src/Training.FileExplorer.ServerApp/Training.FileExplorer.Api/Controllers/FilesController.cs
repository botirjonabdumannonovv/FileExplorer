using Microsoft.AspNetCore.Mvc;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController(IWebHostEnvironment environment, IFileProcessingService fileProcessingService) : ControllerBase
{
    [HttpGet("root/files/filter")]
    public async ValueTask<IActionResult> GetFilesSummary()
    {
        var result = await fileProcessingService.GetFilterDataModelAsync(environment.WebRootPath);
        return Ok(result);
    }

    [HttpGet("root/files/by-filter")]
    public async ValueTask<IActionResult> GetFilesByFilter([FromQuery] StorageFileFilterModel filterModel)
    {
        filterModel.DirectoryPath = environment.WebRootPath;
        var files = await fileProcessingService.GetByFilterAsync(filterModel);
        return files.Any() ? Ok(files) : NotFound(files);
    }
}