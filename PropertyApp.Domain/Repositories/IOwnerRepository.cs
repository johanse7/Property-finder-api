public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string id);
    Task<Owner> CreateAsync(Owner owner);
    Task UpdateAsync(string id, Owner owner);
    Task DeleteAsync(string id);
}