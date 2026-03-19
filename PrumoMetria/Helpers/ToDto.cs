using PrumoMetria.Dto.StudyPlans;
using PrumoMetria.Entities;

namespace PrumoMetria.Helpers;

    public static class StudyPlanExtensions
    {
        public static StudyPlanDTO ToDTO(this StudyPlan studyPlan)
        {
            return new StudyPlanDTO
            {
                Id = studyPlan.Id,
                Name = studyPlan.Name,
                Color = studyPlan.Color,
                Description = studyPlan.Description,
                CreatedAt = studyPlan.CreatedAt
            };
        }
    }