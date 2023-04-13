using System.Data;
using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IPresupuestoRepository
    {
        Task<PresupuestoDao> GetPresupuestoById(long id);
        Task<PresupuestoDao> GetPresupuestoByIdVehiculo(long id);
        Task InsertPresupuesto(IDbConnection db, PresupuestoDto presupuesto, IDbTransaction transaction);
        Task UpdatePresupuestoTotal(IDbConnection db, long idPresupuesto, IDbTransaction transaction);
    }
}
