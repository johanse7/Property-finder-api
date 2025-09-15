
using Microsoft.AspNetCore.Mvc;

namespace PropertyApp.Api.Controllers;

[ApiController]
[Route("real-estate/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly GetPropertiesUseCase _getProperties;
    private readonly GetPropertyDetailsUseCase _getPropertyDetails;

    public PropertiesController(GetPropertiesUseCase getProperties, GetPropertyDetailsUseCase getPropertyDetails)
    {
        _getProperties = getProperties;
        _getPropertyDetails = getPropertyDetails;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PropertiesRequest request)
    {
        var result = await _getProperties.ExecuteAsync(
            request.Name, request.Address,
            request.MinPrice, request.MaxPrice,
            request.Page, request.PageSize
        );

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _getPropertyDetails.ExecuteAsync(id);

        if (result == null)
            return NotFound(new { message = $"Property with id '{id}' was not found." });

        return Ok(result);
    }
}
