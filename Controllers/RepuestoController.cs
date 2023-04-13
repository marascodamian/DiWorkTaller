using Microsoft.AspNetCore.Mvc;
using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Utils;

namespace TallerMecanicoDiWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoController : ControllerBase
    {
        private readonly IRepuestoModule _repuestoModule;

        public RepuestoController(IRepuestoModule repuestoModule)
        {
            _repuestoModule = repuestoModule;
        }
        /// <summary>
        /// Métoso que recupera todos los repuestos de la tabla repuestos
        /// </summary>
        /// <returns>Lista con todos los objetos repuesto</returns>
        [HttpGet("GetAllRepuestos")]
        public async Task<IActionResult> GetRepuestos()
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _repuestoModule.GetAllRepuestos() });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
    }
}
