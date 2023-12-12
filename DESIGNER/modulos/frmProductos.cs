using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOL;
using ENTITIES;
using DESIGNER.Tools;
namespace DESIGNER.modulos
{

    public partial class frmProductos : Form
    {
        producto Producto = new producto();
        EProduto eproduto = new EProduto();
        Subcategoria subcategoria = new Subcategoria();
        Categoria categoria = new Categoria();
        Marca marca = new Marca();
        bool categoriasLista = false; // una bandera en programación es una variable lógica que controla estados

        public frmProductos()
        {
            InitializeComponent();
        }
        #region Método para cargar datos
        private void actualizarProductos()
        {
            gridProductos.DataSource = Producto.listar();
            gridProductos.Refresh();
        }
        private void actualizarMarcas()
        {
            cboMarca.DataSource = marca.listar();
            cboMarca.DisplayMember = "marca";
            cboMarca.ValueMember = "idmarca";
            cboMarca.Refresh();
            cboMarca.Text = " ";

        }
        private void actualizarCaegoria()
        {
            cboCategoria.DataSource = categoria.listar();
            cboCategoria.DisplayMember = "categoria";
            cboCategoria.ValueMember = "idcategoria";
            cboCategoria.Refresh();
            cboCategoria.Text = " ";
        }
        #endregion 
        private void frmProductos_Load(object sender, EventArgs e)
        {
            actualizarProductos();
            actualizarMarcas();
            actualizarCaegoria();
            categoriasLista = true;

        }

        private void cboSubcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoriasLista)
            {
                //Invocar al método que carga las subcategorias
                int idcategoria = Convert.ToInt32(cboCategoria.SelectedValue.ToString());
                cboSubcategoria.DataSource = subcategoria.listar(idcategoria);
                cboSubcategoria.DisplayMember = "subcategoria";
                cboSubcategoria.ValueMember = "idsubcategoria";
                cboSubcategoria.Refresh();
                cboSubcategoria.Text = "";
            }

        }
        private void reiniciar()
        {
            cboMarca.Text = "";
            cboCategoria.Text = "";
            cboSubcategoria.Text = "";
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtGarantia.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (Aviso.Preguntar("¿Está Seguro de registrar un nuevo producto?") == DialogResult.Yes)
            {
                eproduto.idmarca = Convert.ToInt32(cboMarca.SelectedValue.ToString()); ;
                eproduto.subcategoria = Convert.ToInt32(cboSubcategoria.SelectedValue.ToString());
                eproduto.descripcion = txtDescripcion.Text;
                eproduto.garantia = Convert.ToInt32(txtGarantia.Text);
                eproduto.precio = Convert.ToDouble(txtPrecio.Text);
                eproduto.stock = Convert.ToInt32(txtStock.Text);

                if (Producto.registrar(eproduto) > 0)
                {
                    Aviso.informar("Nuevo usuario registrado");
                    reiniciar();
                    actualizarDatos(); // actualiza los datos del datagridview

                }
                else
                {
                    Aviso.advertir("No hemos podido terminar el registro");
                }
            }
            
        }
        private void actualizarDatos()
        {

            gridProductos.DataSource = Producto.listar();
            gridProductos.Refresh();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            reportes.CrystalReport1 reporte = new reportes.CrystalReport1();
            reporte.SetDataSource(Producto.listar());
            reporte.Refresh();

            //instanciar el formulario donde se mosstrarán los reportes
            reportes.formVisorReportes formalario = new reportes.formVisorReportes();

            //pasamos el reporte al visor
            formalario.visor.ReportSource = reporte;
            formalario.visor.Refresh();

            formalario.ShowDialog();
        }
        /// <summary>
        /// Genera un archivo fisico del reporte
        /// </summary>
        /// <param name="extension"> utilice: XLS o PDF </param>
        private void exportardatos(string extension)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = $"Archivos en formato {extension.ToUpper()}|*.{extension.ToLower()}";
            sd.Title = "Reporte de porductos";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                //Creamos una versión del reporte en formato PDF 
                //1. Instancia del objeto reporte
                reportes.CrystalReport1 reporte = new reportes.CrystalReport1();
                //2. Asignar los datos al objeto reporte
                reporte.SetDataSource(Producto.listar());
                reporte.Refresh();
                //3 el reporte creado y en memoria se escribirá en el storage
                if (extension.ToUpper()=="PDF")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sd.FileName);
                }
                else if (extension.ToUpper()=="XLSX")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, sd.FileName);
                }
                
                Aviso.informar("Se ha creado el reporte correctamente");
            }
        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            exportardatos("PDF");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            exportardatos("XLSX");
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}