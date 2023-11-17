using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{
    public partial class InstructorInfoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        InstructorRepository repository;
        string nombre, rfc, tipo, comp;
        int id;

        public InstructorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            deshabilitar();
            btnSave.IsEnabled = false;

            repository = new InstructorRepository();                
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            deshabilitar();
            btnSave.IsEnabled = false;
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
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;
            errComp.Content = string.Empty;

            txtNombreI.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;
            txtCompania.BorderBrush = bordeNormal;

            try
            {
                if (string.IsNullOrEmpty(txtNoInst.Text))
                {
                    MessageBox.Show("Busque un instructor", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    errores = true;
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
                    id = int.Parse(txtNoInst.Text);
                    nombre = txtNombreI.Text;
                    rfc = txtRFC.Text;
                    ComboBoxItem instructorS = (ComboBoxItem)cbTipo.SelectedItem;
                    tipo = instructorS.Content.ToString();
                    comp = txtCompania.Text;


                    repository.EditInstructor(nombre, rfc, tipo, comp, id);
                    MostrarCustomMessageBox();
                    deshabilitar();
                    txtSearch.Text = string.Empty;
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                btnSearch_Click(sender, e);
            }           
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    MessageBox.Show("Ingrese un No. de Instructor", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Limpiar();
                }
                else
                {
                    int idinstructor = int.Parse(txtSearch.Text);
                    InstructorModel instructor = (repository as IInstructorRepository).GetById(idinstructor);

                    if(instructor != null)
                    {
                        txtNoInst.Text = instructor.Id.ToString();
                        txtNombreI.Text = instructor.NomInstr.ToString();
                        txtRFC.Text = instructor.RFC.ToString();
                        txtCompania.Text=instructor.NomCia.ToString();

                        if (instructor.TipoInstr.Equals("Interno"))
                        {
                            cbTipo.SelectedIndex = 0;
                        }
                        if (instructor.TipoInstr.Equals("Externo"))
                        {
                            cbTipo.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existe instructor con ese ID", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado es numérico
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Evita que se ingrese el carácter no numérico
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un entero
        }

        private void Limpiar()
        {
            txtNoInst.Text = string.Empty;
            txtNombreI.Text = string.Empty;
            txtRFC.Text = string.Empty;
            cbTipo.SelectedIndex = 0;
            txtCompania.Text = string.Empty;
            btnSave.IsEnabled = false;
            txtSearch.Text = string.Empty;

            txtSearch.Focus();
        }
    }
}
