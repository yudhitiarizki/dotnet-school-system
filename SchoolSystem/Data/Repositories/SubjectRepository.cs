using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSystem.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _context;

        public SubjectRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects
                .Include(s => s.Teacher)
                .Include(s => s.Class)
                .Include(s => s.Enrollments)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Teacher)
                .Include(s => s.Class)
                .Include(s => s.Enrollments)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Menambah Subject baru ke database
        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Menghapus Subject berdasarkan ID
        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _context.Subjects
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
