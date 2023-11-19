using System.Threading;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class CursoInfoGViewModel : ViewModelBase
    {
        private IUserRepository userRepository;

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

        public CursoInfoGViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            CurrentUserAccount.DisplayArea = $"{user.Area}";
        }
    }
}
