using HolaMundoApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HolaMundoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaludosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Saludar([FromBody] MensajeDto mensaje)
        {
            IdDto id = new IdDto { Fecha = DateTime.Now, Saludo = mensaje.Saludo };
            return Ok(id);
        }

        [HttpDelete]
        public IActionResult HacerError()
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Detail = "Ocurrio un error",
                Status = 500
            };
            return StatusCode(500, problemDetails);
        }
    }
}
