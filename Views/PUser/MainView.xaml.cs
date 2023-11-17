using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;


namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        private DispatcherTimer timer;
        public static MainView CurrentInstance { get; private set; }

        public MainView()
        {
            InitializeComponent();
            CurrentInstance = this;

            // Configura el temporizador para actualizar la fecha cada segundo
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            // Inicia el temporizador
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Obtiene la fecha y hora actual en la zona horaria local
            DateTimeOffset now = DateTimeOffset.Now;

            // Actualiza el contenido del Label con la fecha y hora actual
            fechaActual.Content = now.ToString("yyyy-MM-dd HH:mm:ss zzz");
        }



        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0); 
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal) 
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
