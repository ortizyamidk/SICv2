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
        InstructorModel GetIdByNombre(string nombreinstr);

        void AddInstructor(string nominstr, string rfc, string tipo, string compania);
        void EditInstructor(string nominstr, string rfc, string tipo, string compania, int id);
    }
}
