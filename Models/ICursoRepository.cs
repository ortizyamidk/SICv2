using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface ICursoRepository
    {
        IEnumerable<CursoModel> GetCursosNotRegistered(string area);
        IEnumerable<CursoModel> GetCursosVencidos(string area);

        IEnumerable<CursoModel> GetByAll();

        //reportes
        IEnumerable<CursoModel> GetCursosHistorialCursos(int numficha);
        CursoModel GetCursoListaAsistencia(string idcurso);
        CursoModel GetListaAsistenciaExcel(string idcurso);

        int GetCountCursosRegistered(string areadpto);
        int GetCountTotalCursos();

        void AddCursoInstructorTemporal(string id, string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, string instructor);
        void AddCursoInstructor(string id, string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, int idinstructor);
        void AddCursoArea(int idarea, string idcurso);

        void EditCursoITemporal(string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, string instructor, string id);
        void EditCurso(string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, int idinstructor, string id);

        CursoModel GetByName(string nomcurso);
        CursoModel GetById(string id);

        CursoGModel GetAsistenciaById(string idcurso);       
    }
}
