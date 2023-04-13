using System.ComponentModel.DataAnnotations;
using TallerMecanicoDiWork.DtosDAO;
using TallerMecanicoDiWork.Enums;

namespace TallerMecanicoDiWork.Dtos
{
    public class VehiculoDto
    {
        public Int64? Id { get; set; }
        [Required(ErrorMessage ="La marca es requerida")]
        public String Marca { get; set; }
        [Required(ErrorMessage = "El modelo es requerido")]
        public String Modelo { get; set; }
        [Required(ErrorMessage = "La patente es requerida")]
        public String Patente { get; set; }
        [Required(ErrorMessage = "El tipo de vehiculo es requerido")]
        public Tipo_Vehiculo Tipo_Vehiculo { get; set; }
        public Tipo_Automovil? Tipo_Automovil { get; set; }
        public Int16? Cantidad_De_Puertas { get; set; }
        public Int16? Cilindrada { get; set; }
        public PresupuestoDto? Presupuesto { get; set; }

    }
    public class VehiculoDao
    {
        public Int64 Id { get; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public String Patente { get; set; }
        public Tipo_Vehiculo Tipo_Vehiculo { get; set; }
        public String Tipo_Vehiculo_Descripcion { get; set; }
        public Tipo_Automovil Tipo_Automovil { get; set; }
        public String Tipo_Automovil_Descripcion { get; set; }
        public Int16 Cantidad_De_Puertas { get; set; }
        public Int16 Cilindrada { get; set; }
    }
}
