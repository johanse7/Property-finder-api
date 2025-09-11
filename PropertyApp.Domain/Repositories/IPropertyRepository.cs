using PropertyApp.Domain.Entities;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(string id);
    Task<(IEnumerable<Property> Items, long TotalCount)> FilterAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 20);
    Task<Property> CreateAsync(Property property);
    Task UpdateAsync(string id, Property property);
    Task DeleteAsync(string id);
}
