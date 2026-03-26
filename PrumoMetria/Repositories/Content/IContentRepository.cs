using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public interface IContentRepository 
{
    Task AddContent(Content content);
    Task UpdateContent(Content content);
    Task DeleteContent(Content content);
    Task<Content?> GetContentById(Guid contentId);
    Task<List<Content>> GetContentList(Guid subjectId);
    Task<int> CountContentBySubjectId(Guid subjectId);
}