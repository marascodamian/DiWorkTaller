using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Interfaces;


namespace TallerMecanicoDiWork.Modules
{
    public class DesperfectoModule : IDesperfectoModule
    {
        private readonly IDesperfectoRepository _desperfectoRepository;
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly IRepuestoRepository _repuestoRepository;

        public DesperfectoModule(IDesperfectoRepository desperfectoRepository, IPresupuestoRepository presupuestoRepository, IRepuestoRepository repuestoRepository)
        {
            _desperfectoRepository = desperfectoRepository;
            _presupuestoRepository = presupuestoRepository;
            _repuestoRepository = repuestoRepository;
        }
        /// <summary>
        /// Método que devuelve un lista de los desperfectos que hay en base
        /// </summary>
        /// <returns>Lista de desperfectos</returns>
        public async Task<IEnumerable<DesperfectoDao>> GetAllDesperfectos()
        {
            return await _desperfectoRepository.GetAllDesperfectos();
        }
        /// <summary>
        /// Método que deuelve los desperfectos de un presupuesto en particular
        /// </summary>
        /// <param name="id">Id presupuesto</param>
        public async Task<IEnumerable<DesperfectoDao>> GetAllDesperfectosByIdPresupuesto(Int64 id)
        {
            IEnumerable<DesperfectoDao> desperfectos = await _desperfectoRepository.GetAllDesperfectosByIdPresupuesto(id);

            foreach(DesperfectoDao desperfecto in desperfectos)
            {
                desperfecto.Repuestos = await _repuestoRepository.GetAllRepuestosByIdDesperfecto(desperfecto.Id);
            }

            return desperfectos;
        }
        /// <summary>
        /// Método que inserta un desperfecto con sus respectivos repuestos
        /// </summary>
        /// <param name="desperfectoDto">Datos necesarios para el insert</param>
        /// <exception cref="Exception">Mensaje de exception</exception>
        public async Task InsertarDesperfecto(DesperfectoDto desperfectoDto)
        {
            if (this.ValidaDesperfecto(desperfectoDto))
            {
                PresupuestoDao preusupuesto = await _presupuestoRepository.GetPresupuestoById(desperfectoDto.IdPresupuesto);

                if(preusupuesto.Id_vehiculo != desperfectoDto.IdVehiculo)
                    throw new Exception("No coincide el presupuesto para ese vehículo, verifique");
                if (preusupuesto == null)
                    throw new Exception("No se encotró ningun presupesto asociado a este vehículo, por favor verifique");
                
                await _desperfectoRepository.InsertDesperfecto(desperfectoDto);

            }

        }
        /// <summary>
        /// Método que valida que los datos necesario no vengan nulos
        /// </summary>
        /// <param name="desperfectoDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Mensaje de exception</exception>
        public bool ValidaDesperfecto(DesperfectoDto desperfectoDto)
        {
            if (desperfectoDto.Descripcion == null || desperfectoDto.Mano_De_Obra == null 
                || desperfectoDto.Repuestos == null || desperfectoDto.IdVehiculo == null)
                throw new Exception("Debe ingresar el desperfecto con todos sus datos correspondientes, intente nuevamente");

            return true;
        }
    }
}
