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
    public partial class UpdateLocationsForm : Form
    {
        private List<VisitingLocations> mLstPlantLocations = null;
        private List<VisitingLocations> mLstColonyLocations = null;

        public UpdateLocationsForm()
        {
            InitializeComponent();

            this.visitingLocationsBindingSource.DataSource = EFERTDbUtility.mVisitingLocations.FindAll(location => location.IsOnPlant);
            mLstPlantLocations = (this.visitingLocationsBindingSource.DataSource as List<VisitingLocations>);
            

            this.visitingLocationsBindingSource1.DataSource = EFERTDbUtility.mVisitingLocations.FindAll(location => !location.IsOnPlant);

            mLstColonyLocations = (this.visitingLocationsBindingSource1.DataSource as List<VisitingLocations>);
            
        }

        private void dgvPlantLocations_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.dgvPlantLocations.Rows[e.RowIndex].Tag = mLstPlantLocations[e.RowIndex];
        }

        private void dgvColonyLocations_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.dgvColonyLocations.Rows[e.RowIndex].Tag = mLstColonyLocations[e.RowIndex];
        }
        
        private void dgvPlantLocations_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                string newValue = e.Row.Cells[1].Value as string;

                VisitingLocations newLoacation = new VisitingLocations()
                {
                    IsOnPlant = true,
                    Location = newValue
                };

                EFERTDbUtility.mEFERTDb.VisitingLocations.Add(newLoacation);

                EFERTDbUtility.mEFERTDb.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Some error occurred in deleting location.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
            }
        }

        private void dgvPlantLocations_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                VisitingLocations visitingLocations = e.Row.Tag as VisitingLocations;

                EFERTDbUtility.mEFERTDb.Entry(visitingLocations).State = System.Data.Entity.EntityState.Deleted;

                EFERTDbUtility.mEFERTDb.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Some error occurred in deleting location.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));

                e.Cancel = true;
            }
        }

        private void dgvColonyLocations_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                string newValue = e.Row.Cells[1].Value as string;

                VisitingLocations newLoacation = new VisitingLocations()
                {
                    IsOnPlant = false,
                    Location = newValue
                };

                EFERTDbUtility.mEFERTDb.VisitingLocations.Add(newLoacation);

                EFERTDbUtility.mEFERTDb.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Some error occurred in deleting location.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
            }
            
        }

        private void dgvColonyLocations_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                VisitingLocations visitingLocations = e.Row.Tag as VisitingLocations;

                EFERTDbUtility.mEFERTDb.Entry(visitingLocations).State = System.Data.Entity.EntityState.Deleted;

                EFERTDbUtility.mEFERTDb.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Some error occurred in deleting location.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));

                e.Cancel = true;
            }
        }

        
    }
}
