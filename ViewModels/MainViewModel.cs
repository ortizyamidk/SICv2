using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;
using WPF_LoginForm.Views.GUser;

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
        private LoginViewModel _loginViewModel;


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

        public LoginViewModel LoginViewModel
        {
            get { return _loginViewModel; }
            set
            {
                _loginViewModel = value;
                OnPropertyChanged(nameof(LoginViewModel));
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

        //CERRAR SESION
        public ICommand CerrarSesionCommand { get; }

        //CONSTRUCTOR
        public MainViewModel()
        {

            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            LoginViewModel = new LoginViewModel();


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


            //CERRAR SESION
            CerrarSesionCommand = new ViewModelCommand(ExecuteCerrarSesionCommand);
            

            LoadCurrentUserData();
        }

        public MainViewModel(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        //----------USER GENERAL----------
        private void ExecuteShowHomeGViewCommand(object obj)
        {
            CurrentChildView = new HomeGViewModel();
            Caption = "Inicio";
            Icon = IconChar.Home;

            LoginViewModel.IsViewVisible = false;
            LoginViewModel.IsLoggedIn = true;

            //MessageBox.Show("EXECUTE HOME Login visible: "+LoginViewModel.IsViewVisible.ToString());
            //MessageBox.Show("EXECUTE HOME Usuario loggeado: " + LoginViewModel.IsLoggedIn.ToString());
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


        //CERRAR SESION
        private void ExecuteCerrarSesionCommand(object obj)
        {

            // Llamar al comando de LogoutCommand en el LoginViewModel
            LoginViewModel.LogoutCommand.Execute(null);

            MessageBox.Show("COMANDO SECUNDARIO Login Visible: "+LoginViewModel.IsViewVisible.ToString());
            MessageBox.Show("COMANDO SECUNDARIO Usuario loggeado: " + LoginViewModel.IsLoggedIn.ToString());

            CurrentChildView = null;
            MessageBox.Show("COMANDO SECUNDARIO CurrentChildView: " + CurrentChildView);

            Caption = null;
            MessageBox.Show("COMANDO SECUNDARIO Caption: " + Caption);

            Icon = 0;
            MessageBox.Show("COMANDO SECUNDARIO Icon: " + Icon);

            UserRole = null;
            MessageBox.Show("COMANDO SECUNDARIO UserRole: " + UserRole);

            IsMainVisible = false;
            MessageBox.Show("COMANDO SECUNDARIO IsMainVisible: " + IsMainVisible);

            CurrentUserAccount.DisplayName = null;
            MessageBox.Show("COMANDO SECUNDARIO DisplayName: " + CurrentUserAccount.DisplayName);            

            var loginView = new LoginView();
            loginView.Show();

            //MessageBox.Show("COMANDO SECUNDARIO CurrentInstance: " + MainViewG.CurrentInstance);
            MainViewG.CurrentInstance.Close();

            

            /*var mainViewG = new MainViewG();
            mainViewG.UserLoggeado.Text=string.Empty;
            MessageBox.Show("COMANDO SECUNDARIO mainViewG: " + mainViewG.UserLoggeado.Text);*/

            
        }


        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.TrabajadorNombre}";

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
                CurrentUserAccount.DisplayName = "User not logged in";
                //Hide child views.
            }
        }
       
    }
}
