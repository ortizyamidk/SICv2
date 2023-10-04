﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static WPF_LoginForm.Views.CustomerView;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para InstructorNuevoView.xaml
    /// </summary>
    public partial class InstructorNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

       

        public InstructorNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;          
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombreI.Focus();
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;
            errComp.Content = string.Empty;

            txtNombreI.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;
            txtCompania.BorderBrush = bordeNormal;

            if (string.IsNullOrEmpty(txtNombreI.Text))
            {
                errNombre.Content = req;
                txtNombreI.BorderBrush = bordeError;
                errores = true;
            }

            if (string.IsNullOrEmpty(txtRFC.Text))
            {
                errRfc.Content = req;
                txtRFC.BorderBrush = bordeError;
                errores = true;
            }
            else if (txtRFC.Text.Length < 13)
            {
                errRfc.Content = "El RFC debe tener al menos 13 caracteres";
                txtRFC.BorderBrush = bordeError;
                errores = true;
            }

            if (string.IsNullOrEmpty(txtCompania.Text))
            {
                errComp.Content = req;
                txtCompania.BorderBrush = bordeError;
                errores = true;
            }

            if (!errores)
            {
                MostrarCustomMessageBox();
                limpiar();             
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        public void limpiar()
        {
            txtNombreI.Text = string.Empty;
            txtCompania.Text = string.Empty;
            txtRFC.Text = string.Empty;

            txtNombreI.Focus();
        }

        private void txtRFC_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validar que el carácter ingresado sea válido (letra o número).
            if (!char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            // Obtener el texto actual del TextBox.
            string currentText = txtRFC.Text;

            // Aplicar máscara de entrada.
            if (currentText.Length < 4)
            {
                // Permitir letras (primeras 4 letras).
                e.Handled = !char.IsLetter(e.Text, 0);
            }
            else if (currentText.Length <10)
            {
                // Permitir números (6 números).
                e.Handled = !char.IsDigit(e.Text, 0);
            }
        }

        
    }
}
