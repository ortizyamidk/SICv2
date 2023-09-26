using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CursosView.xaml
    /// </summary>
    public partial class CursosView : UserControl
    {
        public CursosView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            

            var converter = new BrushConverter();
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();

            //create datagrid items info
            cursos.Add(new Curso { num = "128", nombre = "Bloqueo y Etiquetado", area = "Area 1" });
            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8" });

            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8" });

            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8" });



            cursosDataGrid.ItemsSource = cursos;


        }

        public class Curso
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string texto = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("El campo no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
        }
    }
}
