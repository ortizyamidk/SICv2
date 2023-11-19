using System.Collections.ObjectModel;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public ObservableCollection<AreaModel> ListaAreas { get; set; }

        public HomeViewModel() 
        {
            ListaAreas = new ObservableCollection<AreaModel>();

            ObtenerListaDeptos();
        }

        private void ObtenerListaDeptos()
        {
            AreaRepository areaRepository = new AreaRepository();
            var listaResultados = areaRepository.GetProgresoAreas();

            foreach(var listaResultado in listaResultados)
            {
                ListaAreas.Add(listaResultado);
            }
        }
    }
}
