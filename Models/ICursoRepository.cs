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

        int GetCountCursosRegistered(string area);

        int GetCountTotalCursos(string area);
    }
}
