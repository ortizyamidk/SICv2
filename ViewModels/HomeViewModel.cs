using System.Collections.ObjectModel;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public ObservableCollection<DepartamentoModel> ListaDeptos { get; set; }

        //Constructor
        public HomeViewModel() 
        { 
            ListaDeptos = new ObservableCollection<DepartamentoModel>();

            ObtenerListaDeptos();
        }

        private void ObtenerListaDeptos()
        {
            DepartamentoRepository deptoRepository = new DepartamentoRepository();
            var listaResultados = deptoRepository.GetDepartamentos();

            foreach(var listaResultado in listaResultados)
            {
                ListaDeptos.Add(listaResultado);
            }
        }
    }
}
