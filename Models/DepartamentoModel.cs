using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public class DepartamentoModel
    {
        public int Id { get; set; }
        public string NomDepto  { get; set; }
        public int DeptoCorto { get; set; }
        public int IdTrabajador { get; set; }

        public int CursosARegistrar { get; set; }
        public int CursosRegistrados {  get; set; }
        public int PorcentajeAvance {  get; set; }
        public int ValorPorcentaje {  get; set; }
    }
}
