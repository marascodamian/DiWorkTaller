using TallerMecanicoDiWork.Enums;

namespace TallerMecanicoDiWork.Models
{
    public class Automovil : Vehiculo
    {
        public Tipo_Automovil Tipo_Automovil { get; set; }
        public Int16 Cantidad_De_Puertas { get; set; }
    }
}
