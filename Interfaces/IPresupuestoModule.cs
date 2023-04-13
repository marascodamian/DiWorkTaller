using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IPresupuestoModule
    {
        Task<PresupuestoDao> GetPresupuestoById(long id);
        Task<PresupuestoDao> GetPresupuestoByIdVehiculo(long id);
    }
}
