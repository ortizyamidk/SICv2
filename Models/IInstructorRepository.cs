using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IInstructorRepository
    {
        IEnumerable<InstructorModel> GetByAll();
        InstructorModel GetById(int id);
    }
}
