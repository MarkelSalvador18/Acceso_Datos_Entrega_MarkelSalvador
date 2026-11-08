using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _instructores;
        public InstructorRepository(GymDbContext context)
        {
            _instructores = context;
        }
       

        public List<Instructor> ObtenerInstructoresDeBaseDeDatos()
        {
            var listaInstructores = _instructores.Intructors.ToList();
            return listaInstructores;
        }
        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            if (string.IsNullOrWhiteSpace(firstInitial))
            {
                return ObtenerInstructoresDeBaseDeDatos();
            }
            var obtenerInicial = _instructores.Intructors.Where(i => i.Name.StartsWith(firstInitial)).ToList();

            return obtenerInicial;
        }

        public Instructor? GetById(int id)
        {
            return _instructores.Intructors.FirstOrDefault(i => i.Id == id);
        }
        
        public InstructorCreateDto? Add(InstructorCreateDto instructorNew, out ErrorResponse? errorResponse)
        {
            errorResponse = null;

            if (instructorNew == null)
            {
                errorResponse = new ErrorResponse(5, "No puede ser nulo");
                    return null;    
            }
            if (string.IsNullOrWhiteSpace(instructorNew.Name))
            {
                errorResponse = new ErrorResponse(6, "El nombre obligatorio");
                return null;
            }

            Instructor instructorNuevo = new Instructor
            {
                Name = instructorNew.Name,
                PhotoUrl = instructorNew.PhotoUrl,
                Email = instructorNew.Email,
                Phone = instructorNew.Phone

            };

            _instructores.Intructors.Add(instructorNuevo);
            instructorNew.Id = instructorNew.Id;
            return instructorNew;
        }


    }
}
