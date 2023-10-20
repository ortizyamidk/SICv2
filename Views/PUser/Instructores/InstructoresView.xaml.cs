using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPF_LoginForm.Views
{
    public partial class InstructoresView : UserControl
    {
        //filtrar
        private ObservableCollection<InstructorModel> original;
        private ICollectionView filtrado;

        public InstructoresView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // Crear una instancia de IntructorRepository
            InstructorRepository repository = new InstructorRepository();

            IEnumerable<InstructorModel> instructoresList = repository.GetByAll();
            ObservableCollection<InstructorModel> instructores = new ObservableCollection<InstructorModel>(instructoresList);

            // Asignar la lista de instructores al DataGrid
            instructoresDataGrid.ItemsSource = instructores;

            //filtrar
            original = instructores;
            filtrado = CollectionViewSource.GetDefaultView(original);
            instructoresDataGrid.ItemsSource = filtrado;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); // Establece el enfoque en el TextBox
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

        //filtrar
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower(); // Convierte el texto a minúsculas

            if (string.IsNullOrEmpty(search))
            {
                filtrado.Filter = null; // Si el texto está vacío, muestra todos los elementos
            }
            else
            {
                filtrado.Filter = item =>
                {
                    var instructor = item as InstructorModel;
                    return instructor.NomInstr.ToLower().Contains(search);
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (instructoresDataGrid.SelectedItem != null)
            {
                int numValue = (int)((instructoresDataGrid.SelectedItem as InstructorModel)?.Id);             
                txtSearch.Text = numValue.ToString();

                // Llama al método GetById en el repositorio
                InstructorRepository repository = new InstructorRepository();
                InstructorModel instructor = (repository as IInstructorRepository).GetById(numValue);

                if (instructor != null)
                {
                    // Maneja el resultado, por ejemplo, muestra los datos en un MessageBox
                    //string message = $"ID: {instructor.Id}\nNombre: {instructor.NomInstr}\nRFC: {instructor.RFC}\nTipo: {instructor.TipoInstr}\nNombre de la Compañía: {instructor.NomCia}";
                    //MessageBox.Show(message, "Detalles del Instructor");
                }
                else
                {
                    MessageBox.Show("Instructor no encontrado", "Error");
                }
            }
        }
    }
}
