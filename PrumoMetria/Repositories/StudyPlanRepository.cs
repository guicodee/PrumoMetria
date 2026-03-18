using Microsoft.EntityFrameworkCore;
using PrumoMetria.Data;
using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public class StudyPlanRepository : IStudyPlanRepository
{
    private readonly ApplicationDbContext _context;

    public StudyPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddStudyPlan(StudyPlan studyPlan)
    {
        await _context.StudyPlans.AddAsync(studyPlan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudyPlan(StudyPlan studyPlan)
    {
        studyPlan.UpdatedAt = DateTime.UtcNow;
        
        _context.StudyPlans.Update(studyPlan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudyPlan(StudyPlan studyPlan)
    {
        _context.StudyPlans.Remove(studyPlan);
        await _context.SaveChangesAsync();
    }

    public async Task<StudyPlan?> GetStudyPlanById(Guid id)
    {
        return await _context.StudyPlans
            .Where(plan => plan.Id == id)
            .Include(plan => plan.Subjects)
            .ThenInclude(subject => subject.Contents)
            .Include(plan => plan.Tasks)
            .FirstOrDefaultAsync();
    }

    public async Task<List<StudyPlan>> GetStudyPlanByUserId(string userId)
    {
        return await _context.StudyPlans
            .Where(plan => plan.UserId == userId)
            .OrderByDescending(plan => plan.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetStudyPlanCountByUserId(string userId)
    {
        return await _context.StudyPlans
            .CountAsync(plan => plan.UserId == userId);
    }

    public async Task<bool> ExistsStudyPlan(string userId, Guid studyPlanId)
    {
        return await _context.StudyPlans
            .AnyAsync(plan => plan.UserId == userId && plan.Id == studyPlanId);
    }
}