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
    public partial class PermanentChForm : Form
    {
        public PermanentChForm(PermamentCardHolder permanentCh)
        {
            InitializeComponent();

            this.tbxCardNumber.Text = permanentCh.CardNumber;
            this.tbxFirstName.Text = permanentCh.FirstName;
            this.tbxBloodGroup.Text = permanentCh.BloodGroup;
            this.tbxCadre.Text = permanentCh.Cadre;
            this.tbxCrew.Text = permanentCh.Crew;
            this.tbxDob.Text = permanentCh.DateOfBirth;
            this.tbxDepartment.Text = permanentCh.Department;
            this.tbxDesignation.Text = permanentCh.Designation;
            this.tbxContactNumber.Text = permanentCh.EmergancyContactNumber;
            this.tbxCNICNumber.Text = permanentCh.CNICNumber;
            this.tbxPNumber.Text = permanentCh.PNumber;
            this.tbxSection.Text = permanentCh.Section;
            this.tbxLastName.Text = permanentCh.LastName;
            //this.tbxCardNumber.Text = permanentCh.CardNumber;
        }
        
        
    }
}
