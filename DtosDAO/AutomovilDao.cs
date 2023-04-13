using TallerMecanicoDiWork.Enums;

namespace TallerMecanicoDiWork.Dtos
{
    public class AutomovilDao
    {
        public Int64 Id { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public String Patente { get; set; }
        public Tipo_Automovil Tipo_Automovil { get; set; }
        public Int16 Cantidad_De_Puertas { get; set; }
    }
}
