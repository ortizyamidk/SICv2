using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_LoginForm.CustomControls
{
    public partial class MessageBoxPaseLista : Window
    {

        private DispatcherTimer timer;

        public MessageBoxPaseLista()
        {
            InitializeComponent();
            this.Opacity = 0;
            Loaded += MainWindow_Loaded;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); // Configura el intervalo de 2 segundos
            timer.Tick += Timer_Tick;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtBuscar.Focus();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Crea un Storyboard para la animación de opacidad
            Storyboard fadeInStoryboard = new Storyboard();

            // Crea una animación de opacidad que dura 0.5 segundos.
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,         // Opacidad inicial
                To = 1,           // Opacidad final
                Duration = TimeSpan.FromSeconds(0.1)
            };

            // Agrega la animación al Storyboard
            fadeInStoryboard.Children.Add(fadeInAnimation);

            // Asocia la animación con la propiedad de opacidad de la ventana
            Storyboard.SetTarget(fadeInAnimation, this);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(Window.OpacityProperty));

            // Comienza la animación
            fadeInStoryboard.Begin();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Reinicia el temporizador cada vez que se cambia el texto
            timer.Stop();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Este código se ejecutará después de 2 segundos de inactividad en txtBuscar
            // Realiza aquí las acciones que deseas realizar después del retraso.

            // Detén el temporizador
            timer.Stop();

            // Ejecuta tu evento o código aquí
            // Por ejemplo, puedes lanzar un MessageBox como ejemplo:
            MessageBox.Show("Han pasado 2 segundos desde la última edición en txtBuscar");
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

    }
}
