using Microsoft.EntityFrameworkCore;
using PrumoMetria.Data;
using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly ApplicationDbContext _context;

    public ContentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddContent(Content content)
    {
        await _context.Contents.AddAsync(content);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateContent(Content content)
    {
        content.UpdatedAt = DateTime.UtcNow;

        _context.Contents.Update(content);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContent(Content content)
    {
        _context.Contents.Remove(content);
        await _context.SaveChangesAsync();
    }

    public async Task<Content?> GetContentById(Guid contentId)
    {
        return await _context.Contents
            .FirstOrDefaultAsync(c => c.Id == contentId);
    }

    public async Task<List<Content>> GetContentList(Guid subjectId)
    {
        return await _context.Contents
            .Where(content => content.SubjectId == subjectId)
            .OrderBy(content => content.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> CountContentBySubjectId(Guid subjectId)
    {
        return await _context.Contents
            .CountAsync(content => content.SubjectId == subjectId);
    }
}