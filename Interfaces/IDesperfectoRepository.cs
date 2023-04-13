using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IDesperfectoRepository
    {
        Task<IEnumerable<DesperfectoDao>> GetAllDesperfectos();

        Task InsertDesperfecto(DesperfectoDto desperfecto);

        Task<IEnumerable<DesperfectoDao>> GetAllDesperfectosByIdPresupuesto(long id);
    }
}
