using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        int idpuesto, idarea;
        string numficha, nombre, rfc, area, puesto, antecedentes, categoria, nivelest, auditor, perscalif, numtarjeta, activo;

        private int contadorCertificaciones = 0;

        private ObservableCollection<CertificacionesModel> archivos = new ObservableCollection<CertificacionesModel>();
        private List<byte[]> listaDatosImagen = new List<byte[]>();

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
            ImagenDefault();

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
            txtNumTarjeta.IsEnabled = true;

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

            btnActivo.IsEnabled = true;

            btnCert.IsEnabled = true;

            dgSelectedImages.IsEnabled = true;

            btnBorraCert.IsEnabled = true;
           

            txtSearch.Focus();
        }

        public void Deshabilitar()
        {           
            txtNombre.IsEnabled = false;
            txtRFC.IsEnabled = false;
            txtNumTarjeta.IsEnabled = false;

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

            btnActivo.IsEnabled = false;

            btnCert.IsEnabled= false;
            btnBorraCert.IsEnabled= false;

            dgSelectedImages.IsEnabled= false;

            txtSearch.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

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
                    numficha = txtSearch.Text;
                    numtarjeta = txtNumTarjeta.Text;

                    nombre = txtNombre.Text;
                    rfc = txtRFC.Text;
                    antecedentes = txtAntecedentes.Text;

                    //Area
                    ComboBoxItem areaS = (ComboBoxItem)cbArea.SelectedItem;
                    area = areaS.Content.ToString();
                    AreaModel areaModel = areaRepository.GetIdByName(area);
                    int idarea = areaModel.Id;

                    //Puesto
                    ComboBoxItem puestoS = (ComboBoxItem)cbPuesto.SelectedItem;
                    puesto = puestoS.Content.ToString();
                    PuestoModel puesModel = puestoRepository.GetIdByNombrePuesto(puesto);
                    int idpuesto = puesModel.Id;

                    //Escolaridad
                    ComboBoxItem nivelS = (ComboBoxItem)cbNivel.SelectedItem;
                    nivelest = nivelS.Content.ToString();

                    //Personal Calificado
                    CheckBox calif = (CheckBox)chkCalif;
                    if (calif.IsChecked == true)
                    {
                        perscalif = "1";
                    }
                    else
                    {
                        perscalif = "0";
                    }

                    //Auditor
                    CheckBox audi = (CheckBox)chkAuditor;
                    if (audi.IsChecked == true)
                    {
                        auditor = "1";
                    }
                    else
                    {
                        auditor = "0";
                    }                   
                    
                    //Foto
                    byte[] fotoBytes = ObtenerBytesDesdeImagen();

                    //Activo
                    ToggleButton btnAc = (ToggleButton)btnActivo;
                    if(btnAc.IsChecked == true)
                    {
                        activo = "1";
                    }
                    else
                    {
                        activo = "0";
                    }

                    //Certificaciones
                    // Serializar la colección de arreglos de bytes
                    byte[] datosImagenSerializados = SerializarDatosImagen(listaDatosImagen);

                   
                    trabajadorRepository.EditTrabajador(numtarjeta, nombre, rfc, nivelest, antecedentes, perscalif, fotoBytes, auditor, idpuesto, idarea, activo, datosImagenSerializados, numficha);

                    MostrarCustomMessageBox();
                    Deshabilitar();               
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

        private void cbPuesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPuesto.SelectedIndex == 0)
            {
                txtBuscarPuesto.Visibility = Visibility.Visible;
                txtBuscarPuesto.Focus();
                cbPuesto.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtBuscarPuesto.Visibility = Visibility.Collapsed;
                cbPuesto.Visibility = Visibility.Visible;

                ComboBoxItem puestoS = (ComboBoxItem)cbPuesto.SelectedItem;
                puesto = puestoS.Content.ToString();
                PuestoModel puestoModel = puestoRepository.GetCategoriaByPuesto(puesto);

                if (puestoModel != null)
                {
                    lblCategoria.Text = "Categoría: " + puestoModel.Categoria.ToString();
                }
                
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

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CertificacionesModel certificacion)
            {
                archivos.Remove(certificacion);
            }
        }

        private void dgSelectedImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSelectedImages.SelectedItem != null)
            {
                CertificacionesModel certificacionSeleccionada = (CertificacionesModel)dgSelectedImages.SelectedItem;
                byte[] imagenSeleccionada = listaDatosImagen[dgSelectedImages.SelectedIndex];

                MostrarImagenEnAplicacionExterna(imagenSeleccionada);
            }
        }

        private void MostrarImagenEnAplicacionExterna(byte[] imagen)
        {
            try
            {
                // Guardar la imagen en un archivo temporal
                string tempFilePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                File.WriteAllBytes(tempFilePath, imagen);

                // Abrir la imagen con la aplicación predeterminada
                Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBorraCert_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea borrar TODOS los archivos? No se podrán recuperar las certificaciones", "Confirmar Guardar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    trabajadorRepository.DeleteCertificaciones(numficha);
                    archivos.Clear();
                    listaDatosImagen.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                    numficha = txtSearch.Text;
                    TrabajadorModel trabajadorModel = trabajadorRepository.GetTrabajador(numficha);

                    if(trabajadorModel != null)
                    {

                        lblNoFicha.Text = "No. ficha: " + trabajadorModel.Id;
                        txtNumTarjeta.Text = trabajadorModel.NumTarjeta;
                        lblIngreso.Text = "Ingreso: " + trabajadorModel.FechaIng;
                        lblAntig.Text = "Antigüedad: " + trabajadorModel.antiguedadanios + " año(s) " + trabajadorModel.antiguedadmeses + " mes(es)";
                        lblCategoria.Text = "Categoría: " + trabajadorModel.Categoria;
                        txtNombre.Text = trabajadorModel.Nombre;
                        txtRFC.Text = trabajadorModel.RFC;
                        txtAntecedentes.Text = trabajadorModel.Antecedentes;                       

                        //Departamento
                        int deptoIndex = -1; 
                        for (int i = 0; i < cbDpto.Items.Count; i++)
                        {
                            ComboBoxItem item = (ComboBoxItem)cbDpto.Items[i];
                            if (item.Content.ToString() == trabajadorModel.Departamento.ToString())
                            {
                                deptoIndex = i;
                                break; 
                            }
                        }
                        if (deptoIndex == -1)
                        {
                            deptoIndex = 0;
                            ComboBoxItem item = (ComboBoxItem)cbDpto.Items[0];
                            item.Content = trabajadorModel.Departamento.ToString();
                        }
                        cbDpto.SelectedIndex = deptoIndex;

                        //Area
                        int areaIndex = -1;
                        for (int i = 0; i < cbArea.Items.Count; i++)
                        {
                            ComboBoxItem item = (ComboBoxItem)cbArea.Items[i];
                            if (item.Content.ToString() == trabajadorModel.Area.ToString())
                            {
                                areaIndex = i;
                                break;
                            }
                        }
                        if (areaIndex == -1)
                        {
                            areaIndex = 0;
                            ComboBoxItem item = (ComboBoxItem)cbArea.Items[0];
                            item.Content = trabajadorModel.Area.ToString();
                        }
                        cbArea.SelectedIndex = areaIndex;

                        //Puesto
                        int puestoIndex = -1;
                        for (int i = 0; i < cbPuesto.Items.Count; i++)
                        {
                            ComboBoxItem item = (ComboBoxItem)cbPuesto.Items[i];
                            if (item.Content.ToString() == trabajadorModel.Puesto.ToString())
                            {
                                puestoIndex = i;
                                break;
                            }
                        }
                        if (puestoIndex == -1)
                        {
                            puestoIndex = 1;
                            ComboBoxItem item = (ComboBoxItem)cbPuesto.Items[0];
                            item.Content = trabajadorModel.Puesto.ToString();
                        }
                        cbPuesto.SelectedIndex = puestoIndex;

                        //Escolaridad
                        int escolaridadIndex = -1;
                        for (int i = 0; i < cbNivel.Items.Count; i++)
                        {
                            ComboBoxItem item = (ComboBoxItem)cbNivel.Items[i];
                            if (item.Content.ToString() == trabajadorModel.Escolaridad.ToString())
                            {
                                escolaridadIndex = i;
                                break;
                            }
                        }
                        if (escolaridadIndex == -1)
                        {
                            escolaridadIndex = 0;
                            ComboBoxItem item = (ComboBoxItem)cbNivel.Items[0];
                            item.Content = trabajadorModel.Escolaridad.ToString();
                        }
                        cbNivel.SelectedIndex = escolaridadIndex;

                        //Auditor
                        if(trabajadorModel.Auditoriso14001 == true)
                        {
                            chkAuditor.IsChecked = true;
                        }
                        else
                        {
                            chkAuditor.IsChecked = false;
                        }

                        //Personal calificado
                        if(trabajadorModel.PersCalif == true)
                        {
                            chkCalif.IsChecked = true;
                        }
                        else
                        {
                            chkCalif.IsChecked = false;
                        }

                        //Foto
                        if (trabajadorModel.Foto != null && trabajadorModel.Foto.Length > 0)
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = new MemoryStream(trabajadorModel.Foto);
                            bitmapImage.EndInit();

                            imgTrabajador.Source = bitmapImage;
                        }

                        //Activo
                        if(trabajadorModel.Activo == true)
                        {
                            btnActivo.IsChecked = true;
                        }
                        else
                        {
                            btnActivo.IsChecked= false;
                        }

                        //Certificaciones
                        // Obtener las certificaciones serializadas desde el repositorio
                        byte[] certificacionesSerializadas = trabajadorRepository.ObtenerCertificacionesPorIdTrabajador(numficha);

                        if (certificacionesSerializadas != null)
                        {                           
                            // Deserializar las certificaciones
                            List<byte[]> certificacionesDeserializadas = DeserializarDatosImagen(certificacionesSerializadas);

                            // Limpiar la colección antes de agregar nuevas certificaciones
                            archivos.Clear();
                            listaDatosImagen.Clear();

                            // Generar nombres aleatorios para las certificaciones
                            foreach (var certificacion in certificacionesDeserializadas)
                            {
                                string nombreAleatorio = GenerarNombreCertificacion(numficha);
                                archivos.Add(new CertificacionesModel { NombreArchivo = nombreAleatorio });
                                listaDatosImagen.Add(certificacion);
                            }

                            // Mostrar los nombres en el DataGrid
                            dgSelectedImages.ItemsSource = archivos;
                        }
                        else
                        {                         
                            archivos.Clear();
                            listaDatosImagen.Clear();
                    }

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

        public List<byte[]> DeserializarDatosImagen(byte[] datosImagenSerializados)
        {
            // Convertir los bytes de datos serializados a una cadena JSON
            string serializedData = Encoding.UTF8.GetString(datosImagenSerializados);

            // Deserializar la cadena JSON a una lista de bytes
            List<byte[]> listaDatosImagen = JsonConvert.DeserializeObject<List<byte[]>>(serializedData);

            return listaDatosImagen;
        }

        private string GenerarNombreCertificacion(string numFicha)
        {
            contadorCertificaciones++; // Incrementar el contador cada vez que se genera un nombre

            return $"{numFicha}Certificacion{contadorCertificaciones}.jpg"; // Generar el nombre utilizando el contador
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
            txtNumTarjeta.Text = string.Empty;
            lblIngreso.Text = "Ingreso: ";
            lblAntig.Text = "Anitgüedad: ";
            lblCategoria.Text = "Categoría: ";

            ImagenDefault();

            btnActivo.IsChecked = false;

            btnCert.IsEnabled = false;

            archivos.Clear();
            listaDatosImagen.Clear();

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
