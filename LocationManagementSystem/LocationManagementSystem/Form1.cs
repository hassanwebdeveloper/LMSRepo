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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchString = this.tbxSearch.Text;

            bool isNicNumber = this.IsNicNumber(searchString);
            CCFTCentral ccftCentral = new CCFTCentral(); ;


            if (isNicNumber)
            {
                Cardholder cardHolder = (from pds in ccftCentral.PersonalDataStrings
                                         where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == searchString
                                         select pds.Cardholder).FirstOrDefault();
            }
            else
            {
                bool isDigitOnly = this.IsDigitsOnly(searchString);

                if (isDigitOnly)
                {

                }
                else
                {

                }
            }
        }

        private bool IsNicNumber(string str)
        {
            string[] split = str.Split('-');

            bool isNic = split.Length == 3 && split[0].Length == 5 && split[1].Length == 7 && split[2].Length == 1;

            if (isNic)
            {
                foreach (string splitString in split)
                {
                    foreach (char c in splitString)
                    {
                        if (!Char.IsDigit(c))
                        {
                            isNic = false;
                            break;
                        }
                    }

                    if (!isNic)
                    {
                        break;
                    }
                }
            }

            return isNic;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }
}
