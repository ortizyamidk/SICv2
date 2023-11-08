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

        //reportes
        IEnumerable<TrabajadorModel> GetPersonalCalificadoGral();
        IEnumerable<TrabajadorModel> GetPersonalCalificadoByArea(string nomarea);
        IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistencia(string idcurso);
        IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistenciaExcel(string idcurso);


        TrabajadorModel GetById(int numficha, string nomarea);
        TrabajadorModel GetIdByNumTarjeta(string numtarjeta);

        //reportes
        TrabajadorModel FormatoDC3(int numficha);
        TrabajadorModel GetTrabajadorHistorialCursos(int numficha);
        
    }
}
