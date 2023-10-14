using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;
using System.Windows;
using WPF_LoginForm.Views.GUser;

namespace WPF_LoginForm.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private bool _isLoggedIn=false;

        private IUserRepository userRepository;



        //Properties
        public string Username
    {
        get
        {
            return _username;
        }

        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }

            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand LogoutCommand { get; }

        //public ICommand RememberPasswordCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository(); //trae la conexion a bd
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);

            //RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            //solo valida los campos llenados
            bool validData;

            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;

            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password)); //metodo de autenticacion

            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null); //esto indica el usuario actual autenticado
                IsViewVisible = false; //esconder vista login
                IsLoggedIn = true; //loggeado
            }
            else
            {
                ErrorMessage = "* Usuario o Contraseña inválido";           
            }
        }       

        private void ExecuteLogoutCommand(object obj)
        {
           
            IsViewVisible = true;
            IsLoggedIn = false;
            Username = null;
            Password = null;
            ErrorMessage = null;

            //MessageBox.Show("EXECUTE LOGOUT Login visible: " + IsViewVisible.ToString());
            //MessageBox.Show("EXECUTE LOGOUT Usuario loggeado: " + IsLoggedIn.ToString());

            Thread.CurrentPrincipal=null;
            //MessageBox.Show("user loggeado: " + Thread.CurrentPrincipal.Identity.Name);           
        }

        /*private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }*/
    }
}
