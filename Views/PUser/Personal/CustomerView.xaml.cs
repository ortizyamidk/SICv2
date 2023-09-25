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
using static WPF_LoginForm.Views.CursosView;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            txtSearch.Focus();

            var converter = new BrushConverter();
            ObservableCollection<Personal> trabajadores = new ObservableCollection<Personal>();

            trabajadores.Add(new Personal { numficha = "57019", nombre = "Perez Arredondo Manuel Eduardo", area = "Almacen de componentes", puesto = "Acomodo de material" });
            trabajadores.Add(new Personal { numficha = "2", nombre = "Trabajador 2", area = "Área 2", puesto = "Puesto 2" });
            trabajadores.Add(new Personal { numficha = "3", nombre = "Trabajador 3", area = "Área 3", puesto = "Puesto 3" });
            trabajadores.Add(new Personal { numficha = "4", nombre = "Trabajador 4", area = "Área 4", puesto = "Puesto 4" });
            trabajadores.Add(new Personal { numficha = "5", nombre = "Trabajador 5", area = "Área 5", puesto = "Puesto 5" });
            trabajadores.Add(new Personal { numficha = "6", nombre = "Trabajador 6", area = "Área 6", puesto = "Puesto 6" });
            trabajadores.Add(new Personal { numficha = "7", nombre = "Trabajador 7", area = "Área 7", puesto = "Puesto 7" });
            trabajadores.Add(new Personal { numficha = "8", nombre = "Trabajador 8", area = "Área 8", puesto = "Puesto 8" });
            trabajadores.Add(new Personal { numficha = "9", nombre = "Trabajador 9", area = "Área 9", puesto = "Puesto 9" });
            trabajadores.Add(new Personal { numficha = "10", nombre = "Trabajador 10", area = "Área 10", puesto = "Puesto 10" });

            trabajadores.Add(new Personal { numficha = "1", nombre = "Trabajador 1", area = "Área 1", puesto = "Puesto 1" });
            trabajadores.Add(new Personal { numficha = "2", nombre = "Trabajador 2", area = "Área 2", puesto = "Puesto 2" });
            trabajadores.Add(new Personal { numficha = "3", nombre = "Trabajador 3", area = "Área 3", puesto = "Puesto 3" });
            trabajadores.Add(new Personal { numficha = "4", nombre = "Trabajador 4", area = "Área 4", puesto = "Puesto 4" });
            trabajadores.Add(new Personal { numficha = "5", nombre = "Trabajador 5", area = "Área 5", puesto = "Puesto 5" });
            trabajadores.Add(new Personal { numficha = "6", nombre = "Trabajador 6", area = "Área 6", puesto = "Puesto 6" });
            trabajadores.Add(new Personal { numficha = "7", nombre = "Trabajador 7", area = "Área 7", puesto = "Puesto 7" });
            trabajadores.Add(new Personal { numficha = "8", nombre = "Trabajador 8", area = "Área 8", puesto = "Puesto 8" });
            trabajadores.Add(new Personal { numficha = "9", nombre = "Trabajador 9", area = "Área 9", puesto = "Puesto 9" });
            trabajadores.Add(new Personal { numficha = "10", nombre = "Trabajador 10", area = "Área 10", puesto = "Puesto 10" });

            trabajadores.Add(new Personal { numficha = "1", nombre = "Trabajador 1", area = "Área 1", puesto = "Puesto 1" });
            trabajadores.Add(new Personal { numficha = "2", nombre = "Trabajador 2", area = "Área 2", puesto = "Puesto 2" });
            trabajadores.Add(new Personal { numficha = "3", nombre = "Trabajador 3", area = "Área 3", puesto = "Puesto 3" });
            trabajadores.Add(new Personal { numficha = "4", nombre = "Trabajador 4", area = "Área 4", puesto = "Puesto 4" });
            trabajadores.Add(new Personal { numficha = "5", nombre = "Trabajador 5", area = "Área 5", puesto = "Puesto 5" });
            trabajadores.Add(new Personal { numficha = "6", nombre = "Trabajador 6", area = "Área 6", puesto = "Puesto 6" });
            trabajadores.Add(new Personal { numficha = "7", nombre = "Trabajador 7", area = "Área 7", puesto = "Puesto 7" });
            trabajadores.Add(new Personal { numficha = "8", nombre = "Trabajador 8", area = "Área 8", puesto = "Puesto 8" });
            trabajadores.Add(new Personal { numficha = "9", nombre = "Trabajador 9", area = "Área 9", puesto = "Puesto 9" });
            trabajadores.Add(new Personal { numficha = "10", nombre = "Trabajador 10", area = "Área 10", puesto = "Puesto 10" });

            personalDataGrid.ItemsSource = trabajadores;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        public class Personal
        {
            public string numficha { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
            public string puesto { get; set; }
 
        }


    }
}
