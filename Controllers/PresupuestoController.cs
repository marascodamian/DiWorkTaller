using Microsoft.AspNetCore.Mvc;
using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Utils;

namespace TallerMecanicoDiWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoModule _presupuestoModule;

        public PresupuestoController(IPresupuestoModule preuspuestoModule)
        {
            _presupuestoModule = preuspuestoModule;
        }
        /// <summary>
        /// Método que obtiene el presupuesto por el id del mismo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto con datos del presupuesto(Vehículo, desperfectos, repuestos)</returns>
        [HttpGet("ById/{id}", Name = "GetPresupuestoById")]
        public async Task<IActionResult> GetPresusupuestoById(Int64 id)
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _presupuestoModule.GetPresupuestoById(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que obtiene el presupuesto por el id del vehículo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto con datos del presupuesto(Vehículo, desperfectos, repuestos)</returns>
        [HttpGet("ByIdVehiculo/{id}", Name = "GetPresupuestoByIdVehiculo")]
        public async Task<IActionResult> GetPresupuestoByIdVehiculo(Int64 id)
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _presupuestoModule.GetPresupuestoByIdVehiculo(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
    }
}
