using MongoDB.Driver;
using PropertyApp.Infrastructure.Models;

public class OwnerRepository: IOwnerRepository
{
    private readonly IMongoCollection<OwnerDocument> _col;
    public OwnerRepository(IMongoDatabase db, string collectionName = "owners")
    {
        _col = db.GetCollection<OwnerDocument>(collectionName);
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        var docs = await _col.Find(_ => true).ToListAsync();
        return docs.Select(d => d.ToDomain());
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        var doc = await _col.Find(d => d.Id == id).FirstOrDefaultAsync();
        return doc?.ToDomain();
    }

    public async Task<Owner> CreateAsync(Owner owner)
    {
        var doc = OwnerDocument.FromDomain(owner);
        await _col.InsertOneAsync(doc);
        return doc.ToDomain();
    }

    public async Task UpdateAsync(string id, Owner owner)
    {
        var doc = OwnerDocument.FromDomain(owner);
        await _col.ReplaceOneAsync(d => d.Id == id, doc);
    }

    public async Task DeleteAsync(string id)
    {
        await _col.DeleteOneAsync(d => d.Id == id);
    }

}