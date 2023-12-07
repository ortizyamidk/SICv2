using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IAreaRepository
    {
        IEnumerable<AreaModel> GetIdAreasRegistran();
        IEnumerable<AreaModel> GetAreaByDepartamento(string depto);
        IEnumerable<AreaModel> GetProgresoAreas();
        IEnumerable<AreaModel> GetByAll();
        IEnumerable<AreaModel> GetIdsAreasByName(string nomarea);

        AreaModel GetIdByName(string nomarea);

        int GetAreasTerminadas();
        int GetTotalAreas();
    }
}
