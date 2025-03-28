using HCM.Domain.Models;
using HCM.Domain.Models.Application.Interfaces;
using HCM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HCM.Infrastructure.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly HcmDbContext _context;

        public PeopleService(HcmDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            person.Id = Guid.NewGuid();
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> UpdateAsync(Guid id, Person updated)
        {
            var person = await _context.People.FindAsync(id);
            if (person is null) return false;

            person.FirstName = updated.FirstName;
            person.LastName = updated.LastName;
            person.Email = updated.Email;
            person.Department = updated.Department;
            person.Position = updated.Position;
            person.HireDate = updated.HireDate;
            person.ManagerId = updated.ManagerId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            if (person is null) return false;

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
