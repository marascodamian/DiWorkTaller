using TallerMecanicoDiWork.Dtos;
using TallerMecanicoDiWork.Enums;
using TallerMecanicoDiWork.Models;

namespace TallerMecanicoDiWork.DtosDAO
{
    public class PresupuestoDao
    {
        public Int64 Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String EMail { get; set; }
        public IEnumerable<DesperfectoDao>? Desperfectos { get; set; }
        public Decimal Total { get; set; }
        public Int64 Id_vehiculo { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public String Patente { get; set; }
        public Tipo_Vehiculo Tipo_Vehiculo { get; set; }
        public Tipo_Automovil Tipo_Automovil { get; set; }
        public Int16 Cantidad_De_Puertas { get; set; }
        public Int16 Cilindrada { get; set; }
    }
    public class PresupuestoDto
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String EMail { get; set; }
        public Decimal? Total { get; set; } 
        public Int64? Id_vehiculo { get; set; }
    }
}
