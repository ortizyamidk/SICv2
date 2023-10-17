using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class HomeGViewModel : ViewModelBase
    {
        public ObservableCollection<CursoModel> CursosNoRegistrados { get; set; }

        private int _countCursosRegistered;
        public int CountCursosRegistered
        {
            get { return _countCursosRegistered; }
            set
            {
                _countCursosRegistered = value;
                OnPropertyChanged(nameof(CountCursosRegistered)); // Implementa la notificación de cambios en tu ViewModel
            }
        }

        private int _countCursos;
        public int CountCursos
        {
            get { return _countCursos; }
            set
            {
                _countCursos = value;
                OnPropertyChanged(nameof(CountCursos));
            }
        }

        //Constructor
        public HomeGViewModel()
        {
            // Inicializa la colección de cursos
            CursosNoRegistrados = new ObservableCollection<CursoModel>();
            // Llama al método para obtener resultados de la consulta
            ObtenerResultadosDeConsulta();


            // Crear una instancia de CursoRepository y llamar a GetCountCursosRegistered
            ICursoRepository cursoRepository = new CursoRepository();
            CountCursosRegistered = cursoRepository.GetCountCursosRegistered("Calidad");

            CountCursos = cursoRepository.GetCountTotalCursos("Calidad");

        }

        public void ObtenerResultadosDeConsulta()
        {
            // Aquí puedes llamar al método del repositorio para obtener los resultados
            // Asumiendo que tienes una instancia del repositorio llamada "cursoRepository"
            CursoRepository cursoRepository = new CursoRepository();
            var resultados = cursoRepository.GetCursosNotRegistered("Calidad");

            // Agrega los resultados a la colección
            foreach (var resultado in resultados)
            {
                CursosNoRegistrados.Add(resultado);
            }
        }





    }
}
