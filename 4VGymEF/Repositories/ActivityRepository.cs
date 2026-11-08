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
            return _context.Activities.Select(a => new ActivityCreateDto {Id = a.Id, InstructorsId = a.Instructors.Select(i => i.Id).ToList()})
        }

        public ActivityCreateDto? Add(ActivityCreateDto activity, out List<ErrorResponse> errorResponse)
        {

            errorResponse = new List<ErrorResponse>();


            ActivityType? activityType = _context.ActivityTypes.FirstOrDefault(a => a.Id == activity.ActivityTypeId);



            if (activityType == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad no existe en la base de datos"));
            }

            if (activity.InstructorsId != null)
            {
                for (int i = 0; i < activity.InstructorsId.Count; i++)
                {
                    if (_context.Intructors.Any(i => i.Id == i.Id))
                    {
                        bool existeMonitor = _context.Activities.Any(
                            m => activity.DateStart < m.DateEnd
                            && activity.DateEnd > m.DateStart && m.l);
                    }
                    
                }
            }
            // Validacion 2: Que existan todos los monitores
            errorResponse =
                activity.InstructorsId
                        .Where(instructorId => _activityType.GetById(instructorId) == null)
                        .Select(instructorIdNotFound =>
                        {
                            return new ErrorResponse(35, "El instructor: " + instructorIdNotFound + " no existe");
                        })
                        .ToList();
            if (errorResponse.Count > 0)
            {
                return null;
            }
            ActivityCreateDto activityType1 = new ActivityCreateDto
            {
                Id = activity.Id,
                InstructorsId = activity.InstructorsId,
                ActivityTypeId = activity.ActivityTypeId,
                DateEnd = activity.DateEnd,
                DateStart = activity.DateStart,
                MaxParticipants = activity.MaxParticipants
                

                

            };

            _activityType.Add(activity);


            return activity;
        }
    }

}
