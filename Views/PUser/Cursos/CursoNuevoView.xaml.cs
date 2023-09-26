﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_LoginForm.CustomControls;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CursoNuevoView.xaml
    /// </summary>
    public partial class CursoNuevoView : UserControl
    {
        public CursoNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombreC.Focus();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
            txtDescripcion.Text = "¡Registro guardado correctamente!";
            btnA.Content = "Aceptar";
        }
    }
}
