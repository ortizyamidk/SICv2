using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface ITrabajadorModel
    {
        IEnumerable<TrabajadorModel> GetByAll();
        IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista);

        IEnumerable<TrabajadorModel> GetParticipantesById(string idcurso);

        TrabajadorModel GetById(int numficha, string nomarea);
        TrabajadorModel GetIdByNumTarjeta(string numtarjeta);
        
    }
}
