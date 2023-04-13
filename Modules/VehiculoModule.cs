using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Enums;
using TallerMecanicoDiWork.Interfaces;
using TallerMecanicoDiWork.Utils;

namespace TallerMecanicoDiWork.Modules
{
    public class VehiculoModule : IVehiculoModule
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoModule(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }
        /// <summary>
        /// Método que obtiene todos los vehículos de la base de datos
        /// </summary>
        /// <returns>Lista de vehículos</returns>
        public async Task<IEnumerable<VehiculoDao>> GetAllVehiculos()
        {
            return await _vehiculoRepository.GetAllVehiculos();
        }
        /// <summary>
        /// Método que obtiene el vehículo por id
        /// </summary>
        /// <param name="id">Id del vehículo</param>
        /// <returns>objeto automóvil o moto segun corresponda</returns>
        /// <exception cref="Exception">Mensaje de exception</exception>
        public async Task<Object> GetVehiculoById(long id)
        {
            VehiculoDao vehiculoDao = await _vehiculoRepository.GetVehiculoById(id);
            if (vehiculoDao == null)
                throw new Exception("El vehiculo con ese Id no se encuentra o es inexistente");

            switch (vehiculoDao.Tipo_Vehiculo)
            {
                case Tipo_Vehiculo.Automovil:
                        AutomovilDao automovil = Extensions.MapToAutomovilDao(vehiculoDao);
                        return automovil;
                case Tipo_Vehiculo.Moto:
                        MotoDao moto = Extensions.MapToMotoDao(vehiculoDao);
                        return moto;
                default:
                    throw new Exception($"Tipo de vehículo con id {id} no declarado en la base");
            }

        }
        /// <summary>
        /// Método que obtiene solo los automóviles
        /// </summary>
        /// <returns>Lista de objetos automóvil</returns>
        public async Task<IEnumerable<AutomovilDao>> GetAllAutomoviles()
        {
            return await _vehiculoRepository.GetAllAutomoviles();
        }
        /// <summary>
        /// Método que obtiene solo los motos
        /// </summary>
        /// <returns>Lista de objetos moto</returns>
        public Task<IEnumerable<MotoDao>> GetAllMotos()
        {
            return _vehiculoRepository.GetAllMotos();
        }
        /// <summary>
        /// Método que inserta un vehículo en la base y los datos del presupuesto
        /// </summary>
        /// <param name="vehiculo"></param>
        public async Task InsertVehiculo(VehiculoDto vehiculo)
        {

            if (this.ValidaVehiculo(vehiculo))
                await _vehiculoRepository.InsertVehiculo(vehiculo);

        }
        /// <summary>
        /// Método que valida que los datos proporcionados sean correctos para su inserción
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns>true o false según corresponda</returns>
        /// <exception cref="Exception">Mensaje exception</exception>
        public bool ValidaVehiculo(VehiculoDto vehiculo)
        {
            switch (vehiculo.Tipo_Vehiculo)
            {
                case Tipo_Vehiculo.Automovil:
                    if (vehiculo.Cantidad_De_Puertas == null && vehiculo.Tipo_Automovil == null)
                        throw new Exception("El vehículo que trata de ingresar es un automovil pero no ingreso la cantidad de puertas o el tipo de automovil");
                    if (vehiculo.Cilindrada != null)
                        throw new Exception("El vehiculo esta definido como automovil pero se le ingreso cilindrada");
                    break;
                case Tipo_Vehiculo.Moto:
                    if (vehiculo.Cilindrada == null)
                        throw new Exception("El vehículo que trata de ingresar es una moto pero no ingreso la cilindrada");
                    if (vehiculo.Cantidad_De_Puertas != null || vehiculo.Tipo_Automovil != null)
                        throw new Exception("El vehiculo esta definido como moto pero se le ingreso cantidad de puertas o el tipo de automovil");
                    break;
                default: throw new Exception("El tipo de vehículo ingresado no esta contemplado");
            }

            return true;
        }
    }
}
