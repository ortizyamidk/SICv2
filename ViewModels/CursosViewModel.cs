using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class CursosViewModel : ViewModelBase
    {

        private ViewModelBase _currentChildView;

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ICommand ShowCursoNuevoViewCommand { get; }

        public CursosViewModel()
        {
            ShowCursoNuevoViewCommand = new ViewModelCommand(ExecuteShowCursoNuevoViewCommand);
        }

        //navegar entre user controls
        private void ExecuteShowCursoNuevoViewCommand(object obj)
        {
            CurrentChildView = new CursoNuevoViewModel();
        }

    }
}
