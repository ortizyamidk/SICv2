﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.CustomControls
{
    public partial class MessageBoxPaseLista : Window
    {

        private DispatcherTimer timer;
        CursoGRepository cursoGRepository;
        private string idCursoFromParent;

        public MessageBoxPaseLista(string idCurso)
        {
            InitializeComponent();

            cursoGRepository = new CursoGRepository();
            timer = new DispatcherTimer();
            
            Loaded += MainWindow_Loaded;           

            idCursoFromParent = idCurso;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtBuscar.Focus();

            this.Opacity = 0;           
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
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
            try
            {
                if (!string.IsNullOrEmpty(txtBuscar.Text))
                {
                    //PASE DE LISTA                   
                    string numtarjeta = txtBuscar.Text.Trim();

                    TrabajadorRepository trabajador = new TrabajadorRepository();
                    TrabajadorModel trabajadorModel = trabajador.GetIdByNumTarjeta(numtarjeta);

                    if (trabajadorModel != null)
                    {
                        string numficha = trabajadorModel.Id;

                        cursoGRepository.Edit(idCursoFromParent, numficha);
                    }
                    else
                    {
                        MessageBox.Show("No existe trabajador o no está inscrito al curso", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                    timer.Stop();

                    txtBuscar.Text = string.Empty;
                    txtBuscar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBuscar.Text = string.Empty;
                txtBuscar.Focus();
            }
        }
    }
}
