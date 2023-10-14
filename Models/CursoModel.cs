using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string NomCurso { get; set; }
        public string AreaTematica { get; set; }
        public bool Registrado { get; set; }
        public int IdInstructor { get; set; }
    }
}
