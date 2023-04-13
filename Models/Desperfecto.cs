namespace TallerMecanicoDiWork.Models
{
    public class Desperfecto
    {
        public Int64 Id { get; }
        public Int64 IdPresupuesto { get; set; }
        public String Descripcion { get; set; }
        public Double Mano_De_Obra { get; set; }
        public Int16 Tiempo { get; set; }
        public IEnumerable<Repuesto> Repuestos { get; set; }
        
    }
}
