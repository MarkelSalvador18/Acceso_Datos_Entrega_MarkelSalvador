using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {

        private readonly GymDbContext _context;

        //private int _nextId = 1; // Para el autonumerico

        public ActivityRepository(GymDbContext gymContext)
        {
            _context = gymContext;
        }

        public List<ActivityCreateDto> GetAll()
        {
            return _context.Activities.Select(
                a => new ActivityCreateDto
                {
                    Id = a.Id,
                    InstructorsId = a.Instructors
                    .Select(i => i.Id).ToList()
                }).ToList();
        }

        public ActivityCreateDto? Add(ActivityCreateDto activity, out List<ErrorResponse> errorResponse)
        {

            errorResponse = new List<ErrorResponse>();
            ActivityType? activityType = _context.ActivityTypes.FirstOrDefault(
                a => a.Id == activity.ActivityTypeId);

            if (activityType == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad no existe en la base de datos"));
            }

            if (activity.InstructorsId != null)
            {
                for (int i = 0; i < activity.InstructorsId.Count; i++)
                {
                    if (!_context.Intructors.Any(i => i.Id == i.Id))
                    {
                        errorResponse.Add(new ErrorResponse(40, "El monitor con ese id no existe"));
                    }
                   
                }
            }

            bool existeMonitor = _context.Activities.Any(
                            m => activity.DateStart < m.DateEnd
                            && activity.DateEnd > m.DateStart && m.Instructors.Any(
                                i => activity.InstructorsId.Contains(i.Id)));

            if (existeMonitor)
            {
                errorResponse.Add(new ErrorResponse(41, "Ya existe ese monitor a esa actividad"));

            }
            if (errorResponse.Count > 0)
            {
                return null;
            }


            Activity activityNueva = new Activity
            {
                Id = (int)activity.ActivityTypeId,
                DateStart = activity.DateStart,
                DateEnd = activity.DateEnd,
                MaxParticipants = activity.MaxParticipants,
                Instructors = _context.Intructors.Where(
                    i => activity.InstructorsId!.
                    Contains(i.Id)).ToList()



            };
            _context.Activities.Add(activityNueva);
            return activity;
        }
    }

}
