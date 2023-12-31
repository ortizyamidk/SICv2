﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{

    public partial class PersonalNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        int idpuesto, idarea;
        string id, numtarjeta, nombre, fechaing, rfc, escolaridad, antecedentes, foto, perscalif, auditoriso14001;

        PuestoRepository puestoRepository;
        AreaRepository areaRepository;
        TrabajadorRepository trabajadorRepository;
        DepartamentoRepository departamentoRepository;

        private ObservableCollection<CertificacionesModel> archivos = new ObservableCollection<CertificacionesModel>();

        private List<byte[]> listaDatosImagen = new List<byte[]>();

        public PersonalNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            txtJefe.IsEnabled = false;
            dtIngreso.SelectedDate = DateTime.Now;  
            
            puestoRepository = new PuestoRepository();
            areaRepository = new AreaRepository();
            trabajadorRepository = new TrabajadorRepository();
            departamentoRepository = new DepartamentoRepository();
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNoFicha.Focus();

            LoadDepartamentosFromDatabase();
            LoadPuestoFromDatabase();
            ImagenDefault();           
        }

        private void ImagenDefault()
        {
            string imagePath = "/Images/up.png";

            // Crear la URI de la imagen
            Uri imageUri = new Uri(imagePath, UriKind.Relative);

            // Crear un objeto BitmapImage
            System.Windows.Media.Imaging.BitmapImage bitmap = new System.Windows.Media.Imaging.BitmapImage(imageUri);

            // Establecer la imagen al control Image
            imgTrabajador.Source = bitmap;
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

            try
            {
                DateTime? selectedDate = dtIngreso.SelectedDate;
                DateTime fechaConvertida = Convert.ToDateTime(selectedDate);

                DateTime fechaActual = DateTime.Now;

                string selecteddate = selectedDate.ToString();


                if (selectedDate > fechaActual)
                {
                   errores = true;
                }

                if (string.IsNullOrEmpty(selecteddate))
                {
                    errores = true;
                }

                if (string.IsNullOrEmpty(txtNoFicha.Text) || string.IsNullOrEmpty(txtNumTarjeta.Text))
                {
                    MessageBox.Show("Debe llenar No. ficha y Tarjeta");
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
                    id = txtNoFicha.Text;
                    numtarjeta = txtNumTarjeta.Text;
                    nombre = txtNombre.Text;
                    fechaing = selecteddate;
                    rfc = txtRFC.Text;
                    ComboBoxItem escolaridadS = (ComboBoxItem)cbNivel.SelectedItem;
                    escolaridad = escolaridadS.Content.ToString();
                    antecedentes = txtAntecedentes.Text;

                    CheckBox calif = (CheckBox)chkCalif;
                    if (calif.IsChecked == true)
                    {
                        perscalif = "1";
                    }
                    else
                    {
                        perscalif = "0";
                    }

                    byte[] fotoBytes = ObtenerBytesDesdeImagen();

                    CheckBox audi = (CheckBox)chkAudit;
                    if (audi.IsChecked == true)
                    {
                        auditoriso14001 = "1";
                    }
                    else
                    {
                        auditoriso14001 = "0";
                    }

                    ComboBoxItem puestoS = (ComboBoxItem)cbPuesto.SelectedItem;
                    string puestoSelec = puestoS.Content.ToString();
                    PuestoModel puesModel = puestoRepository.GetIdByNombrePuesto(puestoSelec);
                    idpuesto = puesModel.Id;

                    ComboBoxItem areaS = (ComboBoxItem)cbArea.SelectedItem;
                    string areaSelec = areaS.Content.ToString();
                    AreaModel areaModel = areaRepository.GetIdByName(areaSelec);
                    idarea = areaModel.Id;

                    // Serializar la colección de arreglos de bytes
                    byte[] datosImagenSerializados = SerializarDatosImagen(listaDatosImagen);

                    trabajadorRepository.AddTrabajador(id, numtarjeta, nombre, fechaConvertida, rfc, escolaridad, antecedentes, perscalif, fotoBytes, auditoriso14001, idpuesto, idarea, datosImagenSerializados);

                    MostrarCustomMessageBox();
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        private byte[] SerializarDatosImagen(List<byte[]> listaDatosImagen)
        {
            // Serializar la colección usando JSON.NET
            string serializedData = JsonConvert.SerializeObject(listaDatosImagen);

            // Convertir la cadena serializada a un array de bytes
            byte[] serializedBytes = Encoding.UTF8.GetBytes(serializedData);

            return serializedBytes;
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

            ImagenDefault();

            cbDpto.SelectedIndex = 0;
            cbArea.SelectedIndex = 0;
            cbPuesto.SelectedIndex = 0;
            cbCat.SelectedIndex = 0;
            cbNivel.SelectedIndex = 0;

            archivos.Clear();

            txtNoFicha.Focus();
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

        private void cbPuesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(cbPuesto.SelectedIndex == 0)
            {
                txtBuscarPuesto.Visibility = Visibility.Visible;
                txtBuscarPuesto.Focus();
                cbPuesto.Visibility = Visibility.Collapsed;                
            }
            else
            {
                txtBuscarPuesto.Visibility=Visibility.Collapsed;
                cbPuesto.Visibility = Visibility.Visible;
            }

        }

        private void txtBuscarPuesto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarPuesto.Text))
            {
                txtBuscarPuesto.Visibility = Visibility.Collapsed;
                cbPuesto.SelectedIndex = 1;
                cbPuesto.Visibility = Visibility.Visible;
            }
            else
            {
                // Lógica de búsqueda al escribir en el TextBox
                string textoBusqueda = txtBuscarPuesto.Text.ToLower(); // Obtener el texto y convertirlo a minúsculas para hacer la comparación más flexible

                // Buscar coincidencias en los elementos del ComboBox
                ComboBoxItem coincidencia = null;
                foreach (ComboBoxItem item in cbPuesto.Items)
                {
                    if (item.Content.ToString().ToLower().StartsWith(textoBusqueda))
                    {
                        coincidencia = item;
                        break;
                    }
                }

                if (coincidencia != null)
                {
                    // Se encontró una coincidencia
                    cbPuesto.SelectedItem = coincidencia;
                    txtBuscarPuesto.Text = string.Empty;
                    txtBuscarPuesto.Visibility = Visibility.Collapsed;

                }
                else
                {
                    // No se encontró ninguna coincidencia
                    MessageBox.Show("No se encontró ninguna coincidencia en la búsqueda.", "Sin coincidencias");
                    txtBuscarPuesto.Text = string.Empty;
                    txtBuscarPuesto.Visibility = Visibility.Collapsed;

                    cbPuesto.Visibility = Visibility.Visible;
                    cbPuesto.SelectedIndex = 1;
                }
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

        private void txtNoFicha_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verificar si el carácter ingresado es alfanumérico
            if (!IsAlphaNumeric(e.Text))
            {
                e.Handled = true; // Evitar que se ingrese el carácter no alfanumérico
            }
        }

        private void btnCert_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; // Permitir seleccionar múltiples archivos
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif"; // Filtrar por archivos de imagen

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    byte[] datosImagen = File.ReadAllBytes(filename);
                    listaDatosImagen.Add(datosImagen);
                    archivos.Add(new CertificacionesModel { NombreArchivo = System.IO.Path.GetFileName(filename) });
                    dgSelectedImages.ItemsSource = archivos;
                }
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CertificacionesModel certificacion)
            {
                archivos.Remove(certificacion);
            }
        }


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
                //btnGuardar_Click(sender, e);
               
            }           
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

        private bool IsAlphaNumeric(string text)
        {
            return text.All(char.IsLetterOrDigit); // Comprobar si todos los caracteres son letras o números
        }

    }
}
