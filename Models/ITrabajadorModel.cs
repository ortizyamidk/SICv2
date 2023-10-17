using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface ITrabajadorModel
    {
        IEnumerable<TrabajadorModel> GetByAll();
        TrabajadorModel GetById(int id);
        IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista);
    }
}
