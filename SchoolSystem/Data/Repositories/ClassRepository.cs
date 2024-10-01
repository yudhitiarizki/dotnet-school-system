using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSystem.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolContext _context;

        public ClassRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _context.Classes
                .AsNoTracking()
                .Include(s => s.Subjects)
                .ToListAsync();
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _context.Classes.AsNoTracking().Include(s => s.Subjects).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddClassAsync(Class classEntity)
        {
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(Class classEntity)
        {
            _context.Entry(classEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
