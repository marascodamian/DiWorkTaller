
namespace TallerMecanicoDiWork.DtosDAO
{
    public class DesperfectoDao
    {
        public Int64 Id { get; }
        public Int64 IdPresupuesto { get; set; }
        public String Descripcion { get; set; }
        public Decimal Mano_de_obra { get; set; }
        public Int16 Tiempo { get; set; }
        public IEnumerable<RepuestoDao>? Repuestos { get; set; }
    }
    public class DesperfectoDto
    {
        public Int64 IdPresupuesto { get; set; }
        public Int64 IdVehiculo { get; set; }
        public String Descripcion { get; set; }
        public Decimal Mano_De_Obra { get; set; }
        public Int16 Tiempo { get; set; }
        public IEnumerable<Int64> Repuestos { get; set; }
    }
}
