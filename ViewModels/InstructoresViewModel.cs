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
