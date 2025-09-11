using MongoDB.Driver;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure.Models;

public class PropertyImageRepository : IPropertyImageRepository
{
  private readonly IMongoCollection<PropertyImageDocument> _col;
  public PropertyImageRepository(IMongoDatabase db, string collectionName = "propertyImages")
  {
    _col = db.GetCollection<PropertyImageDocument>(collectionName);
  }

  public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId)
  {
    var docs = await _col.Find(d => d.IdProperty == propertyId).ToListAsync();
    return docs.Select(d => d.ToDomain());
  }

  public async Task<PropertyImage> CreateAsync(PropertyImage propertyImage)
  {
    var doc = PropertyImageDocument.FromDomain(propertyImage);
    await _col.InsertOneAsync(doc);
    return doc.ToDomain();
  }

  public async Task DeleteByPropertyIdAsync(string propertyId)
  {
    await _col.DeleteManyAsync(d => d.IdProperty == propertyId);
  }
}