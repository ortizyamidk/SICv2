using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IDepartamentoRepository
    {
        IEnumerable<DepartamentoModel> GetDepartamentos();
        IEnumerable<DepartamentoModel> GetByAll();

        int GetAreasTerminadas();
        int GetTotalAreas();

        DepartamentoModel GetJefeByDepartamento(string depto);
    }
}
