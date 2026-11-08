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
        var lista = _context.ActivityTypes.ToList();
            return lista.Select(l => new ActivityTypeCreateDto 
            { Id = l.Id, 
                Name = l.Name, 
                NumberMonitors = l.NumberMonitors}).ToList();
        }


        /// <summary>
        /// Guarda un nuevo tipo de actividad generando su ID de forma automática. (Autonumerico)
        /// </summary>
        /// <param name="activityType">Objeto con los datos a guardar (sin ID o con ID no asignado).</param>
        /// <returns>La entidad guardada con su ID autogenerado.</returns>
        public ActivityType Add(ActivityType activityType)
        {

            


            
                
        }

        // Método para obtener todos los tipos de actividades por Nombre
        public ActivityType? GetByName(string name)
        {
           var pasarNombre = _context.ActivityTypes.SingleOrDefault(n =>
            n.Name.Equals(name));
            return pasarNombre;

        }

        public ActivityType? GetById(int? id)
        {
            var pasarId = _context.ActivityTypes.SingleOrDefault(i => 
            i.Equals(id));
            return pasarId;
        }


    }

}
