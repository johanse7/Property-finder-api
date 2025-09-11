
namespace PropertyApp.Application.Dtos;

public class PropertyDto
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Year { get; set; }
  public string CodeInternal { get; set; } = string.Empty;
}