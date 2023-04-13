using System.Data;
using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Models;

namespace TallerMecanicoDiWork.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<VehiculoDao> GetVehiculoById(long id);
        Task<IEnumerable<VehiculoDao>> GetAllVehiculos();
        Task<IEnumerable<AutomovilDao>> GetAllAutomoviles();
        Task<IEnumerable<MotoDao>> GetAllMotos();
        Task InsertVehiculo(VehiculoDto vehiculo);
    }
}
