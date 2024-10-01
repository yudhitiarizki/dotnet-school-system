using SchoolSystem.Data.Repositories;
using SchoolSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _enrollmentRepository.GetAllEnrollmentsAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
        {
            return await _enrollmentRepository.GetEnrollmentByIdAsync(id);
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            await _enrollmentRepository.AddEnrollmentAsync(enrollment);
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            await _enrollmentRepository.UpdateEnrollmentAsync(enrollment);
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            await _enrollmentRepository.DeleteEnrollmentAsync(id);
        }
    }
}
