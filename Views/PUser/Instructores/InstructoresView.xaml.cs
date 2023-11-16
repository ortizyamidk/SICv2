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

        public event EventHandler<int> SelectedIdChanged;

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

        private void instructoresDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (instructoresDataGrid.SelectedItem != null)
            {
                // Suponiendo que tienes una clase Instructor y el ID está en la propiedad Id
                InstructorModel selectedInstructor = (InstructorModel)instructoresDataGrid.SelectedItem;
                int selectedId = selectedInstructor.Id;

                
            }
        }
    }
}
