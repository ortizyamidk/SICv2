using MaterialDesignThemes.Wpf;
using Microsoft.Office.Interop.Word;
using System;
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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;
using objWord = Microsoft.Office.Interop.Word;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para ReportesView.xaml
    /// </summary>
    public partial class ReportesView : UserControl
    {
        DepartamentoRepository departamentoRepository;
        

        public ReportesView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            departamentoRepository = new DepartamentoRepository();
        }

        private void CargarAreas()
        {
            var areas = departamentoRepository.GetDepartamentos();
            foreach (var area in areas)
            {
                var item = new ComboBoxItem
                {
                    Content = area.NomDepto
                };
                cbArea.Items.Add(item);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAreas();
        }

        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDC4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
            string area = areaseleccionada.Content.ToString();
        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {
            string numficha = buscarTrab.Text;
        }

        private void btnDC3_Click(object sender, RoutedEventArgs e)
        {
            int numficha = int.Parse(buscarDC3.Text);

            TrabajadorRepository trabajadorRepository = new TrabajadorRepository();

            TrabajadorModel trabajador = trabajadorRepository.FormatoDC3(numficha);

            string idtrabajador = trabajador.Id.ToString();
            string nombretrabajador = trabajador.Nombre.ToString();
            string rfc = trabajador.RFC.ToString();
            string puesto = trabajador.Puesto.ToString();


            //crear documento
            object ObjMiss = System.Reflection.Missing.Value;
            objWord.Application ObjWord = new objWord.Application();

            object numf1 = "mnumficha";
            object nombre1 = "mnombre";
            //object rfc1 = "mrfc";
            object puesto1 = "mpuesto";
            

            objWord.Document ObjDoc = ObjWord.Documents.Open("\"C:\\Users\\yami_\\Documents\\PLANTILLAS\\formatodc3.docx\"");

            objWord.Range num = ObjDoc.Bookmarks.get_Item(ref numf1).Range;
            num.Text = idtrabajador;

            objWord.Range nom = ObjDoc.Bookmarks.get_Item(ref nombre1).Range;
            nom.Text = nombretrabajador;

            //objWord.Range RFC = ObjDoc.Bookmarks.get_Item(ref rfc1).Range;
            //RFC.Text = rfc;

            objWord.Range pues = ObjDoc.Bookmarks.get_Item(ref puesto1).Range;
            pues.Text = puesto;

            object rango1 = num;
            object rango2 = nom;
            //object rango3 = RFC;
            object rango4 = pues;

            ObjDoc.Bookmarks.Add("mnum", ref rango1);
            ObjDoc.Bookmarks.Add("mnombre", ref rango2);
            //ObjDoc.Bookmarks.Add("mrfc", ref rango3);
            ObjDoc.Bookmarks.Add("mpuesto", ref rango4);

            ObjWord.Visible = true;


        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "Se generará reporte en formato de Excel";

            string numcurso = buscarCurso.Text;
        }

        private void buscarTrab_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
}
