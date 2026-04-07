using Microsoft.EntityFrameworkCore;
using PrumoMetria.Data;
using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public class StudyTasksRepository : IStudyTasksRepository
{
    private readonly ApplicationDbContext _context;

    public StudyTasksRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddStudyTasks(StudyTask task)
    {
        await _context.StudyTask.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudyTasks(StudyTask task)
    {
        task.UpdatedAt = DateTime.UtcNow;
        
        _context.StudyTask.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudyTasks(StudyTask task)
    {
        _context.StudyTask.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task<StudyTask?> GetStudyTasksById(Guid taskId)
    {
        return await _context.StudyTask
            .FirstOrDefaultAsync(task => task.Id == taskId);
    }

    public async Task<List<StudyTask>> GetStudyTaskss(Guid studyPlanId)
    {
        return await _context.StudyTask
            .Where(task => task.StudyPlanId == studyPlanId)
            .OrderBy(task => task.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> CountStudyTaskByStudyPlanId(Guid studyPlanId)
    {
        return await _context.StudyTask
            .CountAsync(task => task.StudyPlanId == studyPlanId);
    }
}