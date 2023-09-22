using FontAwesome.Sharp;
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
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        //Properties
        public ViewModelBase CurrentChildView {
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

        public string Caption {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //-->Commands
        //COMANDOS USER PRINCIPAL
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowCursosViewCommand { get; }
        public ICommand ShowInstructoresViewCommand { get; }
        public ICommand ShowReportesViewCommand { get; }

        //COMANDOS USER GENERAL
        public ICommand ShowHomeGViewCommand { get; }
        public ICommand ShowCursoGViewCommand { get; }


        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            //USER PRINCIPAL
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowCursosViewCommand = new ViewModelCommand(ExecuteShowCursosViewCommand);
            ShowInstructoresViewCommand = new ViewModelCommand(ExecuteShowInstructoresViewCommand);
            ShowReportesViewCommand = new ViewModelCommand(ExecuteShowReportesViewCommand);


            //USER GENERAL
            ShowCursoGViewCommand = new ViewModelCommand(ExecuteShowCursoGViewCommand);
            ShowHomeGViewCommand = new ViewModelCommand(ExecuteShowHomeGViewCommand);

            //Default view (HACER CONDICIONAL CONFORME A ROLES)
            //ExecuteShowHomeViewCommand(null); //User principal
            ExecuteShowHomeGViewCommand(null); //User general

            LoadCurrentUserData();
        }

        //USER GENERAL
        private void ExecuteShowHomeGViewCommand(object obj)
        {
            CurrentChildView = new HomeGViewModel();
            Caption = "Inicio";
            Icon = IconChar.Home;
        }

        private void ExecuteShowCursoGViewCommand(object obj)
        {
            CurrentChildView = new CursoGViewModel();
            Caption = "Cursos";
            Icon = IconChar.ChalkboardUser;
        }

        //USER PRINCIPAL
        private void ExecuteShowReportesViewCommand(object obj)
        {
            CurrentChildView = new ReportesViewModel();
            Caption = "Reportes";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowInstructoresViewCommand(object obj)
        {
            CurrentChildView = new InstructoresViewModel();
            Caption = "Instructores";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowCursosViewCommand(object obj)
        {
            CurrentChildView = new CursosViewModel();
            Caption = "Cursos";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Personal";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Inicio";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "User not logged in";
                //Hide child views.
            }
        }
    }
}
