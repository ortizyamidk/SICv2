using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public class CursoGModel
    {
        public int Id { get; set; }
        public string NomCurso { get; set; }
        public string AreaTematica { get; set; }

        public int IdLista {  get; set; }
        public string Inicia { get; set; }
        public string Termina { get; set; }
        public string Horario { get; set; }
        public int Duracion { get; set; }
        public string Lugar { get; set; }
        public string Instructor { get; set; }

        public int NumFicha { get; set; }
        public string NomTrabajador { get; set; }
        public string Puesto { get; set; }

    }
}
