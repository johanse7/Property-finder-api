using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using PropertyApp.Domain.Entities;

using PropertyApp.Infrastructure.Models;

public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<PropertyDocument> _propertyCollection;
    private readonly IMongoCollection<PropertyImageDocument> _imageCollection;

    private readonly IMapper _mapper;
    public PropertyRepository(IMongoDatabase db, IMapper mapper)
    {
        _propertyCollection = db.GetCollection<PropertyDocument>("properties");
        _imageCollection = db.GetCollection<PropertyImageDocument>("propertyImages");
        _mapper = mapper;

        var keys = Builders<PropertyDocument>.IndexKeys.Ascending(p => p.Price).Text(p => p.Name).Text(p => p.Address);
        _propertyCollection.Indexes.CreateOne(new CreateIndexModel<PropertyDocument>(keys));
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        var allProperties = await _propertyCollection.Find(_ => true).ToListAsync();
        return _mapper.Map<IEnumerable<Property>>(allProperties);
    }

    public async Task<Property?> GetByIdAsync(string id)
    {

        if (!ObjectId.TryParse(id, out var objectId))
            return null;


        var filter = Builders<PropertyDocument>.Filter.Eq(p => p.Id, id);

        var propertyDetails = await _propertyCollection.Aggregate()
       .Match(filter)
       .Lookup(
           foreignCollectionName: "propertyImages",
           localField: "_id",
           foreignField: "IdProperty",
           @as: "Images"
       )
       .Lookup(
           foreignCollectionName: "propertyTraces",
           localField: "_id",
           foreignField: "IdProperty",
           @as: "Traces"
       )
       .Lookup(
           foreignCollectionName: "owners",
           localField: "IdOwner",
           foreignField: "_id",
           @as: "Owner"
       )
       .Unwind("Owner", new AggregateUnwindOptions<BsonDocument> { PreserveNullAndEmptyArrays = true })
       .As<PropertyAggregateResult>()
       .FirstOrDefaultAsync();

        return _mapper.Map<Property>(propertyDetails);
    }

    public async Task<(IEnumerable<Property> Items, long TotalCount)> FilterAsync(
      string? name,
      string? address,
      decimal? minPrice,
      decimal? maxPrice,
      int page = 1,
      int pageSize = 20)
    {
        var builder = Builders<PropertyDocument>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrWhiteSpace(name))
            filter &= builder.Regex(p => p.Name, new BsonRegularExpression(name, "i"));
        if (!string.IsNullOrWhiteSpace(address))
            filter &= builder.Regex(p => p.Address, new BsonRegularExpression(address, "i"));
        if (minPrice.HasValue)
            filter &= builder.Gte(p => p.Price, minPrice.Value);
        if (maxPrice.HasValue)
            filter &= builder.Lte(p => p.Price, maxPrice.Value);

        var skip = (page - 1) * pageSize;

        // Total count
        var totalCount = await _propertyCollection.CountDocumentsAsync(filter);

        var propertiesFiltered = _propertyCollection.Aggregate()
            .Match(filter)
            .Lookup<PropertyDocument, PropertyImageDocument, PropertyAggregateResult>(
                _imageCollection,
                p => p.Id,
                i => i.IdProperty,
                result => result.Images
            )
            .Skip(skip)
            .Limit(pageSize);

        var items = _mapper.Map<IEnumerable<Property>>(await propertiesFiltered.ToListAsync());

        return (items, totalCount);
    }

    public async Task<Property> CreateAsync(Property property)
    {
        var propertyToSave = _mapper.Map<PropertyDocument>(property);
        await _propertyCollection.InsertOneAsync(propertyToSave);
        return _mapper.Map<Property>(propertyToSave);
    }

    public async Task UpdateAsync(string id, Property property)
    {
        var propertyToUpdate = _mapper.Map<PropertyDocument>(property);
        await _propertyCollection.ReplaceOneAsync(d => d.Id == id, propertyToUpdate);
    }

    public async Task DeleteAsync(string id)
    {
        await _propertyCollection.DeleteOneAsync(d => d.Id == id);
    }
}
