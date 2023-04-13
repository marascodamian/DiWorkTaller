using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Interfaces;

namespace TallerMecanicoDiWork.Modules
{
    public class RepuestoModule : IRepuestoModule
    {
        private readonly IRepuestoRepository _repuestoRepository;

        public RepuestoModule(IRepuestoRepository repuestoRepository)
        {
            _repuestoRepository = repuestoRepository;
        }
        /// <summary>
        /// Método que obtiene todos los repuestos
        /// </summary>
        /// <returns>Lista de repuestos</returns>
        public Task<IEnumerable<RepuestoDao>> GetAllRepuestos()
        {
            return _repuestoRepository.GetAllRepuestos();           
        }
    }
}
