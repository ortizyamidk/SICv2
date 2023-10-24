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
        AreaModel GetIdByName(string nomarea);
    }
}
