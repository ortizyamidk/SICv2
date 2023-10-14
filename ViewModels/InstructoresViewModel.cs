using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class InstructoresViewModel : ViewModelBase
    {
        private InstructorSeleccionadoModel _instructorSeleccionado;
        private IInstructorRepository instructorRepository;


        public InstructorSeleccionadoModel CurrentInstructor
        {
            get
            {
                return _instructorSeleccionado;
            }

            set
            {
                _instructorSeleccionado = value;
                OnPropertyChanged(nameof(CurrentInstructor));
            }
        }

        //COMMANDS
        public ICommand ShowInstructorInfoViewCommand { get; }

        public InstructoresViewModel()
        {
            CurrentInstructor = new InstructorSeleccionadoModel();
            instructorRepository = new InstructorRepository();

            LoadCurrentInstructorData();

        }

        

        private void LoadCurrentInstructorData()
        {
            var instructor = instructorRepository.GetById(2);
            if (instructor != null)
            {
                CurrentInstructor.IdS = instructor.Id;
                CurrentInstructor.NomInstrS = instructor.NomInstr;
                CurrentInstructor.TipoInstrS = instructor.TipoInstr;
                CurrentInstructor.RFCS = instructor.RFC;
                CurrentInstructor.NomCiaS = instructor.NomCia;
            }
        }

       

    }
}
