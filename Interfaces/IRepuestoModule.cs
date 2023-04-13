using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IRepuestoModule
    {
        Task<IEnumerable<RepuestoDao>> GetAllRepuestos();
    }
}
