using Microsoft.AspNetCore.Identity;
using PrumoMetria.Dto.Contents;
using PrumoMetria.Entities;
using PrumoMetria.Helpers;
using PrumoMetria.Repositories;

namespace PrumoMetria.Services.Contents;

public class ContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IStudyPlanRepository _studyPlanRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ContentService(
        IContentRepository contentRepository,
        ISubjectRepository subjectRepository,
        IStudyPlanRepository studyPlanRepository,
        UserManager<ApplicationUser> userManager )
    {
        _contentRepository = contentRepository;
        _subjectRepository = subjectRepository;
        _studyPlanRepository = studyPlanRepository;
        _userManager = userManager;
    }
    
    public async Task<ServiceResult<ContentDTO?>> Create(string userId, Guid subjectId, CreateContentDTO content)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return ServiceResult<ContentDTO?>.Fail("User not found", 404);

        var existsSubject = await _subjectRepository.GetSubjectById(subjectId);
        
        if(existsSubject == null)
            return ServiceResult<ContentDTO?>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null ||  studyPlan.UserId != userId)
            return ServiceResult<ContentDTO?>.Fail("StudyPlan not found", 404);
        
        var countOfContents = await _contentRepository.CountContentBySubjectId(subjectId);
        var maxContents = user.IsPremium ? int.MaxValue : 20;

        if (countOfContents >= maxContents)
            return ServiceResult<ContentDTO?>.Fail("Contents limit reached.", 403);
        
        var newContent = new Content()
        {
            Name = content.Name,
            Description = content.Description,
            Priority = content.Priority,
            SubjectId = existsSubject.Id,
        };

        await _contentRepository.AddContent(newContent);
        return ServiceResult<ContentDTO?>.Success(newContent.ToDTOContent(), 201);
    }

    public async Task<ServiceResult<ContentDTO?>> Update(string userId, Guid contentId, UpdateContentDTO content)
    {
        var existsContent = await _contentRepository.GetContentById(contentId);
        
        if(existsContent == null)
            return ServiceResult<ContentDTO?>.Fail("Content not found", 404);
        
        var existsSubject = await _subjectRepository.GetSubjectById(existsContent.SubjectId);
        
        if(existsSubject == null)
            return ServiceResult<ContentDTO?>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null ||  studyPlan.UserId != userId)
            return ServiceResult<ContentDTO?>.Fail("StudyPlan not found", 404);

        existsContent.Name = content.Name;
        existsContent.Description = content.Description;
        existsContent.Priority = content.Priority;
        existsContent.Status = content.Status;
        
        await _contentRepository.UpdateContent(existsContent);
        return ServiceResult<ContentDTO?>.Success(existsContent.ToDTOContent());
    }

    public async Task<ServiceResult<bool>> Delete(string userId, Guid contentId)
    {
        var existsContent = await _contentRepository.GetContentById(contentId);
        
        if(existsContent == null)
            return ServiceResult<bool>.Fail("Content not found", 404);
        
        var existsSubject = await _subjectRepository.GetSubjectById(existsContent.SubjectId);
        
        if(existsSubject == null)
            return ServiceResult<bool>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null ||  studyPlan.UserId != userId)
            return ServiceResult<bool>.Fail("StudyPlan not found", 404);

        await _contentRepository.DeleteContent(existsContent);
        
        return ServiceResult<bool>.Success(true, 204);
    }

    public async Task<ServiceResult<ContentDTO?>> GetContentById(string userId, Guid contentId)
    {
        var result = await _contentRepository.GetContentById(contentId);
        
        if(result == null)
            return ServiceResult<ContentDTO?>.Fail("Content not found", 404);
        
        var existsSubject = await _subjectRepository.GetSubjectById(result.SubjectId);
        
        if(existsSubject == null)
            return ServiceResult<ContentDTO?>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null ||  studyPlan.UserId != userId)
            return ServiceResult<ContentDTO?>.Fail("StudyPlan not found", 404);
        
        return ServiceResult<ContentDTO?>.Success(result.ToDTOContent());
    }

    public async Task<ServiceResult<List<ContentDTO>>> GetContentList(string userId, Guid subjectId)
    {
        var existsSubject = await _subjectRepository.GetSubjectById(subjectId);
        
        if(existsSubject == null)
            return ServiceResult<List<ContentDTO>>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null ||  studyPlan.UserId != userId)
            return ServiceResult<List<ContentDTO>>.Fail("StudyPlan not found", 404);
        
        var result = await _contentRepository.GetContentList(existsSubject.Id);
        return ServiceResult<List<ContentDTO>>.Success(result.Select(x => x.ToDTOContent()).ToList());
    }
}