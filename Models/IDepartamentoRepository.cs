using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IDepartamentoRepository
    {
        IEnumerable<DepartamentoModel> GetByAll();

        DepartamentoModel GetJefeByDepartamento(string depto);
    }
}
