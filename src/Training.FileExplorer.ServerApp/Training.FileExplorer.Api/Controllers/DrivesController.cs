using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrivesController(IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromServices] IDriveService driveService)
    {
        var data = await driveService.GetAsync();
        var result = mapper.Map<IEnumerable<StorageDriveDto>>(data);
        return result.Any() ? Ok(result) : NoContent();
    }
}