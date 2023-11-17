using System.Collections.Generic;

namespace WPF_LoginForm.Models
{
    public interface IInstructorRepository
    {
        IEnumerable<InstructorModel> GetByAll();

        InstructorModel GetById(int id);
        InstructorModel GetIdByNombre(string nombreinstr);

        void AddInstructor(int id, string nominstr, string rfc, string tipo, string compania);
        void EditInstructor(string nominstr, string rfc, string tipo, string compania, int id);
    }
}
