using SchoolSystem.Data.Repositories;
using SchoolSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllTeachersAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.AddTeacherAsync(teacher);
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateTeacherAsync(teacher);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}
