namespace TallerMecanicoDiWork.Models
{
    public abstract class Vehiculo
    {
        public Int64 Id { get; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public String Patente { get; set; }
    }
}
