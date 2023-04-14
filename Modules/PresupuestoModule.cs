using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Modules
{
    public class PresupuestoModule : IPresupuestoModule
    {
        private readonly IPresupuestoRepository _presupuestoRepository;
        private readonly IDesperfectoModule _desperfectoModule;

        public PresupuestoModule(IPresupuestoRepository presupuestoRepository,IDesperfectoModule desperfectoModule)
        {
            _presupuestoRepository = presupuestoRepository;
            _desperfectoModule = desperfectoModule;
        }
        /// <summary>
        /// Método que obtiene el presupuesto por su id
        /// </summary>
        /// <param name="id">Id del presupuesto</param>
        /// <returns>Presupuesto con todos sus datos</returns>
        public async Task<PresupuestoDao> GetPresupuestoById(Int64 id)
        {
            PresupuestoDao presupuesto = await _presupuestoRepository.GetPresupuestoById(id);

            presupuesto.Desperfectos = await _desperfectoModule.GetAllDesperfectosByIdPresupuesto(id);

            return presupuesto;   
        }
        /// <summary>
        /// Método que obtiene el presupuesto por el id del vehículo
        /// </summary>
        /// <param name="id">Id del vehículo</param>
        /// <returns>Presupuesto con todos sus datos</returns>
        public async Task<PresupuestoDao> GetPresupuestoByIdVehiculo(Int64 id)
        {
            PresupuestoDao presupuesto = await _presupuestoRepository.GetPresupuestoByIdVehiculo(id);

            if (presupuesto == null)
                throw new Exception("No existe presupuesto para ese id de vehículo, verificar");

            presupuesto.Desperfectos = await _desperfectoModule.GetAllDesperfectosByIdPresupuesto(presupuesto.Id);

            return presupuesto;
        }

    }
}
