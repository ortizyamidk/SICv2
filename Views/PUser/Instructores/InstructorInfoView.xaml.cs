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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para InstructorInfoView.xaml
    /// </summary>
    public partial class InstructorInfoView : UserControl
    {
        public InstructorInfoView()
        {
            InitializeComponent();

            deshabilitar();
        }

        public void habilitar()
        {
            txtNombreI.IsEnabled = true;
            txtRFC.IsEnabled = true;
            cbTipo.IsEnabled = true;
            txtCompania.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

            txtNombreI.Focus();
        }

        public void deshabilitar()
        {
            txtNoInst.IsEnabled = false;
            txtNombreI.IsEnabled = false;
            txtRFC.IsEnabled = false;
            cbTipo.IsEnabled = false;
            txtCompania.IsEnabled = false;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            habilitar();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
            txtDescripcion.Text = "¡Registro editado correctamente!";
            btnA.Content = "Aceptar";
            deshabilitar();
        }

        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            if (!IsLetter(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsLetter(string text)
        {
            return text.All(char.IsLetter);
        }
    }
}
