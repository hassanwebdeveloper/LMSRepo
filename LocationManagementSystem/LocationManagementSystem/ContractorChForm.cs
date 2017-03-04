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
    public partial class ContractorChForm : Form
    {
        public ContractorChForm(ContractorCardHolder contractor)
        {
            InitializeComponent();

            this.tbxCardNumber.Text = contractor.CardNumber;
            this.tbxFirstName.Text = contractor.FirstName;
            this.tbxCompanyName.Text = contractor.CompanyName;
            this.tbxCadre.Text = contractor.Cadre;
            this.tbxDepartment.Text = contractor.Department;
            this.tbxDesignation.Text = contractor.Designation;
            this.tbxContactNumber.Text = contractor.EmergancyContactNumber;
            this.tbxCNICNumber.Text = contractor.CNICNumber;
            this.tbxWONumber.Text = contractor.WONumber;
            this.tbxSection.Text = contractor.Section;
            this.tbxLastName.Text = contractor.LastName;
            //this.tbxCardNumber.Text = permanentCh.CardNumber;
        }
    }
}
