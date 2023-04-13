namespace TallerMecanicoDiWork.Models
{
    public class Presupuesto
    {
        public Int64 Id { get; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String EMail { get; set; }
        public IEnumerable<Desperfecto> Desperfectos { get; set; }
        public Double Total { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
