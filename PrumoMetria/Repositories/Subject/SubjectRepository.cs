using Microsoft.EntityFrameworkCore;
using PrumoMetria.Data;
using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddSubject(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubject(Subject subject)
    {
        subject.UpdatedAt = DateTime.UtcNow;
        
        _context.Subjects.Update(subject);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubject(Subject subject)
    {
        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();
    }

    public async Task<Subject?> GetSubjectById(Guid subjectId)
    {
        return await _context.Subjects
            .Where(subject => subject.Id == subjectId)
            .Include(subject => subject.Contents)
            .Include(subject => subject.StudySessions)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Subject>> GetSubjects(Guid studyPlanId)
    {
        return await _context.Subjects
            .Where(subject => subject.StudyPlanId == studyPlanId)
            .Include(subject => subject.Contents)
            .OrderByDescending(subject => subject.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<int> CountSubjectsByStudyPlanId(Guid studyPlanId)
    {
        return await _context.Subjects
            .CountAsync(subject => subject.StudyPlanId == studyPlanId);
    }
}