using Microsoft.AspNetCore.Mvc;
using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IDesperfectoModule
    {
        Task<IEnumerable<DesperfectoDao>> GetAllDesperfectos();
        Task InsertarDesperfecto(DesperfectoDto desperfectoDto);
        Task<IEnumerable<DesperfectoDao>> GetAllDesperfectosByIdPresupuesto(long id);
    }
}
