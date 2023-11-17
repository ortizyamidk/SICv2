using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IPuestoRepository
    {
        PuestoModel GetIdByNombrePuesto(string nompuesto);

        IEnumerable<PuestoModel> GetByAll();
    }
}
