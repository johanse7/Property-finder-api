public interface IPropertyTraceRepository
{
    Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyTrace> CreateAsync(PropertyTrace pt);
    Task DeleteByPropertyIdAsync(string propertyId);
}