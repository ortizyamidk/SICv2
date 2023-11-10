using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.ViewModels
{
    public class CursoInfoGViewModel : ViewModelBase
    {
        public CursoGViewModel _cursoGViewModel;
        public int idlista;

        public CursoInfoGViewModel(CursoGViewModel cursoGViewModel)
        {
            _cursoGViewModel = cursoGViewModel;

            // Ahora puedes acceder a la propiedad IdLista
             idlista = _cursoGViewModel.IdLista;
        }
    }
}
