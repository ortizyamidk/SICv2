using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface ICursoGRepository
    {

        IEnumerable<CursoGModel> GetByAll(string nomarea);

        CursoGModel GetById(int id);

        IEnumerable<CursoGModel> GetCursos(string area);

        CursoGModel GetIDCursoByNombre(string nombrecurso);


        void Edit(string idcurso, int numficha);
        void AddInstructor(int idinstructor, int idcurso);
        void AddInstructorTemporal(string nominstr, int idcurso);
        void AddListaAsistencia (string inicia, string termina, string horario, int duracion, string lugar, int curso);

        void IsCursoRegistered(int idcurso);

        void AddParticipantes(int numficha, string idcurso);

        void AddListaAsistencia(int idarea, string idcurso);

        int CursoImpartido(string nomcurso);
    }
}
