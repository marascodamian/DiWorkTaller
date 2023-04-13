using Microsoft.AspNetCore.Mvc;
using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Utils;

namespace TallerMecanicoDiWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoModule _vehiculoModule;

        public VehiculoController(IVehiculoModule vehiculoModule)
        {
            _vehiculoModule = vehiculoModule;
        }
        /// <summary>
        /// Método que obtiene todos los vehículos de la base de datos
        /// </summary>
        /// <returns>Lista con todos los objetos vehículo</returns>
        [HttpGet("GetAllVehiculos")]
        public async Task<IActionResult> GetVehiculos()
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _vehiculoModule.GetAllVehiculos() });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que solo trae los vehiculos del tipo automóvil
        /// </summary>
        /// <returns>Lista de los objetos automóvil</returns>
        [HttpGet("GetAllAutomoviles")]
        public async Task<IActionResult> GetAutomoviles()
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _vehiculoModule.GetAllAutomoviles() });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que solo trae los vehiculos del tipo moto
        /// </summary>
        /// <returns>Lista de los objetos moto</returns>
        [HttpGet("GetAllMotos")]
        public async Task<IActionResult> GetMotos()
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _vehiculoModule.GetAllMotos() });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que ingresa los datos del vehículo ingresante y los datos del cliente del presupuesto
        /// </summary>
        /// <param name="vehiculo">Datos del vehículo y del cliente</param>
        /// <returns></returns>
        [HttpPost("IngresarVehiculo")]
        public async Task<IActionResult> InsertarVehiculo([FromBody] VehiculoDto vehiculo)
        {
            try
            {
                await _vehiculoModule.InsertVehiculo(vehiculo);
                return Ok(new Utils.HttpResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
        /// <summary>
        /// Método que trae al Vehículo por su id
        /// </summary>
        /// <param name="id">Id del vehículo</param>
        /// <returns>un objeto automóvil si es automóvil o un objeto moto si es moto</returns>
        [HttpGet("GetVehiculoById/{id}", Name ="GetVehiculoById")]
        public async Task<IActionResult> GetVehiculoById(Int64 id)
        {
            try
            {
                return Ok(new Utils.HttpResponse { Data = await _vehiculoModule.GetVehiculoById(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpBadResponse(ex));
            }
        }
    }
}
