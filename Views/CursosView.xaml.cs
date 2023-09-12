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

            var converter = new BrushConverter();
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();

            //create datagrid items info
            cursos.Add(new Curso { num = "1", nombre = "Curso 1", area = "Area 1", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "9", nombre = "Curso 9", area = "Area 9", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "10", nombre = "Curso 10", area = "Area 10", tipo = "Externo", instructor = "No definido" });

            cursos.Add(new Curso { num = "1", nombre = "Curso 1", area = "Area 1", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "9", nombre = "Curso 9", area = "Area 9", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "10", nombre = "Curso 10", area = "Area 10", tipo = "Externo", instructor = "No definido" });

            cursos.Add(new Curso { num = "1", nombre = "Curso 1", area = "Area 1", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "2", nombre = "Curso 2", area = "Area 2", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "3", nombre = "Curso 3", area = "Area 3", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "4", nombre = "Curso 4", area = "Area 4", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "5", nombre = "Curso 5", area = "Area 5", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "6", nombre = "Curso 6", area = "Area 6", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "7", nombre = "Curso 7", area = "Area 7", tipo = "Interno", instructor = "No definido" });
            cursos.Add(new Curso { num = "8", nombre = "Curso 8", area = "Area 8", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "9", nombre = "Curso 9", area = "Area 9", tipo = "Externo", instructor = "No definido" });
            cursos.Add(new Curso { num = "10", nombre = "Curso 10", area = "Area 10", tipo = "Externo", instructor = "No definido" });

            cursosDataGrid.ItemsSource = cursos;


        }

        public class Curso
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
            public string tipo { get; set; }
            public string instructor { get; set; }
        }
    }
}
