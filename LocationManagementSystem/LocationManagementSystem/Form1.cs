using LocationManagementSystem.CCFTCentralDb;
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
    public partial class Form1 : Form
    {
        public static Form1 mMainForm = null;
        public static AppUser mLoggedInUser = null;

        public Form1()
        {
            InitializeComponent();
            mMainForm = this;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            EFERTDbContext efertDb = new EFERTDbContext();

            string userName = this.tbxUserName.Text;
            string password = this.tbxPassword.Text;

            AppUser loggedInUser = (from user in efertDb.AppUsers
                                    where user != null && user.UserName == userName && user.Password == password
                                    select user).FirstOrDefault();

            if (loggedInUser == null)
            {
                MessageBox.Show(this, "Either username or password is incorrect.");
            }
            else
            {
                mLoggedInUser = loggedInUser;

                SearchForm searchForm = new SearchForm();

                searchForm.Show();

                this.Hide();
            }

            
        }
    }
}
