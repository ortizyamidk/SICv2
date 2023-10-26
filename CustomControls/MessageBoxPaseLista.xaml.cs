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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views.GUser;

namespace WPF_LoginForm.CustomControls
{
    public partial class MessageBoxPaseLista : Window
    {

        private DispatcherTimer timer;
        CursoGRepository repository;
        CursoRepository cursoRepository;

        public MessageBoxPaseLista()
        {
            InitializeComponent();
            repository = new CursoGRepository();
            cursoRepository = new CursoRepository();

            this.Opacity = 0;
            Loaded += MainWindow_Loaded;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Configura el intervalo de n segundos
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

            // Verifica si el texto en txtBuscar no está vacío
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                //PASE DE LISTA
                string numtarjeta = txtBuscar.Text;

                TrabajadorRepository trabajador = new TrabajadorRepository();
                TrabajadorModel trabajadorModel = trabajador.GetIdByNumTarjeta(numtarjeta);

                int numficha = trabajadorModel.Id;

                //guardar nombre del curso de la ventana CursoInfoGView y traerlo
                CursoModel cursoModel = cursoRepository.GetIdByName("Curso 1");

                repository.Edit(cursoModel.Id, numficha); //traerme idcurso de CursoInfoView

                // Detén el temporizador
                timer.Stop();

                txtBuscar.Text = string.Empty;
                txtBuscar.Focus();
            }
        }

    }
}
