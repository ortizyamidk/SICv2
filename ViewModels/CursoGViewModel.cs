using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.ViewModels
{
    public class CursoGViewModel : ViewModelBase
    {
        private string _numValue;

        public string NumValue
        {
            get { return _numValue; }
            set
            {
                if (_numValue != value)
                {
                    _numValue = value;
                    OnPropertyChanged(nameof(NumValue));
                }
            }
        }


    }
}
