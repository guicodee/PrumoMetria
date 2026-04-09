using Microsoft.AspNetCore.Identity;
using PrumoMetria.Dto;
using PrumoMetria.Entities;
using PrumoMetria.Helpers;
using PrumoMetria.Repositories;

namespace PrumoMetria.Services.StudyTasks;

public class StudyTasksService : IStudyTasksService
{
    private readonly IStudyTasksRepository _studyTasksRepository;
    private readonly IStudyPlanRepository _studyPlanRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public StudyTasksService(
        IStudyTasksRepository studyTasksRepository,
        IStudyPlanRepository studyPlanRepository,
        UserManager<ApplicationUser> userManager)
    {
        _studyTasksRepository = studyTasksRepository;
        _studyPlanRepository = studyPlanRepository;
        _userManager = userManager;
    }

    public async Task<ServiceResult<StudyTaskDTO>> Create(string userId, Guid studyPlanId, CreateStudyTasksDTO studyTask)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return ServiceResult<StudyTaskDTO>.Fail("User not found.", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(studyPlanId);
        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<StudyTaskDTO>.Fail("Study plan not found.", 404);

        var newStudyTask = new StudyTask
        {
            Title = studyTask.Title,
            Description = studyTask.Description,
            DueDate = studyTask.DueDate,
            StudyPlanId = studyPlanId,
            SubjectId = studyTask.SubjectId,
            ContentId = studyTask.ContentId
        };

        await _studyTasksRepository.AddStudyTasks(newStudyTask);

        return ServiceResult<StudyTaskDTO>.Success(newStudyTask.ToDTOStudyTasks(), 201);
    }

    public async Task<ServiceResult<StudyTaskDTO>> Update(string userId, Guid studyTaskId, UpdateStudyTaskDTO studyTask)
    {
        var task = await _studyTasksRepository.GetStudyTasksById(studyTaskId);
        if (task == null)
            return ServiceResult<StudyTaskDTO>.Fail("Task not found.", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(task.StudyPlanId);
        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<StudyTaskDTO>.Fail("Task not found.", 404);

        task.Title = studyTask.Title;
        task.Description = studyTask.Description;
        task.DueDate = studyTask.DueDate;
        task.IsCompleted = studyTask.IsCompleted;
        task.SubjectId = studyTask.SubjectId;
        task.ContentId = studyTask.ContentId;

        await _studyTasksRepository.UpdateStudyTasks(task);

        return ServiceResult<StudyTaskDTO>.Success(task.ToDTOStudyTasks());
    }

    public async Task<ServiceResult<bool>> Delete(string userId, Guid studyTaskId)
    {
        var task = await _studyTasksRepository.GetStudyTasksById(studyTaskId);
        if (task == null)
            return ServiceResult<bool>.Fail("Task not found.", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(task.StudyPlanId);
        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<bool>.Fail("Task not found.", 404);

        await _studyTasksRepository.DeleteStudyTasks(task);

        return ServiceResult<bool>.Success(true, 204);
    }

    public async Task<ServiceResult<StudyTaskDTO>> GetStudyTaskById(string userId, Guid studyTaskId)
    {
        var task = await _studyTasksRepository.GetStudyTasksById(studyTaskId);
        if (task == null)
            return ServiceResult<StudyTaskDTO>.Fail("Task not found.", 404);

        var studyPlan = await _studyPlanRepository.GetStudyPlanById(task.StudyPlanId);
        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<StudyTaskDTO>.Fail("Task not found.", 404);

        return ServiceResult<StudyTaskDTO>.Success(task.ToDTOStudyTasks());
    }


    public async Task<ServiceResult<List<StudyTaskDTO>>> GetStudyTasksByStudyPlanId(string userId, Guid studyPlanId)
    {
        var studyPlan = await _studyPlanRepository.GetStudyPlanById(studyPlanId);
        if (studyPlan == null || studyPlan.UserId != userId)
            return ServiceResult<List<StudyTaskDTO>>.Fail("Study plan not found.", 404);

        var tasks = await _studyTasksRepository.GetStudyTasks(studyPlanId);

        return ServiceResult<List<StudyTaskDTO>>.Success(tasks.Select(x => x.ToDTOStudyTasks()).ToList());
    }
}