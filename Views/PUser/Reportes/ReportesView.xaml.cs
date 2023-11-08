using MaterialDesignThemes.Wpf;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
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
using System.IO;
using NPOI.XSSF.UserModel;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;
using objWord = Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace WPF_LoginForm.Views
{

    public partial class ReportesView : UserControl
    {
        DepartamentoRepository departamentoRepository;
        TrabajadorRepository trabajadorRepository;
        CursoRepository cursoRepository;
        TrabajadorModel trabajadorModel;
        CursoModel cursoModel, cursoModel2;

        int numficha;
        string idtrabajador, nombretrabajador, rfc, puesto, depto, area, fechaing;

        string idcurso;
        string numcurso, nomcurso, instructor, inicio, termino, duracion, lugar, horario, idinstructor, rfcinstructor;

        object ObjMiss, filePath;

        object numf1, nombre1, puesto1, depto1, area1, fechaing1;
      
        object numc1, curso1, instructor1, inicio1, termino1, duracion1, lugar1, horario1;

        object bookmarkName, participantesBookmarkName, cursosBookmarkName, personalCABookmarkName;



        public ReportesView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            departamentoRepository = new DepartamentoRepository();
            trabajadorRepository = new TrabajadorRepository();
            cursoRepository = new CursoRepository();
        }

        private void CargarAreas()
        {
            var areas = departamentoRepository.GetDepartamentos();
            foreach (var area in areas)
            {
                var item = new ComboBoxItem
                {
                    Content = area.NomDepto
                };
                cbArea.Items.Add(item);
            }
        }

        private void CargarAreas2()
        {
            var areas = departamentoRepository.GetDepartamentos();
            foreach (var area in areas)
            {
                var item = new ComboBoxItem
                {
                    Content = area.NomDepto
                };
                cbArea2.Items.Add(item);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAreas();
            CargarAreas2();
        }

        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {
            List<TrabajadorModel> trabajadores = (List<TrabajadorModel>)trabajadorRepository.GetPersonalCalificadoGral();

            if(trabajadores.Count > 0)
            {
                ObjMiss = System.Reflection.Missing.Value;
                objWord.Application ObjWord = new objWord.Application();
                filePath = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\pcalificado.docx";
                objWord.Document ObjDoc = ObjWord.Documents.Open(filePath);

                personalCABookmarkName = "mpersonalCalificado";

                objWord.Range personalRange = ObjDoc.Bookmarks.get_Item(ref personalCABookmarkName).Range;
                objWord.Table tabla = personalRange.Tables[1];


                foreach (var personal in trabajadores)
                {
                    // Agrega una fila a la tabla
                    objWord.Row fila = tabla.Rows.Add();

                    // Rellena las celdas con los datos del trabajador
                    fila.Cells[1].Range.Text = personal.Id.ToString();
                    fila.Cells[2].Range.Text = personal.Nombre;
                    fila.Cells[3].Range.Text = personal.Area;
                    fila.Cells[4].Range.Text = personal.Puesto;
                }

                ObjWord.Visible = true;
            }
            else
            {
                MessageBox.Show("No existen registros", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnDC4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAsistencia2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(buscarCurso2.Text))
            {
                MessageBox.Show("Escriba un número de curso", "Vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                idcurso = buscarCurso2.Text;
                ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea2.SelectedItem;
                string areaS = areaseleccionada.Content.ToString();

                cursoModel = cursoRepository.GetListaAsistenciaExcel(idcurso, areaS);
                List<TrabajadorModel> trabajadores = (List<TrabajadorModel>)trabajadorRepository.GetTrabajadoresListaAsistenciaExcel(idcurso, areaS);

                if (cursoModel != null && trabajadores.Count > 0)
                {
                    nomcurso = cursoModel.NomCurso.ToString();
                    numcurso = cursoModel.Id.ToString();
                    duracion = cursoModel.Duracion.ToString();
                    inicio = cursoModel.Inicio.ToString();
                    termino = cursoModel.Termino.ToString();
                    horario = cursoModel.Horario.ToString();
                    instructor = cursoModel.Instructor.ToString();
                    idinstructor = cursoModel.idinstructor.ToString();
                    rfcinstructor = cursoModel.rfcinstructor.ToString();
                    lugar = cursoModel.Lugar.ToString();

                    string filePathExcel = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\listaAs.xlsx";

                    // Crear una aplicación Excel
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Visible = true; // Mostrar Excel (opcional)

                    // Abrir la plantilla
                    Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(filePathExcel);

                    // Acceder a la hoja de trabajo (puedes cambiar el nombre de la hoja si es necesario)
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["ListaAsistencia"];

                    //ASIGNAR DATOS A CELDAS
                    worksheet.Cells[3, 3] = nomcurso;
                    worksheet.Cells[3, 27] = numcurso;
                    worksheet.Cells[4, 3] = duracion;
                    worksheet.Cells[4, 8] = inicio;
                    worksheet.Cells[4, 14] = termino;
                    worksheet.Cells[4, 27] = horario;
                    worksheet.Cells[5, 3] = instructor;
                    worksheet.Cells[5, 8] = idinstructor;
                    worksheet.Cells[5, 11] = rfcinstructor;
                    worksheet.Cells[5, 27] = lugar;

                    worksheet.Cells[34, 6] = instructor;


                    int lim = trabajadores.Count + 9;
                    int contador = 0;

                    for (int i = 9; i < lim; i++)
                    {
                        var trabajador = trabajadores[contador];
                        worksheet.Cells[i, 2] = trabajador.Nombre;
                        worksheet.Cells[i, 5] = trabajador.Id;
                        worksheet.Cells[i, 7] = trabajador.Area;
                        contador++;
                    }

                    // Liberar recursos
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
                else
                {
                    MessageBox.Show("No existen registros para ese número de curso", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            buscarCurso2.Focus();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
            string area = areaseleccionada.Content.ToString();

            List<TrabajadorModel> trabajadores = (List<TrabajadorModel>)trabajadorRepository.GetPersonalCalificadoByArea(area);

            if (trabajadores.Count > 0)
            {
                ObjMiss = System.Reflection.Missing.Value;
                objWord.Application ObjWord = new objWord.Application();
                filePath = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\pcalificadoarea.docx";

                objWord.Document ObjDoc = ObjWord.Documents.Open(filePath);


                area1 = "marea";
                personalCABookmarkName = "mpersonal";


                objWord.Range ar = ObjDoc.Bookmarks.get_Item(ref area1).Range;
                ar.Text = area;

                objWord.Range personalRange = ObjDoc.Bookmarks.get_Item(ref personalCABookmarkName).Range;
                objWord.Table tabla = personalRange.Tables[1];
                

                foreach (var personal in trabajadores)
                {
                    // Agrega una fila a la tabla
                    objWord.Row fila = tabla.Rows.Add();

                    // Rellena las celdas con los datos del trabajador
                    fila.Cells[1].Range.Text = personal.Id.ToString();
                    fila.Cells[2].Range.Text = personal.Nombre;
                    fila.Cells[3].Range.Text = personal.Puesto;
                }

                ObjWord.Visible = true;
            }
            else
            {
                MessageBox.Show("No existen registros para esa Área", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {            
            if (string.IsNullOrEmpty(buscarTrab.Text))
            {
                MessageBox.Show("Escriba un número de ficha", "Vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else 
            {
                numficha = int.Parse(buscarTrab.Text);
                trabajadorModel = trabajadorRepository.GetTrabajadorHistorialCursos(numficha);

                List<CursoModel> cursos = (List<CursoModel>)cursoRepository.GetCursosHistorialCursos(numficha);

                if (trabajadorModel != null && cursos.Count > 0)
                {
                    idtrabajador = trabajadorModel.Id.ToString();
                    nombretrabajador = trabajadorModel.Nombre.ToString();
                    puesto = trabajadorModel.Puesto.ToString();
                    depto = trabajadorModel.Departamento.ToString();
                    area = trabajadorModel.Area.ToString();
                    fechaing = trabajadorModel.FechaIngreso.ToString();

                    ObjMiss = System.Reflection.Missing.Value;
                    objWord.Application ObjWord = new objWord.Application();
                    filePath = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\historialcursos.docx";

                    objWord.Document ObjDoc = ObjWord.Documents.Open(filePath);

                    numf1 = "mnumficha";
                    nombre1 = "mnombre";
                    puesto1 = "mpuesto";
                    depto1 = "mdepto";
                    area1 = "marea";
                    fechaing1 = "mingreso";
                    cursosBookmarkName = "mcursos";

                    objWord.Range num = ObjDoc.Bookmarks.get_Item(ref numf1).Range;
                    num.Text = idtrabajador;

                    objWord.Range nom = ObjDoc.Bookmarks.get_Item(ref nombre1).Range;
                    nom.Text = nombretrabajador;

                    objWord.Range pues = ObjDoc.Bookmarks.get_Item(ref puesto1).Range;
                    pues.Text = puesto;

                    objWord.Range dep = ObjDoc.Bookmarks.get_Item(ref depto1).Range;
                    dep.Text = depto;

                    objWord.Range ar = ObjDoc.Bookmarks.get_Item(ref area1).Range;
                    ar.Text = area;

                    objWord.Range fecha = ObjDoc.Bookmarks.get_Item(ref fechaing1).Range;
                    fecha.Text = fechaing;

                    objWord.Range cursosRange = ObjDoc.Bookmarks.get_Item(ref cursosBookmarkName).Range;
                    objWord.Table tabla = cursosRange.Tables[1];

                    foreach (var curso in cursos)
                    {
                        // Agrega una fila a la tabla
                        objWord.Row fila = tabla.Rows.Add();

                        // Rellena las celdas con los datos del trabajador
                        fila.Cells[1].Range.Text = curso.NomCurso;
                        fila.Cells[2].Range.Text = curso.Inicio;
                        fila.Cells[3].Range.Text = curso.Instructor;
                    }

                    ObjWord.Visible = true;
                }
                else
                {
                    MessageBox.Show("No existen registros para ese número de ficha", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            Limpiar();
            buscarTrab.Focus();

        }

        private void btnDC3_Click(object sender, RoutedEventArgs e)
        {           
            if (string.IsNullOrEmpty(buscarDC3.Text))
            {
                MessageBox.Show("Escriba un número de ficha", "Vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                numficha = int.Parse(buscarDC3.Text);
                trabajadorModel = trabajadorRepository.FormatoDC3(numficha);

                if (trabajadorModel != null)
                {
                    idtrabajador = trabajadorModel.Id.ToString();
                    nombretrabajador = trabajadorModel.Nombre.ToString();
                    rfc = trabajadorModel.RFC.ToString();
                    puesto = trabajadorModel.Puesto.ToString();

                    //crear documento
                    ObjMiss = System.Reflection.Missing.Value;
                    objWord.Application ObjWord = new objWord.Application();
                    filePath = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\formatodc3.docx";
                    //object filePath = "/Plantillas/formatodc3.docx"; // Reemplaza "NombreCarpetaEnProyecto" con la ubicación correcta

                    objWord.Document ObjDoc = ObjWord.Documents.Open(filePath);

                    numf1 = "mnumficha";
                    nombre1 = "mnombre";
                    puesto1 = "mpuesto";

                    for (int i = 0; i < rfc.Length; i++)
                    {
                        bookmarkName = "x" + (i + 1);
                        char character = rfc[i];

                        objWord.Range bookmarkRange = ObjDoc.Bookmarks.get_Item(ref bookmarkName).Range;
                        bookmarkRange.Text = character.ToString();
                    }


                    objWord.Range num = ObjDoc.Bookmarks.get_Item(ref numf1).Range;
                    num.Text = idtrabajador;

                    objWord.Range nom = ObjDoc.Bookmarks.get_Item(ref nombre1).Range;
                    nom.Text = nombretrabajador;


                    objWord.Range pues = ObjDoc.Bookmarks.get_Item(ref puesto1).Range;
                    pues.Text = puesto;

                    ObjWord.Visible = true;
                }
                else
                {
                    MessageBox.Show("No existen registros para ese número de ficha", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            Limpiar();
            buscarDC3.Focus();

        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(buscarCurso.Text))
            {
                MessageBox.Show("Escriba un número de curso", "Vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                idcurso = buscarCurso.Text;
                cursoModel = cursoRepository.GetCursoListaAsistencia(idcurso);

                List<TrabajadorModel> trabajadores = (List<TrabajadorModel>)trabajadorRepository.GetTrabajadoresListaAsistencia(idcurso);

                if (cursoModel != null && trabajadores.Count>0 && cursoModel2 != null)
                {
                    //obtener los resultados de la consulta sql en variables string
                    numcurso = cursoModel.Id.ToString();
                    nomcurso = cursoModel.NomCurso.ToString();
                    instructor = cursoModel.Instructor.ToString();
                    inicio = cursoModel.Inicio.ToString();
                    termino = cursoModel.Termino.ToString();
                    duracion = cursoModel.Duracion.ToString() + " minutos";
                    lugar = cursoModel.Lugar.ToString();
                    horario = cursoModel.Horario.ToString();

                    //abrir plantilla documento
                    ObjMiss = System.Reflection.Missing.Value;
                    objWord.Application ObjWord = new objWord.Application();
                    filePath = "C:\\Users\\yami_\\Documents\\PLANTILLAS\\listaasistencia.docx";

                    objWord.Document ObjDoc = ObjWord.Documents.Open(filePath);

                    //asignarle a los object de referencia el nombre de las bookmark en el word
                    numc1 = "midcurso";
                    curso1 = "mnomcurso";
                    instructor1 = "minstructor";
                    inicio1 = "minicio";
                    termino1 = "mtermino";
                    duracion1 = "mduracion";
                    lugar1 = "mlugar";
                    horario1 = "mhorario";
                    participantesBookmarkName = "mparticipantes";

                    objWord.Range numc = ObjDoc.Bookmarks.get_Item(ref numc1).Range;
                    numc.Text = numcurso;

                    objWord.Range nomc = ObjDoc.Bookmarks.get_Item(ref curso1).Range;
                    nomc.Text = nomcurso;

                    objWord.Range inst = ObjDoc.Bookmarks.get_Item(ref instructor1).Range;
                    inst.Text = instructor;

                    objWord.Range ini = ObjDoc.Bookmarks.get_Item(ref inicio1).Range;
                    ini.Text = inicio;

                    objWord.Range ter = ObjDoc.Bookmarks.get_Item(ref termino1).Range;
                    ter.Text = termino;

                    objWord.Range dur = ObjDoc.Bookmarks.get_Item(ref duracion1).Range;
                    dur.Text = duracion;

                    objWord.Range lug = ObjDoc.Bookmarks.get_Item(ref lugar1).Range;
                    lug.Text = lugar;

                    objWord.Range hor = ObjDoc.Bookmarks.get_Item(ref horario1).Range;
                    hor.Text = horario;

                    objWord.Range participantesRange = ObjDoc.Bookmarks.get_Item(ref participantesBookmarkName).Range;
                    objWord.Table tabla = participantesRange.Tables[1];
                    // Itera a través de la lista de trabajadores y agrega cada uno a la tabla
                    foreach (var trabajador in trabajadores)
                    {
                        // Agrega una fila a la tabla
                        objWord.Row fila = tabla.Rows.Add();

                        // Rellena las celdas con los datos del trabajador
                        fila.Cells[1].Range.Text = trabajador.Id.ToString();
                        fila.Cells[2].Range.Text = trabajador.Nombre;
                        fila.Cells[3].Range.Text = trabajador.Area;
                    }

                    ObjWord.Visible = true;


                    //EXCEL

                }
                else
                {
                    MessageBox.Show("No existen registros para ese número de curso", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }

            Limpiar();
            buscarCurso.Focus();
            
        }

        private void buscarTrab_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado es numérico
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Evita que se ingrese el carácter no numérico
            }
        }

        // Método para verificar si una cadena es numérica
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un entero
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

        private void Limpiar()
        {
            buscarTrab.Text = string.Empty;
            buscarDC3.Text = string.Empty;
            buscarCurso.Text = string.Empty;
            buscarCurso2.Text = string.Empty;
        }
    }
}
