using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface ICursoGRepository
    {
        IEnumerable<CursoGModel> GetByAll(string nomarea);

        CursoGModel GetById(int idlista, string nomarea);

        void Edit(string idcurso, string numficha);
        void AddParticipantes(string numficha, string idcurso);
        void AddListaAsistencia(int idarea, string idcurso);

        int CursoImpartido(string nomcurso);
    }
}
