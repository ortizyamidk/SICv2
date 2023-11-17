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
using WPF_LoginForm.Models;
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
        AreaRepository areaRepository;
        TrabajadorRepository trabajadorRepository;

        int numficha, idpuesto, idarea;
        string nombre, rfc, depto, area, puesto, auditor, perscalif, antecedentes, categoria, nivelest;

        public PersonalInfoView()
        {
            InitializeComponent();

            Deshabilitar();

            departamentoRepository = new DepartamentoRepository();
            puestoRepository = new PuestoRepository();
            areaRepository = new AreaRepository();
            trabajadorRepository = new TrabajadorRepository();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();

            LoadDepartamentosFromDatabase();
            LoadPuestoFromDatabase();
        }

        private void LoadDepartamentosFromDatabase()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void LoadPuestoFromDatabase()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Habilitar();           
        }

        public void Habilitar()
        {
            txtNombre.IsEnabled = true;
            txtRFC.IsEnabled = true;

            cbDpto.IsEnabled = true;
            cbArea.IsEnabled = true;
            cbPuesto.IsEnabled = true;

            txtJefe.IsEnabled = false;

            chkAuditor.IsEnabled = true;
            chkCalif.IsEnabled = true;

            txtAntecedentes.IsEnabled = true;

            cbNivel.IsEnabled = true;

            btnExam.IsEnabled = true;

            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;

            txtSearch.Focus();
        }

        public void Deshabilitar()
        {           
            txtNombre.IsEnabled = false;
            txtRFC.IsEnabled = false;

            cbDpto.IsEnabled = false;
            cbArea.IsEnabled = false;
            cbPuesto.IsEnabled = false;           
            
            txtJefe.IsEnabled = false;

            chkAuditor.IsEnabled = false;
            chkCalif.IsEnabled = false;

            txtAntecedentes.IsEnabled = false;

            cbNivel.IsEnabled = false;

            btnExam.IsEnabled= false;

            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = true;

            txtSearch.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;

            txtNombre.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;

            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    MessageBox.Show("Debe buscar un núm. de ficha", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                    numficha = int.Parse(txtSearch.Text);
                    nombre = txtNombre.Text;
                    rfc = txtRFC.Text;

                    ComboBoxItem deptoS = (ComboBoxItem)cbDpto.SelectedItem;
                    depto = deptoS.Content.ToString();

                    ComboBoxItem areaS = (ComboBoxItem)cbArea.SelectedItem;
                    area = areaS.Content.ToString();
                    AreaModel areaModel = areaRepository.GetIdByName(area);
                    int idarea = areaModel.Id;

                    ComboBoxItem puestoS = (ComboBoxItem)cbPuesto.SelectedItem;
                    puesto = puestoS.Content.ToString();
                    PuestoModel puesModel = puestoRepository.GetIdByNombrePuesto(puesto);
                    int idpuesto = puesModel.Id;

                    CheckBox calif = (CheckBox)chkCalif;
                    if (calif.IsChecked == true)
                    {
                        perscalif = "1";
                    }
                    else
                    {
                        perscalif = "0";
                    }

                    CheckBox audi = (CheckBox)chkAuditor;
                    if (audi.IsChecked == true)
                    {
                        auditor = "1";
                    }
                    else
                    {
                        auditor = "0";
                    }

                    ComboBoxItem nivelS = (ComboBoxItem)cbNivel.SelectedItem;
                    nivelest = nivelS.Content.ToString();

                    antecedentes = txtAntecedentes.Text;

                    byte[] fotoBytes = ObtenerBytesDesdeImagen();

                
                    MostrarCustomMessageBox();
                    Deshabilitar();               
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch_Click(sender, e);
            }          
        }

        private void cbDpto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbDpto.SelectedItem != null)
                {
                    ComboBoxItem deptoS = (ComboBoxItem)cbDpto.SelectedItem;
                    string departamento = deptoS.Content.ToString();

                    DepartamentoModel departamentoModel = departamentoRepository.GetJefeByDepartamento(departamento);
                    if (departamentoModel != null)
                    {
                        txtJefe.Text = departamentoModel.Jefe.ToString();
                    }
                    else
                    {
                        txtJefe.Text = string.Empty;
                    }

                    var areas = areaRepository.GetAreaByDepartamento(departamento);

                    cbArea.Items.Clear();
                    foreach (var area in areas)
                    {
                        var item = new ComboBoxItem
                        {
                            Content = area.NombreArea
                        };
                        cbArea.Items.Add(item);
                    }
                    cbArea.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); 
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {               
                    MessageBox.Show("Ingrese un No. de ficha", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Limpiar();
                }
                else
                {
                    int numficha = int.Parse(txtSearch.Text);
                    TrabajadorModel trabajadorModel = trabajadorRepository.GetTrabajador(numficha);

                    if(trabajadorModel != null)
                    {
                        lblNoFicha.Text = "No. ficha: " + trabajadorModel.Id;
                        lblTarjeta.Text = "No. tarjeta: " + trabajadorModel.NumTarjeta;
                        lblIngreso.Text = "Ingreso: " + trabajadorModel.FechaIngreso;
                        lblAntig.Text = "Antigüedad: " + trabajadorModel.antiguedadanios + " año(s) " + trabajadorModel.antiguedadmeses + " mes(s)";
                        lblCategoria.Text = "Categoría: " + trabajadorModel.Categoria;
                        txtNombre.Text = trabajadorModel.Nombre;
                        txtRFC.Text = trabajadorModel.RFC;
                        txtAntecedentes.Text = trabajadorModel.Antecedentes;

                        txtSearch.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No existe trabajador con ese núm. de ficha", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Limpiar()
        {
            txtSearch.Text = string.Empty;
            txtNombre.Text= string.Empty;
            txtRFC.Text= string.Empty;

            cbDpto.SelectedIndex = 0;
            cbArea.SelectedIndex = 0;
            cbPuesto.SelectedIndex = 0;
            cbNivel.SelectedIndex = 0;

            chkAuditor.IsChecked = false;
            chkCalif.IsChecked = false;           

            txtAntecedentes.Text= string.Empty;

            lblNoFicha.Text = "No. ficha: ";
            lblTarjeta.Text = "No. tarjeta: ";
            lblIngreso.Text = "Ingreso: ";
            lblAntig.Text = "Anitgüedad: ";
            lblCategoria.Text = "Categoría: ";

            txtSearch.Focus();
        }

        private byte[] ObtenerBytesDesdeImagen()
        {
            byte[] fotoBytes = null;

            try
            {
                if (imgTrabajador.Source is BitmapImage bitmapImage)
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                    using (MemoryStream memoria = new MemoryStream())
                    {
                        encoder.Save(memoria);
                        fotoBytes = memoria.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

            return fotoBytes;
        }
    }
}
