using System;
using System.Windows.Forms;

namespace GP.WFapp
{
    public partial class Main : Form
    {
        private frmValorEditar _frmValorEditar;
        private FrmFactorEditar _frmFactorEditar;
        private FrmFactorListar _frmFactorListar;
        private FrmValorListar _frmValorListar;

        public Main()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmFactorEditar != null)
                _frmFactorEditar.Dispose();
                
            _frmFactorEditar = new FrmFactorEditar(0,this);

            _frmFactorEditar.MdiParent = this;
             
            _frmFactorEditar.Show();
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmFactorListar != null)
                _frmFactorListar.Dispose();
                
            _frmFactorListar = new FrmFactorListar(this);

            _frmFactorListar.MdiParent = this;

            _frmFactorListar.Show();
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_frmValorEditar != null)
                _frmValorEditar.Dispose();
                
            _frmValorEditar = new frmValorEditar(0,this);

            _frmValorEditar.MdiParent = this;

            _frmValorEditar.Show();
        }

        private void listarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_frmValorListar != null)
            {
                _frmValorListar.Dispose();      
            }

            _frmValorListar = new FrmValorListar(this);
            _frmValorListar.MdiParent = this;

            _frmValorListar.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

    }
}
