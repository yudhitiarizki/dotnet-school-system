using SchoolSystem.Data.Repositories;
using SchoolSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllSubjectsAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepository.GetSubjectByIdAsync(id);
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            await _subjectRepository.AddSubjectAsync(subject);
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            await _subjectRepository.UpdateSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            await _subjectRepository.DeleteSubjectAsync(id);
        }
    }
}
