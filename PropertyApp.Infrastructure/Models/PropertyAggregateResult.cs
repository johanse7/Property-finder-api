using MongoDB.Bson.Serialization.Attributes;

namespace PropertyApp.Infrastructure.Models;

public class PropertyAggregateResult
{
  [BsonId]
  [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
  public string Id { get; set; } = string.Empty;

  [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
  public string IdOwner { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public string CodeInternal { get; set; } = string.Empty;
  public int Year { get; set; }
  public List<PropertyImageDocument> Images { get; set; } = new();
  public List<PropertyTraceDocument> Traces { get; set; } = new();
  public OwnerDocument Owner { get; set; } = new();
}
