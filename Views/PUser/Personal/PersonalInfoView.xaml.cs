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
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{
    
    public partial class PersonalInfoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);

        string req = "*Campo requerido";

        DepartamentoRepository departamentoRepository;
        PuestoRepository puestoRepository;

        public PersonalInfoView()
        {
            InitializeComponent();

            Deshabilitar();

            departamentoRepository = new DepartamentoRepository();
            puestoRepository = new PuestoRepository();

            LoadDepartamentosFromDatabase();
            LoadPuestoFromDatabase();
        }

        private void LoadDepartamentosFromDatabase()
        {
            var deptos = departamentoRepository.GetByAll();
            foreach (var depto in deptos)
            {
                var item = new ComboBoxItem
                {
                    Content = depto.NomDepto
                };
                cbDpto.Items.Add(item);
            }
        }

        private void LoadPuestoFromDatabase()
        {
            var puestos = puestoRepository.GetByAll();
            foreach (var puesto in puestos)
            {
                var item = new ComboBoxItem
                {
                    Content = puesto.NomPuesto
                };
                cbPuesto.Items.Add(item);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Habilitar();   
        }

        public void Habilitar()
        {
            cbCateg.IsEnabled = true;
            cbNivel.IsEnabled = true;
            txtNombre.IsEnabled = true;
            cbPuesto.IsEnabled = true;
            cbArea.IsEnabled = true;
            chkAuditor.IsEnabled = true;
            chkCalif.IsEnabled = true;
            txtAntecedentes.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;
            cbDpto.IsEnabled = true;
            txtRFC.IsEnabled = true;

            btnExam.IsEnabled = true;


            txtNombre.Focus();
        }

        public void Deshabilitar()
        {
            cbCateg.IsEnabled = false;
            cbNivel.IsEnabled = false;
            txtNombre.IsEnabled = false;
            cbPuesto.IsEnabled = false;
            cbArea.IsEnabled = false;
            cbDpto.IsEnabled = false;
            txtJefe.IsEnabled = false;
            chkAuditor.IsEnabled = false;
            chkCalif.IsEnabled = false;
            txtAntecedentes.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = true;
            txtRFC.IsEnabled= false;

            btnExam.IsEnabled= false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;

            txtNombre.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;

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
                Deshabilitar();               
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
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
                btnSave_Click(sender, e);
            }          
        }

        private void cbDpto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
