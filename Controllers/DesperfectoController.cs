using Microsoft.AspNetCore.Mvc;
using TallerMecanicoDiWork.Utils;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesperfectoController : ControllerBase
    {
        private readonly IDesperfectoModule _desperfectoModule;

        public DesperfectoController(IDesperfectoModule desperfectoModule)
        {
            _desperfectoModule = desperfectoModule;
        }
        /// <summary>
        /// Método que obtiene todos los desperfectos
        /// </summary>
        /// <returns>Una lista con los objetos desperfecto</returns>
        [HttpGet("GetAllDesperfectos")]
        public async Task<IActionResult> GetDesperfectos()
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _desperfectoModule.GetAllDesperfectos() });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que inserta un desperfecto con sus repuestos
        /// </summary>
        /// <param name="desperfecto">Datos necesario para el insert</param>
        /// <returns>Status</returns>
        [HttpPost("IngresarDesperfecto")]
        public async Task<IActionResult> InsertarDesperfecto([FromBody] DesperfectoDto desperfecto)
        {
            try
            {
                await _desperfectoModule.InsertarDesperfecto(desperfecto);
                return Ok(new Utils.HttpResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
    }
}
