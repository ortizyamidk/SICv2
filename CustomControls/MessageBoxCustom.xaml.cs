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

namespace WPF_LoginForm.CustomControls
{
    public partial class MessageBoxCustom : Window
    {       
        public MessageBoxCustom()
        {
            InitializeComponent();
            this.Opacity = 0; // Establece la opacidad inicial en 0
        }

        // Este evento se dispara cuando el contenido de la ventana se ha renderizado.
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Crea un Storyboard para la animación de opacidad
            Storyboard fadeInStoryboard = new Storyboard();

            // Crea una animación de opacidad que dura 0.5 segundos.
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,         // Opacidad inicial
                To = 1,           // Opacidad final
                Duration = TimeSpan.FromSeconds(0.2)
            };

            // Agrega la animación al Storyboard
            fadeInStoryboard.Children.Add(fadeInAnimation);

            // Asocia la animación con la propiedad de opacidad de la ventana
            Storyboard.SetTarget(fadeInAnimation, this);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(Window.OpacityProperty));

            // Comienza la animación
            fadeInStoryboard.Begin();
        }


        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al manejador de eventos del botón btnSearch.
                Aceptar_Click(sender, e);
            }
        }
    }
}
