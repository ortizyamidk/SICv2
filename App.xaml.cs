using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Views;
using WPF_LoginForm.Views.GUser;

namespace WPF_LoginForm
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, EventArgs e)
        {

            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    string username = loginView.txtUser.Text; // Obtén el valor de txtUser desde la vista de inicio de sesión

                    // Verifica el valor de username y abre la ventana correspondiente
                    if (username == "admin")
                    {
                        var adminView = new MainView();
                        adminView.Show();
                    }
                    else if (username == "gral")
                    {
                        var userView = new MainViewG();
                        userView.Show();
                    }

                    loginView.Close();

                }
            };
        }
    }
}
