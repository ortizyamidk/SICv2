using FontAwesome.Sharp;
using System.Threading;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

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
        private bool _isMainVisible = true;
        private string _userRole;


        //Properties
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

        public string UserRole
        {
            get { return _userRole; }
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }

        public bool IsMainVisible
        {
            get
            {
                return _isMainVisible;
            }

            set
            {
                _isMainVisible = value;
                OnPropertyChanged(nameof(IsMainVisible));
            }
        }

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
        //-----------COMANDOS USER PRINCIPAL---------------
        public ICommand ShowHomeViewCommand { get; }

        //Personal
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowPersonalNuevoViewCommand { get; }
        public ICommand ShowPersonalInfoViewCommand { get; }

        //Cursos
        public ICommand ShowCursosViewCommand { get; }
        public ICommand ShowCursoInfoViewCommand { get; }
        public ICommand ShowCursoNuevoViewCommand { get; }
        public ICommand ShowCursosTrabajadorViewCommand { get; }

        //Instructores
        public ICommand ShowInstructoresViewCommand { get; }
        public ICommand ShowInstructorInfoViewCommand { get; }
        public ICommand ShowInstructorNuevoViewCommand { get; }

        //Reportes
        public ICommand ShowReportesViewCommand { get; }



        //-----------COMANDOS USER GENERAL--------
        public ICommand ShowHomeGViewCommand { get; }

        public ICommand ShowCursoGViewCommand { get; }
        public ICommand ShowCursoInfoGViewCommand { get; }
        public ICommand ShowCursoNuevoGViewCommand { get; }

        //CONSTRUCTOR
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();


            //Initialize commands
            //--------------USER PRINCIPAL------------
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);

            //Personal
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowPersonalNuevoViewCommand = new ViewModelCommand(ExecuteShowPersonalNuevoViewCommand);
            ShowPersonalInfoViewCommand = new ViewModelCommand(ExecuteShowPersonalInfoViewCommand);

            //Cursos
            ShowCursosViewCommand = new ViewModelCommand(ExecuteShowCursosViewCommand);
            ShowCursoInfoViewCommand = new ViewModelCommand(ExecuteShowCursoInfoViewCommand);
            ShowCursoNuevoViewCommand = new ViewModelCommand(ExecuteShowCursoNuevoViewCommand);
            ShowCursosTrabajadorViewCommand = new ViewModelCommand(ExecuteShowCursosTrabajadorViewCommand);

            //Instructores
            ShowInstructoresViewCommand = new ViewModelCommand(ExecuteShowInstructoresViewCommand);
            ShowInstructorInfoViewCommand = new ViewModelCommand(ExecuteShowInstructorInfoViewCommand);
            ShowInstructorNuevoViewCommand = new ViewModelCommand(ExecuteShowInstructorNuevoViewCommand);

            //Reportes
            ShowReportesViewCommand = new ViewModelCommand(ExecuteShowReportesViewCommand);


            //----------USER GENERAL-------------
            ShowCursoGViewCommand = new ViewModelCommand(ExecuteShowCursoGViewCommand);
            ShowCursoInfoGViewCommand = new ViewModelCommand(ExecuteShowCursoInfoGViewCommand);
            ShowCursoNuevoGViewCommand = new ViewModelCommand(ExecuteShowCursoNuevoGViewCommand);

            ShowHomeGViewCommand = new ViewModelCommand(ExecuteShowHomeGViewCommand);

            LoadCurrentUserData();

        }

        //----------USER GENERAL----------
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
        private void ExecuteShowCursoInfoGViewCommand(object obj)
        {
            CurrentChildView = new CursoInfoGViewModel();
            Caption = "Cursos";
            Icon = IconChar.ChalkboardUser;
        }
        private void ExecuteShowCursoNuevoGViewCommand(object obj)
        {
            CurrentChildView = new CursoNuevoGViewModel();
            Caption = "Cursos";
            Icon = IconChar.ChalkboardUser;
        }

        //----------USER PRINCIPAL----------
        //Reportes
        private void ExecuteShowReportesViewCommand(object obj)
        {
            CurrentChildView = new ReportesViewModel();
            Caption = "Reportes";
            Icon = IconChar.UserGroup;
        }
        
        //Instructores
        private void ExecuteShowInstructoresViewCommand(object obj)
        {
            CurrentChildView = new InstructoresViewModel();
            Caption = "Instructores";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowInstructorInfoViewCommand(object obj)
        {
            CurrentChildView = new InstructorInfoViewModel();
        }
        private void ExecuteShowInstructorNuevoViewCommand(object obj)
        {
            CurrentChildView = new InstructorNuevoViewModel();
            Caption = "Instructores";
            Icon = IconChar.UserGroup;
        }

        //Cursos
        private void ExecuteShowCursosViewCommand(object obj)
        {
            CurrentChildView = new CursosViewModel();
            Caption = "Cursos";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowCursoInfoViewCommand(object obj)
        {
            CurrentChildView = new CursoInfoViewModel();
            Caption = "Cursos";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowCursoNuevoViewCommand(object obj)
        {
            CurrentChildView = new CursoNuevoViewModel();
            Caption = "Cursos";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowCursosTrabajadorViewCommand(object obj)
        {
            CurrentChildView = new CursosTrabajadorViewModel();
            Caption = "Cursos";
            Icon = IconChar.UserGroup;
        }

        //Personal
        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Personal";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowPersonalNuevoViewCommand(object obj)
        {
            CurrentChildView = new PersonalNuevoViewModel();
            Caption = "Personal";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowPersonalInfoViewCommand(object obj)
        {
            CurrentChildView = new PersonalInfoViewModel();
            Caption = "Personal";
            Icon = IconChar.UserGroup;
        }

        //Home
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
                CurrentUserAccount.DisplayArea = $"{user.Area}";

                UserRole = user.Rol;

                if (UserRole == "admin")
                {
                    // Usuario administrador
                    ExecuteShowHomeViewCommand(null);
                }
                else
                {
                    // Usuario general
                    ExecuteShowHomeGViewCommand(null);
                }
            }
            else
            {
                CurrentUserAccount.DisplayArea = "User not logged in";
                //Hide child views.
            }
        }       
    }
}
