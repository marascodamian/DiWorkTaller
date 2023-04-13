using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IRepuestoRepository
    {
        Task<IEnumerable<RepuestoDao>> GetAllRepuestos();
        Task<IEnumerable<RepuestoDao>> GetAllRepuestosByIdDesperfecto(long id);

    }
}
