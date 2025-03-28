namespace HCM.Domain.Models.Application.Interfaces
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(Guid id);
        Task<Person> CreateAsync(Person person);
        Task<bool> UpdateAsync(Guid id, Person updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
