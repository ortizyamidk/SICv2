using DocumentFormat.OpenXml.Wordprocessing;
using System;
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
using WPF_LoginForm.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WPF_LoginForm.Views.CustomerView;

namespace WPF_LoginForm.Views
{
 
    public partial class InstructorNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        string nombre, rfc, tipo, compania;
        int id;

        InstructorRepository repository;



        public InstructorNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;  
            
            repository = new InstructorRepository();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNumF.Focus();
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNumF.Content = string.Empty;
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;
            errComp.Content = string.Empty;

            txtNumF.BorderBrush = bordeNormal;
            txtNombreI.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;
            txtCompania.BorderBrush = bordeNormal;
            
            if (string.IsNullOrEmpty(txtNumF.Text))
            {
                errNumF.Content = req;
                txtNumF.BorderBrush = bordeError;
                errores = true;
            }
            else
            {
                int numf = int.Parse(txtNumF.Text);
                var existingInstructor = repository.GetByAll().FirstOrDefault(c => c.Id == numf);

                if (existingInstructor != null)
                {
                    MessageBox.Show("Ya existe ese ID de Instructor", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    errores = true;
                }
            }

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
                id = int.Parse(txtNumF.Text);
                nombre = txtNombreI.Text;
                rfc = txtRFC.Text;

                ComboBoxItem instructorS = (ComboBoxItem)cbTipo.SelectedItem;
                tipo = instructorS.Content.ToString();

                compania = txtCompania.Text;

                repository.AddInstructor(id, nombre, rfc, tipo, compania);
                
                MostrarCustomMessageBox();
                limpiar();             
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al manejador de eventos del botón btnSearch.
                btnGuardar_Click(sender, e);
            }           
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        public void limpiar()
        {
            txtNumF.Text = string.Empty;
            txtNombreI.Text = string.Empty;
            txtCompania.Text = string.Empty;
            txtRFC.Text = string.Empty;
            cbTipo.SelectedIndex = 0;

            txtNumF.Focus();
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
