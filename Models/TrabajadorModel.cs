using System;

namespace WPF_LoginForm.Models
{
    public class TrabajadorModel
    {
        public string Id { get; set; }
        public byte[] Foto { get; set; }
        public string NumTarjeta { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string FechaIng { get; set; }
        public int antiguedadanios { get; set; }
        public int antiguedadmeses { get; set; }
        public string Categoria { get; set; }
        public string Escolaridad { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Departamento { get; set; }
        public string Area { get; set; }       
        public string Puesto { get; set; }
        public bool Auditoriso14001 { get; set; }
        public bool PersCalif { get; set; }
        public string Antecedentes { get; set; }      
    }
}
