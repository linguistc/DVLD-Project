using DVLDBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;
using DVLD.User;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private static DataTable _dtAllApplicationTypes;


        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();

            dgvApplications.DataSource = _dtAllApplicationTypes;
            lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();

            if (dgvApplications.Rows.Count > 0)
            {
                dgvApplications.Columns[0].HeaderText = "ID";
                dgvApplications.Columns[0].Width = 110;

                dgvApplications.Columns[1].HeaderText = "Title";
                dgvApplications.Columns[1].Width = 350;

                dgvApplications.Columns[2].HeaderText = "Fees";
                dgvApplications.Columns[2].Width = 120;
            }
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType Frm1 = new frmEditApplicationType((int)dgvApplications.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
            frmListApplicationTypes_Load(null, null);
        }
    }
}
