using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GP.DTO.DTO;
using GP.Gestores.Gestores;
using GP.MVP.Presenters;
using GP.MVP.Views;

namespace GP.WFapp
{
    public partial class FrmFactorListar : Form, IFactorListarView
    {
        private readonly FactorListarPresenter _presenter;
        private Main _main;

        public FrmFactorListar(Main main) : this()
        {
            _main = main;
        }

        public FrmFactorListar()
        {
            try
            {
                InitializeComponent();

                _presenter = new FactorListarPresenter(this, new FactorGestor());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public event Action BuscarFactores;
        public event Action Seleccionado;

        public IList<FactorDTO> Factores
        {
            set
            {
                dgvFactores.DataSource = null;
                dgvFactores.Rows.Clear();

                dgvFactores.DataSource = value;

                var gridViewColumn = dgvFactores.Columns["Deshabilitado"];
                if (gridViewColumn != null) gridViewColumn.Visible = false;
                var dataGridViewColumn = dgvFactores.Columns["ValoresSeleccionados"];
                if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            }
        }

        public int FactorSeleccionado
        {
            get{
               return (dgvFactores.SelectedRows.Count > 0) ? (int) dgvFactores.SelectedRows[0].Cells[0].Value : 0;
            }
        }

        public string FiltroNombre { 
            get { return txtNombre.Text.Trim(); }
        }

        public void MostrarDetalleFactor(FactorDTO factor)
        {
            dgValores.DataSource = factor.ValoresSeleccionados;

            dgValores.Columns["Deshabilitado"].Visible = false;
            dgValores.Columns["ValorId"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarFactores();
        }

        private void FrmFactorListar_Load(object sender, EventArgs e)
        {
            try{

                _presenter.Init();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvFactores_SelectionChanged(object sender, EventArgs e)
        {
            Seleccionado();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.FactorSeleccionado == 0)
                MessageBox.Show("Debe seleccionar un factor");
            else
            {
                var form = new FrmFactorEditar(this.FactorSeleccionado);
                form.Show();
                this.Close();
            }
        }

        private void FrmFactorListar_Activated(object sender, EventArgs e)
        {
            try
            {
                BuscarFactores();
            }
            catch (Exception x)
            {
                
                throw x;
            }
           
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var form = new FrmFactorEditar(0);
            form.Show();
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
