using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class CursoGViewModel : ViewModelBase
    {
        private IUserRepository userRepository;
        CursoGRepository cursoRepository;

        private UserAccountModel _currentUserAccount;
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


        public CursoGViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            cursoRepository = new CursoGRepository();

            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            CurrentUserAccount.DisplayArea = $"{user.Area}";
        }
    }
}
