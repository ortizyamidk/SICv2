namespace WPF_LoginForm.Models
{
    public class CursoModel
    {
        public string Id { get; set; }
        public string NomCurso { get; set; }
        public string AreaTematica { get; set; }
        public string Inicio { get; set; }
        public string Termino { get; set; }
        public string Horario { get; set; }
        public int Duracion { get; set; }
        public string Lugar { get; set; }

        public int idinstructor {  get; set; }
        public string Instructor { get; set; }
        public string rfcinstructor {  get; set; }

        public string MesLimite { get; set; }

        public string Impartido { get; set; } 
    }
}
