using PropertyApp.Domain.Entities;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyImage> CreateAsync(PropertyImage image);
    Task DeleteByPropertyIdAsync(string propertyId);
} 