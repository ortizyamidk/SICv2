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

namespace WPF_LoginForm.Views
{

    public partial class CustomerView : UserControl
    {
        //filtrar
        private ObservableCollection<TrabajadorModel> original;
        private ICollectionView filtrado;

        public CustomerView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            TrabajadorRepository repository = new TrabajadorRepository();

            try
            {
                IEnumerable<TrabajadorModel> trabajadoresList = repository.GetByAll();
                ObservableCollection<TrabajadorModel> trabajadores = new ObservableCollection<TrabajadorModel>(trabajadoresList);          
                personalDataGrid.ItemsSource = trabajadores;

                //filtrar
                original = trabajadores;
                filtrado = CollectionViewSource.GetDefaultView(original);
                personalDataGrid.ItemsSource = filtrado;
                txtSearch.TextChanged += TxtSearch_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
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
                    var curso = item as TrabajadorModel;
                    return curso.Nombre.ToLower().Contains(search);
                };
            }
        }
    }
}
