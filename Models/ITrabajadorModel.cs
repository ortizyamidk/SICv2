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
        IEnumerable<TrabajadorModel> GetParticipantesByIdAndArea(string idcurso, string area);

        TrabajadorModel GetById(int numficha, string nomarea);
        TrabajadorModel GetIdByNumTarjeta(string numtarjeta);

        TrabajadorModel FormatoDC3(int numficha);
        
    }
}
