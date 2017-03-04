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
    public partial class SearchForm : Form
    {

        public SearchForm()
        {
            InitializeComponent();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = this.tbxSearch.Text;

            bool isNicNumber = this.IsNicNumber(searchString);
            CCFTCentral ccftCentral = new CCFTCentral(); ;
            Cardholder cardHolder = null;

            if (isNicNumber)
            {
                cardHolder = (from pds in ccftCentral.PersonalDataStrings
                              where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == searchString
                              select pds.Cardholder).FirstOrDefault();
            }
            else
            {
                bool isDigitOnly = this.IsDigitsOnly(searchString);

                if (isDigitOnly)
                {
                    cardHolder = (from c in ccftCentral.Cardholders
                                  where c != null && c.LastName == searchString
                                  select c).FirstOrDefault();
                }
                else
                {
                    cardHolder = (from c in ccftCentral.Cardholders
                                  where c != null && c.FirstName == searchString
                                  select c).FirstOrDefault();
                }
            }

            if (cardHolder == null)
            {

            }
            else
            {
                Dictionary<int, string> chPds = new Dictionary<int, string>();

                foreach (PersonalDataString pds in cardHolder.PersonalDataStrings)
                {
                    if (pds != null)
                    {
                        chPds.Add(pds.PersonalDataFieldID, pds.Value);
                    }
                }

                string cadre = (from c in chPds
                                where c.Key == 12952 && c.Value != null
                                select c.Value).FirstOrDefault();

                if (string.IsNullOrEmpty(cadre))
                {
                    MessageBox.Show(this, "No Cadre found.");
                }
                else
                {
                    bool isPermanent = cadre.ToLower() == "nmpt" || cadre.ToLower() == "mpt";

                    if (isPermanent)
                    {
                        int? pNumber = cardHolder.PersonalDataIntegers == null || cardHolder.PersonalDataIntegers.Count == 0 ? null : cardHolder.PersonalDataIntegers.ElementAt(0).Value;
                        string strPNumber = pNumber == null ? "P-Number not found." : pNumber.ToString();

                        DateTime? dateOfBirth = cardHolder.PersonalDataDates == null || cardHolder.PersonalDataDates.Count == 0 ? null : cardHolder.PersonalDataDates.ElementAt(0).Value;
                        string strDOB = dateOfBirth == null ? "Date of birth not found." : dateOfBirth.ToString();

                        PermamentCardHolder permanentCh = new PermamentCardHolder()
                        {
                            FirstName = cardHolder.FirstName,
                            LastName = cardHolder.LastName,
                            BloodGroup = chPds.ContainsKey(5047) && chPds[5047] != null ? chPds[5047] : "Blood Group Not Found.",
                            Cadre = cadre,
                            CardNumber = cardHolder.LastName,
                            CNICNumber = chPds.ContainsKey(5051) && chPds[5051] != null ? chPds[5051] : "CINC Not Found.",
                            Crew = chPds.ContainsKey(12869) && chPds[12869] != null ? chPds[12869] : "Crew Not Found.",
                            Department = chPds.ContainsKey(5043) && chPds[5043] != null ? chPds[5043] : "Department Not Found.",
                            Designation = chPds.ContainsKey(5042) && chPds[5042] != null ? chPds[5042] : "Designation Not Found.",
                            EmergancyContactNumber = chPds.ContainsKey(5053) && chPds[5053] != null ? chPds[5053] : "Contact Number Not Found.",
                            Section = chPds.ContainsKey(12951) && chPds[12951] != null ? chPds[12951] : "Section Not Found.",
                            PNumber = strPNumber,
                            DateOfBirth = strDOB
                        };

                        PermanentChForm permanentForm = new PermanentChForm(permanentCh);
                        permanentForm.Show();
                    }
                    else
                    {
                        ContractorCardHolder contratorCh = new ContractorCardHolder()
                        {
                            FirstName = cardHolder.FirstName,
                            LastName = cardHolder.LastName,
                            CompanyName = chPds.ContainsKey(5059) && chPds[5059] != null ? chPds[5059] : "Company Name Not Found.",
                            Cadre = cadre,
                            CardNumber = cardHolder.LastName,
                            CNICNumber = chPds.ContainsKey(5051) && chPds[5051] != null ? chPds[5051] : "CINC Not Found.",
                            Department = chPds.ContainsKey(5043) && chPds[5043] != null ? chPds[5043] : "Department Not Found.",
                            Designation = chPds.ContainsKey(5042) && chPds[5042] != null ? chPds[5042] : "Designation Not Found.",
                            EmergancyContactNumber = chPds.ContainsKey(5053) && chPds[5053] != null ? chPds[5053] : "Contact Number Not Found.",
                            Section = chPds.ContainsKey(12951) && chPds[12951] != null ? chPds[12951] : "Section Not Found.",
                            WONumber = chPds.ContainsKey(5344) && chPds[5344] != null ? chPds[5344] : "WO No. Not Found."
                        };

                        ContractorChForm contractorForm = new ContractorChForm(contratorCh);
                        contractorForm.Show();
                    }
                }
            }
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form1.mMainForm.Close();
        }
    }
}
