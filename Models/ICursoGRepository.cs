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

        void Edit(string idcurso, int numficha);
        void AddParticipantes(int numficha, string idcurso);
        void AddListaAsistencia(int idarea, string idcurso);

        int CursoImpartido(string nomcurso);
    }
}
