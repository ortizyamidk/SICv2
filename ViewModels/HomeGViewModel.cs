using System.Collections.ObjectModel;
using System.Threading;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class HomeGViewModel : ViewModelBase
    {
        public ObservableCollection<CursoModel> CursosNoRegistrados { get; set; }
        public ObservableCollection<CursoModel> CursosVencidos { get; set; }

        private IUserRepository userRepository;

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

        private UserAccountModel _currentUserAccount;
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        //Constructor
        public HomeGViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            CursosNoRegistrados = new ObservableCollection<CursoModel>();
            CursosVencidos = new ObservableCollection<CursoModel>();

            ObtenerResultadosDeConsulta();
            ObtenerCursosVencidos();


            // Crear una instancia de CursoRepository y llamar a GetCountCursosRegistered
            ICursoRepository cursoRepository = new CursoRepository();

            //AREA LOGGEADA
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            //cuantos cursos han sido registrados de esa area
            CountCursosRegistered = cursoRepository.GetCountCursosRegistered(user.Area); 

            //cuantos cursos hay que registrar en total d esa area
            CountCursos = cursoRepository.GetCountTotalCursos(user.Area); 

        }


        public void ObtenerResultadosDeConsulta()
        {
            //AREA LOGGEADA
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            CursoRepository cursoRepository = new CursoRepository();
            var resultados = cursoRepository.GetCursosNotRegistered(user.Area);

            foreach (var resultado in resultados)
            {
                CursosNoRegistrados.Add(resultado);
            }
        }

        public void ObtenerCursosVencidos()
        {
            //AREA LOGGEADA
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            CursoRepository cr = new CursoRepository();
            var resultadosCV = cr.GetCursosVencidos(user.Area);

            foreach(var resultadoCV in resultadosCV)
            {
                CursosVencidos.Add(resultadoCV);
            }
        }
    }
}
