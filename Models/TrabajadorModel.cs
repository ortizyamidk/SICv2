using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public class TrabajadorModel
    {
        public int Id { get; set; }
        public string NumTarjeta { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public string Puesto { get; set; }

        public string Departamento { get; set; }

    }
}
