using MongoDB.Driver;
using PropertyApp.Infrastructure.Models;

public class PropertyTraceRepository: IPropertyTraceRepository
{
    private readonly IMongoCollection<PropertyTraceDocument> _col;
    public PropertyTraceRepository(IMongoDatabase db, string collectionName = "propertyTraces")
    {
        _col = db.GetCollection<PropertyTraceDocument>(collectionName);
    }

    public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId)
    {
        var docs = await _col.Find(d => d.IdProperty == propertyId).ToListAsync();
        return docs.Select(d => d.ToDomain());
    }

    public async Task<PropertyTrace> CreateAsync(PropertyTrace pt)
    {
        var doc = PropertyTraceDocument.FromDomain(pt);
        await _col.InsertOneAsync(doc);
        return doc.ToDomain();
    }

    public async Task DeleteByPropertyIdAsync(string propertyId)
    {
        await _col.DeleteManyAsync(d => d.IdProperty == propertyId);
    }

}