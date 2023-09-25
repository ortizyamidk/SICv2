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

namespace WPF_LoginForm.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private bool _isLoggedIn = false;

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
        public ICommand RememberPasswordCommand { get; }

        public ICommand LogoutCommand { get; }

        //Constructor
        public LoginViewModel()
    {
        userRepository = new UserRepository();
        LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));

        LogoutCommand = new ViewModelCommand(p => Logout());
        }

    private bool CanExecuteLoginCommand(object obj)
    {
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
        var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
        if (isValidUser)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity(Username), null);
            IsViewVisible = false;
                IsLoggedIn = true;
            }
        else
        {
            ErrorMessage = "* Usuario o Contraseña inválido";
        }
    }

    private void ExecuteRecoverPassCommand(string username, string email)
    {
        throw new NotImplementedException();
    }

        public void Logout()
        {
            // Realiza la limpieza necesaria (puede incluir la eliminación de datos de la sesión)
            // ...

            IsLoggedIn = false; // Cierra la sesión del usuario
            Username = string.Empty; // Restablece el nombre de usuario
            Password = new SecureString(); // Restablece la contraseña
        }
    }
}
