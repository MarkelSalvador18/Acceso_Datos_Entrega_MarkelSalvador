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
       

        private List<Instructor> ObtenerInstructoresDeBaseDeDatos()
        {
            var listaInstructores = _instructores.Intructors.ToList();
            return listaInstructores;
        }
        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            if (firstInitial == null)
            {
                return null;
            }
            var obtenerInicial = _instructores.Intructors.Where(i => i.Name.StartsWith(firstInitial)).ToList();

            return obtenerInicial;
        }

        public IEnumerable<Instructor> GetAll()
        {
            var listaIntructores = _instructores.Intructors.ToList();
            return listaIntructores;
        }
        public Instructor? GetById(int id)
        {
            if (id == null)
            {
                return null;
            }
            var pasarId = _instructores.Intructors.SingleOrDefault(i =>
            i.Equals(id));
            return pasarId;
        }
        public List<Instructor> GetByIds(List<int> ids)
        {
            if (ids == null)
                return null;

            var devolverInsturctores = _instructores.Intructors.Where(i => i.Id.Equals(ids)).ToList();

            return devolverInsturctores;
        }
        public Instructor? Add(Instructor instructorNew, out ErrorResponse? errorResponse)
        {
            errorResponse = null;

            bool existeEmail = _instructores.Intructors.Any(i => 
            i.Email.ToLower().Equals(instructorNew.Email.ToLower()));

            bool existeNombre = _instructores.Intructors.Any(e => 
            e.Name.ToLower().Equals(instructorNew.Name.ToLower()));

            // Comprobación de duplicados (Email o Nombre existente, ignorando mayúsculas/minúsculas)

            if (existeEmail || existeNombre)
            {
                errorResponse = new ErrorResponse(34, "An instructor with the same email or name already exists");
                return null;
            }
            int nuevoId = (_instructores.Intructors.Select(i => (int?)i.Id).Max() ?? 0) + 1;
            Instructor instructor = new Instructor(nuevoId, instructorNew.Name, instructorNew.Email, instructorNew.PhotoUrl, instructorNew.Phone);
            _instructores.Add(nuevoId, instructor);

           

            return instructor;
        }


    }
}
