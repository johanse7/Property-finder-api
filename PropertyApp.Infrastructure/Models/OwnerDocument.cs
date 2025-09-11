

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace  PropertyApp.Infrastructure.Models;
public class OwnerDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = string.Empty;

  public string Name { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;

  public string Photo { get; set; } = string.Empty;

  public DateTime? Birthday { get; set; }

  public Owner ToDomain() => new Owner
  {
    Id = Id,
    Name = Name,
    Address = Address,
    Photo = Photo,
    Birthday = Birthday

  };

  public static OwnerDocument FromDomain(Owner owner) => new OwnerDocument
  {
    Id = owner.Id,
    Name = owner.Name,
    Address = owner.Address,
    Photo = owner.Photo,
    Birthday = owner.Birthday
  };
}