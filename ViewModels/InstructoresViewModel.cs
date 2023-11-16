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
        private int _idParametro;

        public int IdParametro
        {
            get
            {
                return _idParametro;
            }

            set
            {
                _idParametro = value;
                OnPropertyChanged(nameof(IdParametro));
            }
        }

        
    }
}
