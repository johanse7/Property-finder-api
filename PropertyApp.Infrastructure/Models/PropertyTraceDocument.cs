using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PropertyApp.Infrastructure.Models;

public class PropertyTraceDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = string.Empty;

  [BsonRepresentation(BsonType.ObjectId)]
  public string IdProperty { get; set; } = string.Empty;
  public DateTime DateSale { get; set; }
  public string Name { get; set; } = string.Empty;
  public decimal Value { get; set; }
  public decimal Tax { get; set; }

  public PropertyTrace ToDomain() => new PropertyTrace
  {
    Id = Id,
    IdProperty = IdProperty,
    DateSale = DateSale,
    Name = Name,
    Value = Value,
    Tax = Tax
  };

  public static PropertyTraceDocument FromDomain(PropertyTrace pt) => new PropertyTraceDocument
  {
    Id = pt.Id,
    IdProperty = pt.IdProperty,
    DateSale = pt.DateSale,
    Name = pt.Name,
    Value = pt.Value,
    Tax = pt.Tax
  };
}