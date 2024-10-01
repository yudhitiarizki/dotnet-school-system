using SchoolSystem.Data.Repositories;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classRepository.GetAllClassesAsync();
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetClassByIdAsync(id);
        }

        public async Task AddClassAsync(Class classEntity)
        {
            await _classRepository.AddClassAsync(classEntity);
        }

        public async Task UpdateClassAsync(Class classEntity)
        {
            await _classRepository.UpdateClassAsync(classEntity);
        }

        public async Task DeleteClassAsync(int id)
        {
            await _classRepository.DeleteClassAsync(id);
        }
    }
}
