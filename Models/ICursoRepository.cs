using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface ICursoRepository
    {
        IEnumerable<CursoModel> GetCursosNotRegistered(string area);
        IEnumerable<CursoModel> GetByAll();

        int GetCountCursosRegistered(string area);
        int GetCountTotalCursos(string area);

        void AddCurso(string nomcurso, string areatem, string meslim);
        void EditCurso(string nomcurso, string areatem, string meslim, int idcurso);
        CursoModel GetById(int id);

        CursoGModel GetAsistenciaById(int id);
        IEnumerable<CursoGModel> GetParticipantes(int id);
    }
}
