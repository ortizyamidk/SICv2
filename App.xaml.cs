using System;
using System.Windows;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;
using WPF_LoginForm.Views;
using WPF_LoginForm.Views.GUser;

namespace WPF_LoginForm
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {
            var loginViewModel = new LoginViewModel();
            var loginView = new LoginView(); 
            loginView.DataContext = loginViewModel;
            loginView.Show();


            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    string username = loginView.txtUser.Text;

                    var userRepository = new UserRepository();
                    var roles = userRepository.GetUserRoles(username);

                    if (roles.Contains("admin"))
                    {
                        var adminView = new MainView();
                        adminView.Show();                     
                    }
                    else if (roles.Contains("gral"))
                    {
                        var userView = new MainViewG();
                        userView.Show();
                    }

                    // Retrasa el cierre de la ventana del Login
                  loginView.Dispatcher.BeginInvoke(new Action(() =>{ loginView.Close(); }));
                }
            };
        }
    }
}
