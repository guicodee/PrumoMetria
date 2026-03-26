using PrumoMetria.Dto.Contents;

namespace PrumoMetria.Services.Contents;

public interface IContentService
{
    Task<ServiceResult<ContentDTO?>> Create(string userId, Guid subjectId, CreateContentDTO content);
    Task<ServiceResult<ContentDTO?>> Update(string userId, Guid contentId, UpdateContentDTO content);
    Task<ServiceResult<bool>> Delete(string userId, Guid contentId);
    Task<ServiceResult<ContentDTO?>> GetContentById(string userId, Guid contentId);
    Task<ServiceResult<List<ContentDTO>>> GetContentList(string userId, Guid subjectId);
}