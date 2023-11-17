using System.Collections.Generic;

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
