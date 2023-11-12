﻿using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
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

            // Inicializa la colección de cursos
            CursosNoRegistrados = new ObservableCollection<CursoModel>();
            CursosVencidos = new ObservableCollection<CursoModel>();

            // Llama al método para obtener resultados de la consulta
            ObtenerResultadosDeConsulta();
            ObtenerCursosVencidos();


            // Crear una instancia de CursoRepository y llamar a GetCountCursosRegistered
            ICursoRepository cursoRepository = new CursoRepository();

            //AREA LOGGEADA
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);


            //cuantos cursos han sido registrados de esa area
            CountCursosRegistered = cursoRepository.GetCountCursosRegistered(user.Area); //obtener area de user loggeado

            //cuantos cursos hay que registrar en total d esa area
            CountCursos = cursoRepository.GetCountTotalCursos(); //obtener area tematica

        }


        public void ObtenerResultadosDeConsulta()
        {
            //AREA LOGGEADA
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            // Aquí puedes llamar al método del repositorio para obtener los resultados
            // Asumiendo que tienes una instancia del repositorio llamada "cursoRepository"
            CursoRepository cursoRepository = new CursoRepository();
            var resultados = cursoRepository.GetCursosNotRegistered(user.Area); //obtener area de user loggeado

            // Agrega los resultados a la colección
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
            var resultadosCV = cr.GetCursosVencidos(user.Area); //obtener area de user loggeado

            foreach(var resultadoCV in resultadosCV)
            {
                CursosVencidos.Add(resultadoCV);
            }
        }
    }
}
