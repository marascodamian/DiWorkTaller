using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.DtosDAO;

namespace TallerMecanicoDiWork.Utils
{
    public static class Extensions
    {
        public static AutomovilDao MapToAutomovilDao(VehiculoDao vehiculoDao)
        {
            return new AutomovilDao
            {
                Id = vehiculoDao.Id,
                Marca = vehiculoDao.Marca,  
                Modelo = vehiculoDao.Modelo,   
                Patente = vehiculoDao.Patente,  
                Cantidad_De_Puertas = vehiculoDao.Cantidad_De_Puertas,
                Tipo_Automovil = vehiculoDao.Tipo_Automovil
            };
                
        }
        public static MotoDao MapToMotoDao(VehiculoDao vehiculoDao)
        {
            return new MotoDao
            {
                Id = vehiculoDao.Id,
                Marca = vehiculoDao.Marca,
                Modelo = vehiculoDao.Modelo,
                Patente = vehiculoDao.Patente,
                Cilindrada = vehiculoDao.Cilindrada
            };

        }
    }
}
