
namespace PropertyApp.Domain.Entities;

public class Property
{

    public string Id { get; set; } = string.Empty;

    public string IdOwner { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public string CodeInternal { get; set; } = string.Empty;

    public int Year { get; set; }

    public List<PropertyImage> Images { get; set; } = new();

    public List<PropertyTrace> Traces { get; set; } = new();

    public Owner Owner { get; set; } = new();

}
