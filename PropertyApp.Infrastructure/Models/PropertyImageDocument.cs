using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Models;

public class PropertyImageDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; } = string.Empty;

    public string File { get; set; } = string.Empty;

    public bool Enabled { get; set; } = true;

    public PropertyImage ToDomain() => new PropertyImage
    {
        Id = Id,
        IdProperty = IdProperty,
        File = File,
        Enabled = Enabled
    };

    public static PropertyImageDocument FromDomain(PropertyImage img) => new PropertyImageDocument
    {
        Id = img.Id,
        IdProperty = img.IdProperty,
        File = img.File,
        Enabled = img.Enabled
    };
}
