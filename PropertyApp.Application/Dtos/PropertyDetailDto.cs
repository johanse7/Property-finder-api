
namespace PropertyApp.Application.Dtos;
public class PropertyDetailDto
{
  public PropertyDto Property { get; set; } = new();
  public OwnerDto? Owner { get; set; }
  public IEnumerable<PropertyImageDto> Images { get; set; } = Enumerable.Empty<PropertyImageDto>();
  public IEnumerable<PropertyTraceDto> Traces { get; set; } = Enumerable.Empty<PropertyTraceDto>();

}