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
    /// Lógica de interacción para CursosTrabajadorInfoView.xaml
    /// </summary>
    public partial class CursosTrabajadorInfoView : UserControl
    {
        public CursosTrabajadorInfoView()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<CursoTrabajador> listaAsistencia = new ObservableCollection<CursoTrabajador>();

            listaAsistencia.Add(new CursoTrabajador { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            listaAsistencia.Add(new CursoTrabajador { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            listaAsistencia.Add(new CursoTrabajador { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            cursosTrabajadorDataGrid.ItemsSource = listaAsistencia;
        }

        public class CursoTrabajador
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }
    }
}
