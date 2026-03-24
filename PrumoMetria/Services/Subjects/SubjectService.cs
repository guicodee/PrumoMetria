using Microsoft.AspNetCore.Identity;
using PrumoMetria.Dto.Subjects;
using PrumoMetria.Entities;
using PrumoMetria.Helpers;
using PrumoMetria.Repositories;

namespace PrumoMetria.Services.Subjects;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IStudyPlanRepository _studyPlanRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public SubjectService(
        ISubjectRepository subjectRepository, 
        IStudyPlanRepository studyPlanRepository,
        UserManager<ApplicationUser> userManager)
    {
        _subjectRepository = subjectRepository;
        _studyPlanRepository = studyPlanRepository;
        _userManager = userManager;
    }

    public async Task<ServiceResult<SubjectDTO?>> Create(string userId, Guid studyPlanId, CreateSubjectDTO subject)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if(user == null)
            return ServiceResult<SubjectDTO?>.Fail("User not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(studyPlanId);
        
        if(studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<SubjectDTO?>.Fail("StudyPlan not found", 404);

        var countOfSubjects = await _subjectRepository.CountSubjectsByStudyPlanId(studyPlanId);
        var maxSubjects = user.IsPremium ? int.MaxValue : 15;
        
        if(countOfSubjects >= maxSubjects) 
            return ServiceResult<SubjectDTO?>.Fail("Subject limit reached.", 403);

        var newSubject = new Subject()
        {
            Name = subject.Name,
            Color = subject.Color,
            StudyPlanId = studyPlanId,
            
        };
        
        await _subjectRepository.AddSubject(newSubject);
        
        return ServiceResult<SubjectDTO?>.Success(newSubject.ToDTOSubject());
    }

    public async Task<ServiceResult<SubjectDTO?>> Update(string userId, Guid subjectId, UpdateSubjectDTO subject)
    {
        var existsSubject = await _subjectRepository.GetSubjectById(subjectId);
        
        if (existsSubject == null)
            return ServiceResult<SubjectDTO?>.Fail("Subject not found", 404);
        
        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<SubjectDTO?>.Fail("Subject not found", 404);
        
        existsSubject.Name = subject.Name;
        existsSubject.Color = subject.Color;

        await _subjectRepository.UpdateSubject(existsSubject);
        
        return ServiceResult<SubjectDTO?>.Success(existsSubject.ToDTOSubject());
    }

    public async Task<ServiceResult<bool>> Delete(string userId, Guid subjectId)
    {
        var existsSubject = await _subjectRepository.GetSubjectById(subjectId);
        
        if (existsSubject == null)
            return ServiceResult<bool>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<bool>.Fail("Subject not found", 404);

        await _subjectRepository.DeleteSubject(existsSubject);
        
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<SubjectDTO?>> GetSubjectById(string userId, Guid subjectId)
    {
        var existsSubject = await _subjectRepository.GetSubjectById(subjectId);
        
        if (existsSubject == null)
            return ServiceResult<SubjectDTO?>.Fail("Subject not found", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(existsSubject.StudyPlanId);
        
        if(studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<SubjectDTO?>.Fail("Subject not found", 404);

        
        return ServiceResult<SubjectDTO?>.Success(existsSubject.ToDTOSubject());
    }

    public async Task<ServiceResult<List<SubjectDTO>>> GetSubjectsByUserId(string userId, Guid studyPlanId)
    {
        var studyPlan = await _studyPlanRepository.GetStudyPlanById(studyPlanId);

        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<List<SubjectDTO>>.Fail("Study plan not found.", 404);
        
        var subjects = await _subjectRepository.GetSubjects(studyPlanId);
        
        return ServiceResult<List<SubjectDTO>>.Success(subjects.Select(x => x.ToDTOSubject()).ToList());
    }
}