using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationManagementSystem
{
    public partial class LocationSelectorForm : Form
    {
        public LocationSelectorForm()
        {
            InitializeComponent();

            this.linkLabel1.Visible = Form1.mLoggedInUser.IsAdmin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(true);

            searchForm.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(false);

            searchForm.Show();

            this.Hide();
        }

        private void LocationSelectorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.mMainForm.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateLocationsForm updateLocationsForms = new UpdateLocationsForm();

            updateLocationsForms.ShowDialog(this);
        }
    }
}
