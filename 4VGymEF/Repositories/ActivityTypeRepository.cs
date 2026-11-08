using _4VGymEF.Data;
using _4VGymEF.Models;
using System.Linq;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        //private List<ActivityType> _activityTypes = new();
        private readonly GymDbContext _context;
        //private int _nextId = 1; // Para el autonumerico
        public ActivityTypeRepository(GymDbContext context)
        {
            _context = context;
            
        }
        
        // Método para obtener todos los tipos de actividades
        public IEnumerable<ActivityTypeCreateDto> GetAll()
        {
        var listaActivityType = _context.ActivityTypes.ToList();
            return listaActivityType.Select(l => new ActivityTypeCreateDto 
            { Id = l.Id, 
                Name = l.Name, 
                NumberMonitors = l.NumberMonitors}).ToList();
        }


        /// <summary>
        /// Guarda un nuevo tipo de actividad generando su ID de forma automática. (Autonumerico)
        /// </summary>
        /// <param name="activityType">Objeto con los datos a guardar (sin ID o con ID no asignado).</param>
        /// <returns>La entidad guardada con su ID autogenerado.</returns>
        public ActivityTypeCreateDto Add(ActivityTypeCreateDto activityType)
        {
            ActivityType activityType1 = new ActivityType
            {
                Name = activityType.Name,
                NumberMonitors = activityType.NumberMonitors
            };
            var aniadirTipoActividad = _context.ActivityTypes.Add(activityType1);

            return activityType;
            
           

            
                
        }

        // Método para obtener todos los tipos de actividades por Nombre
        public ActivityTypeCreateDto? GetByName(string name)
        {

           var pasarNombre = _context.ActivityTypes.SingleOrDefault(n =>
            n.Name.Equals(name));
            ActivityTypeCreateDto activityType1 = new ActivityTypeCreateDto
            {
                Id = pasarNombre.Id,
                Name = pasarNombre.Name,
                NumberMonitors = pasarNombre.NumberMonitors
                
            };
            return activityType1;
           

        }

        public ActivityType? GetById(int? id)
        {
            var pasarId = _context.ActivityTypes.SingleOrDefault(i => 
            i.Equals(id));
            ActivityTypeCreateDto activityType1 = new ActivityTypeCreateDto
            {
                Id = pasarId.Id,
                Name = pasarId.Name,
                NumberMonitors = pasarId.NumberMonitors

            };
            return pasarId;
        }


    }

}
