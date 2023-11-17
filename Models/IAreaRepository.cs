using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IAreaRepository
    {
        IEnumerable<AreaModel> GetIdAreasRegistran();
        IEnumerable<AreaModel> GetAreaByDepartamento(string depto);

        AreaModel GetIdByName(string nomarea);
    }
}
