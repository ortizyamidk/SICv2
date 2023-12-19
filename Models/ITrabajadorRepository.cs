using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface ITrabajadorRepository
    {
        IEnumerable<TrabajadorModel> GetByAll();
        IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista, string nomarea);

        IEnumerable<TrabajadorModel> GetParticipantesById(string idcurso);
        IEnumerable<TrabajadorModel> GetParticipantesByIdAndArea(string idcurso, string area);

        //reportes
        IEnumerable<TrabajadorModel> GetPersonalCalificadoGral();
        IEnumerable<TrabajadorModel> GetPersonalCalificadoByArea(string nomarea);
        IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistencia(string idcurso);
        IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistenciaExcel(string idcurso);


        TrabajadorModel GetById(string numficha, string nomarea);
        TrabajadorModel GetIdByNumTarjeta(string numtarjeta);
        TrabajadorModel GetTrabajador(string numficha);

        //reportes
        TrabajadorModel FormatoDC3(string numficha);
        TrabajadorModel GetTrabajadorHistorialCursos(string numficha);
        

        void AddTrabajador(string id, string numtarjeta, string nombre, DateTime fechaing, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea, byte[] certificaciones);
        void EditTrabajador(string numtarjeta, string nombre, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea, string activo, byte[] certificaciones, string id);

        void DeleteCertificaciones(string numficha);
    }
}
