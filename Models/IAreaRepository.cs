using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IAreaRepository
    {
        IEnumerable<AreaModel> GetIdAreasRegistran();
        IEnumerable<AreaModel> GetAreaByDepartamento(string depto);

        AreaModel GetIdByName(string nomarea);
    }
}
