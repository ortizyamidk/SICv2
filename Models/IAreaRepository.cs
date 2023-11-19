﻿using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IAreaRepository
    {
        IEnumerable<AreaModel> GetIdAreasRegistran();
        IEnumerable<AreaModel> GetAreaByDepartamento(string depto);
        IEnumerable<AreaModel> GetProgresoAreas();

        AreaModel GetIdByName(string nomarea);

        int GetAreasTerminadas();
        int GetTotalAreas();
    }
}
