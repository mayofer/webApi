using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Becas.service;
using Becas.Models;
using Microsoft.EntityFrameworkCore;

namespace Becas.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AlumnoController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AlumnoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Alumno>>> Get(){
            return await context.Alumnos.ToListAsync();
        }
        
      [HttpGet("{Id}")]
      public async Task<IActionResult> Get(int Id)
      {
            var alumno = await context.Alumnos.FindAsync(Id);
            if (alumno == null)
            {
              return NotFound();
            }
            else
            {
                return Ok(alumno);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Alumno alumno){
            context.Add(alumno);
            await context.SaveChangesAsync();
            return Ok();
        }
       
        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(int Id, Alumno alumno)
        {
            var alumnoExiste = await AlumnoExiste(Id);
            if (!alumnoExiste)
            {
                return NotFound();
            }

            context.Update(alumno);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var alumnoExiste = await AlumnoExiste(Id);
            if (!alumnoExiste)
            {
                return NotFound();
            }

            context.Remove(new Alumno() { Id = Id});
            await context.SaveChangesAsync();
            return NotFound();
        }
        private async Task<bool> AlumnoExiste(int Id)
        {
            return await context.Alumnos.AnyAsync(p => p.Id == Id);
        }
        


    }

}