namespace WPF_LoginForm.Models
{
    public class AreaModel
    {
        public int Id { get; set; }
        public string NombreArea { get; set; }

        public int CursosARegistrar { get; set; }
        public int CursosRegistrados { get; set; }
        public int PorcentajeAvance { get; set; }
        public int ValorPorcentaje { get; set; }
    }
}
