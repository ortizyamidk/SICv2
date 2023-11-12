using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WPF_LoginForm.Views
{

    public partial class PersonalNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";


        public PersonalNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            txtJefe.IsEnabled = false;

            dtIngreso.SelectedDate = DateTime.Now;

           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNoFicha.Focus();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;

            txtNoFicha.BorderBrush = bordeNormal;
            txtNombre.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;

            if (string.IsNullOrEmpty(txtNoFicha.Text))
            {
                txtNoFicha.BorderBrush = bordeError;
                txtNoFicha.Focus();
                errores = true;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errNombre.Content = req;
                txtNombre.BorderBrush = bordeError;
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
                errRfc.Content = "Al menos 13 caracteres";
                txtRFC.BorderBrush = bordeError;
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

        public void limpiar()
        {
            txtNoFicha.Text = string.Empty;
            txtNumTarjeta.Text = string.Empty;
            dtIngreso.SelectedDate = DateTime.Now;
            txtNombre.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtAntecedentes.Text = string.Empty;
            chkAudit.IsChecked = false;
            chkCalif.IsChecked = false;

            // Carga la imagen predeterminada en imgTrabajador
            string imagePath = "/Images/up.png"; // Ruta relativa a la imagen predeterminada
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            imgTrabajador.Source = bitmapImage;

            cbDpto.SelectedIndex = 0;
            cbArea.SelectedIndex = 0;
            cbPuesto.SelectedIndex = 0;


            txtNoFicha.Focus();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado es numérico
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Evita que se ingrese el carácter no numérico
            }
        }

        // Método para verificar si una cadena es numérica
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un entero
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

        private void dtIngreso_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtIngreso.SelectedDate;
            DateTime fechaActual = DateTime.Now;

            if (selectedDate.HasValue)
            {
                if (selectedDate > fechaActual)
                {
                    errFecha.Content = "No puede ser posterior a la actual";
                    errFecha.Visibility = Visibility.Visible;
                    btnGuardar.IsEnabled = false;

                }
                else
                {
                    errFecha.Visibility = Visibility.Collapsed;
                    btnGuardar.IsEnabled = true;
                }

                dtIngreso.BorderBrush = bordeNormal;
            }
            else
            {
                btnGuardar.IsEnabled = false;
                errFecha.Content = req;
                dtIngreso.FontSize = 13;
                errFecha.Visibility = Visibility.Visible;
                dtIngreso.BorderBrush = bordeError;
            }
        }

        private void btnLimp_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
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
            else if (currentText.Length < 10)
            {
                // Permitir números (6 números).
                e.Handled = !char.IsDigit(e.Text, 0);
            }
        }

        private void btnExam_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Title = "Seleccionar Imagen";

            if (selectorImagen.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = selectorImagen.OpenFile();
                bitmapImage.EndInit();

                imgTrabajador.Source = bitmapImage;

                // Convierte la imagen a bytes (en este caso, a un arreglo de bytes en formato PNG)
                byte[] imagenByte = null;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                using (MemoryStream memoria = new MemoryStream())
                {
                    encoder.Save(memoria);
                    imagenByte = memoria.ToArray();
                }
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al manejador de eventos del botón btnSearch.
                btnGuardar_Click(sender, e);
            }           
        }
    }
}
