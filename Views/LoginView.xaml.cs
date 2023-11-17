using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views
{
    public partial class LoginView : Window
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            InitializeComponent();
            txtUser.Focus();

            viewModel = new LoginViewModel();
            // Establecer el DataContext en la instancia de LoginViewModel
            this.DataContext = viewModel;

        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al comando LoginCommand.
                ((LoginViewModel)DataContext).LoginCommand.Execute(null);
            }
        }
    }
}
