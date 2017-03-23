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
            CardHolderInfo cardHolderInfo = null;
            EFERTDbContext efertDb = new EFERTDbContext();

            if (isNicNumber)
            {
                Task<Cardholder> cardHolderByNicTask = new Task<Cardholder>(() =>
                {
                    Cardholder cardHolderByNic = (from pds in ccftCentral.PersonalDataStrings
                                                  where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == searchString
                                                  select pds.Cardholder).FirstOrDefault();

                    return cardHolderByNic;
                });

                cardHolderByNicTask.Start();

                cardHolderInfo = (from card in efertDb.CardHolders
                                  where card != null && card.CNICNumber == searchString
                                  select card).FirstOrDefault();

                if (cardHolderInfo == null)
                {
                    cardHolder = cardHolderByNicTask.Result;
                }
            }
            else
            {
                MessageBox.Show(this, "Please enter some valid CNIC number.");
                return;
                //bool isDigitOnly = this.IsDigitsOnly(searchString);

                //if (isDigitOnly)
                //{
                //    cardHolder = (from c in ccftCentral.Cardholders
                //                  where c != null && c.LastName == searchString
                //                  select c).FirstOrDefault();
                //}
                //else
                //{
                //    cardHolder = (from c in ccftCentral.Cardholders
                //                  where c != null && c.FirstName == searchString
                //                  select c).FirstOrDefault();
                //}
            }

            if (cardHolder == null && cardHolderInfo == null)
            {

            }
            else
            {
                if (cardHolderInfo == null)
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
                            string bloodGroup = chPds.ContainsKey(5047) && chPds[5047] != null ? chPds[5047] : string.Empty;
                            string CNICNumber = chPds.ContainsKey(5051) && chPds[5051] != null ? chPds[5051] : string.Empty;
                            string crew = chPds.ContainsKey(12869) && chPds[12869] != null ? chPds[12869] : string.Empty;
                            string department = chPds.ContainsKey(5043) && chPds[5043] != null ? chPds[5043] : string.Empty;
                            string designation = chPds.ContainsKey(5042) && chPds[5042] != null ? chPds[5042] : string.Empty;
                            string contactNumber = chPds.ContainsKey(5053) && chPds[5053] != null ? chPds[5053] : string.Empty;
                            string section = chPds.ContainsKey(12951) && chPds[12951] != null ? chPds[12951] : string.Empty;

                            PermamentCardHolder permanentCh = new PermamentCardHolder()
                            {
                                FirstName = cardHolder.FirstName,
                                LastName = cardHolder.LastName,
                                BloodGroup = string.IsNullOrEmpty(bloodGroup) ? "Blood Group Not Found." : bloodGroup,
                                Cadre = cadre,
                                CardNumber = cardHolder.LastName,
                                CNICNumber = string.IsNullOrEmpty(CNICNumber) ? "CINC Not Found." : CNICNumber,
                                Crew = string.IsNullOrEmpty(crew) ? "Crew Not Found." : crew,
                                Department = string.IsNullOrEmpty(department) ? "Department Not Found." : department,
                                Designation = string.IsNullOrEmpty(designation) ? "Designation Not Found." : designation,
                                EmergancyContactNumber = string.IsNullOrEmpty(contactNumber) ? "Contact Number Not Found." : contactNumber,
                                Section = string.IsNullOrEmpty(section) ? "Section Not Found." : section,
                                PNumber = strPNumber,
                                DateOfBirth = strDOB
                            };

                            PermanentChForm permanentForm = new PermanentChForm(permanentCh);

                            Task saveTask = new Task(() =>
                            {


                                CadreInfo cadreInfo = (from c in efertDb.Cadres
                                                       where c != null && c.CadreName == cadre
                                                       select c).FirstOrDefault() ?? new CadreInfo() { CadreName = cadre };

                                CrewInfo crewInfo = string.IsNullOrEmpty(crew) ? null :
                                                     ((from c in efertDb.Crews
                                                       where c != null && c.CrewName == crew
                                                       select c).FirstOrDefault() ?? new CrewInfo() { CrewName = crew });

                                DepartmentInfo departmentInfo = string.IsNullOrEmpty(department) ? null :
                                                                ((from c in efertDb.Departments
                                                                  where c != null && c.DepartmentName == department
                                                                  select c).FirstOrDefault() ?? new DepartmentInfo() { DepartmentName = department });

                                DesignationInfo designationInfo = string.IsNullOrEmpty(designation) ? null :
                                                                ((from c in efertDb.Designations
                                                                  where c != null && c.Designation == designation
                                                                  select c).FirstOrDefault() ?? new DesignationInfo() { Designation = designation });

                                SectionInfo sectionInfo = string.IsNullOrEmpty(section) ? null :
                                                        ((from c in efertDb.Sections
                                                          where c != null && c.SectionName == section
                                                          select c).FirstOrDefault() ?? new SectionInfo() { SectionName = section });


                                CardHolderInfo cardHolerInfo = new CardHolderInfo()
                                {
                                    FirstName = permanentCh.FirstName,
                                    LastName = permanentCh.LastName,
                                    BloodGroup = string.IsNullOrEmpty(bloodGroup) ? null : permanentCh.BloodGroup,
                                    Cadre = cadreInfo,
                                    CardNumber = permanentCh.LastName,
                                    CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : permanentCh.CNICNumber,
                                    Crew = crewInfo,
                                    Department = departmentInfo,
                                    Designation = designationInfo,
                                    EmergancyContactNumber = string.IsNullOrEmpty(contactNumber) ? null : permanentCh.EmergancyContactNumber,

                                    Section = sectionInfo,
                                    PNumber = pNumber == null ? null : pNumber.ToString(),
                                    DateOfBirth = dateOfBirth == null ? null : dateOfBirth.ToString()
                                };

                                efertDb.CardHolders.Add(cardHolerInfo);

                                efertDb.SaveChanges();

                            });

                            saveTask.Start();

                            permanentForm.Show();
                        }
                        else
                        {
                            string companyName = chPds.ContainsKey(5059) && chPds[5059] != null ? chPds[5059] : string.Empty;
                            string CNICNumber = chPds.ContainsKey(5051) && chPds[5051] != null ? chPds[5051] : string.Empty;
                            string department = chPds.ContainsKey(5043) && chPds[5043] != null ? chPds[5043] : string.Empty;
                            string designation = chPds.ContainsKey(5042) && chPds[5042] != null ? chPds[5042] : string.Empty;
                            string emergancyContactNumber = chPds.ContainsKey(5053) && chPds[5053] != null ? chPds[5053] : string.Empty;
                            string section = chPds.ContainsKey(12951) && chPds[12951] != null ? chPds[12951] : string.Empty;
                            string wONumber = chPds.ContainsKey(5344) && chPds[5344] != null ? chPds[5344] : string.Empty;

                            ContractorCardHolder contratorCh = new ContractorCardHolder()
                            {
                                FirstName = cardHolder.FirstName,
                                LastName = cardHolder.LastName,
                                CompanyName = string.IsNullOrEmpty(companyName) ? "Company Name Not Found." : companyName,
                                Cadre = cadre,
                                CardNumber = cardHolder.LastName,
                                CNICNumber = string.IsNullOrEmpty(CNICNumber) ? "CINC Not Found." : CNICNumber,
                                Department = string.IsNullOrEmpty(department) ? "Department Not Found." : department,
                                Designation = string.IsNullOrEmpty(designation) ? "Designation Not Found." : designation,
                                EmergancyContactNumber = string.IsNullOrEmpty(emergancyContactNumber) ? "Contact Number Not Found." : emergancyContactNumber,
                                Section = string.IsNullOrEmpty(section) ? "Section Not Found." : section,
                                WONumber = string.IsNullOrEmpty(wONumber) ? "WO No. Not Found." : wONumber
                            };

                            ContractorChForm contractorForm = new ContractorChForm(contratorCh);

                            Task saveTask = new Task(() =>
                            {


                                CadreInfo cadreInfo = (from c in efertDb.Cadres
                                                       where c != null && c.CadreName == cadre
                                                       select c).FirstOrDefault() ?? new CadreInfo() { CadreName = cadre };

                                CrewInfo crewInfo = null;

                                DepartmentInfo departmentInfo = string.IsNullOrEmpty(department) ? null :
                                                                ((from c in efertDb.Departments
                                                                  where c != null && c.DepartmentName == department
                                                                  select c).FirstOrDefault() ?? new DepartmentInfo() { DepartmentName = department });

                                DesignationInfo designationInfo = string.IsNullOrEmpty(designation) ? null :
                                                                ((from c in efertDb.Designations
                                                                  where c != null && c.Designation == designation
                                                                  select c).FirstOrDefault() ?? new DesignationInfo() { Designation = designation });

                                SectionInfo sectionInfo = string.IsNullOrEmpty(section) ? null :
                                                        ((from c in efertDb.Sections
                                                          where c != null && c.SectionName == section
                                                          select c).FirstOrDefault() ?? new SectionInfo() { SectionName = section });

                                CompanyInfo companyInfo = string.IsNullOrEmpty(companyName) ? null :
                                                        ((from c in efertDb.Companies
                                                          where c != null && c.CompanyName == companyName
                                                          select c).FirstOrDefault() ?? new CompanyInfo() { CompanyName = companyName });

                                CardHolderInfo cardHolerInfo = new CardHolderInfo()
                                {
                                    FirstName = contratorCh.FirstName,
                                    LastName = contratorCh.LastName,
                                    Company = companyInfo,
                                    Cadre = cadreInfo,
                                    CardNumber = contratorCh.LastName,
                                    CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : CNICNumber,
                                    Crew = crewInfo,
                                    Department = departmentInfo,
                                    Designation = designationInfo,
                                    EmergancyContactNumber = string.IsNullOrEmpty(emergancyContactNumber) ? null : emergancyContactNumber,

                                    Section = sectionInfo,
                                    WONumber = string.IsNullOrEmpty(wONumber) ? null : wONumber,
                                };

                                efertDb.CardHolders.Add(cardHolerInfo);

                                efertDb.SaveChanges();

                            });

                            saveTask.Start();

                            contractorForm.Show();
                        }
                    }
                }
                else
                {
                    string cadre = cardHolderInfo.Cadre== null ? "" : cardHolderInfo.Cadre.CadreName;

                    bool isPermanent = cadre.ToLower() == "nmpt" || cadre.ToLower() == "mpt";

                    if (isPermanent)
                    {                        
                        string strPNumber = string.IsNullOrEmpty(cardHolderInfo.PNumber) ? "P-Number not found." : cardHolderInfo.PNumber;

                        string strDOB = string.IsNullOrEmpty(cardHolderInfo.DateOfBirth) ? "Date of birth not found." : cardHolderInfo.DateOfBirth;
                        string bloodGroup = cardHolderInfo.BloodGroup;
                        string CNICNumber = cardHolderInfo.CNICNumber;
                        string crew = cardHolderInfo.Crew == null ? "" : cardHolderInfo.Crew.CrewName;
                        string department = cardHolderInfo.Department == null ? "" : cardHolderInfo.Department.DepartmentName;
                        string designation = cardHolderInfo.Designation == null ? "" : cardHolderInfo.Designation.Designation ;
                        string contactNumber = cardHolderInfo.EmergancyContactNumber;
                        string section = cardHolderInfo.Section == null ? "" : cardHolderInfo.Section.SectionName;

                        PermamentCardHolder permanentCh = new PermamentCardHolder()
                        {
                            FirstName = cardHolderInfo.FirstName,
                            LastName = cardHolderInfo.LastName,
                            BloodGroup = string.IsNullOrEmpty(bloodGroup) ? "Blood Group Not Found." : bloodGroup,
                            Cadre = cadre,
                            CardNumber = cardHolderInfo.LastName,
                            CNICNumber = string.IsNullOrEmpty(CNICNumber) ? "CINC Not Found." : CNICNumber,
                            Crew = string.IsNullOrEmpty(crew) ? "Crew Not Found." : crew,
                            Department = string.IsNullOrEmpty(department) ? "Department Not Found." : department,
                            Designation = string.IsNullOrEmpty(designation) ? "Designation Not Found." : designation,
                            EmergancyContactNumber = string.IsNullOrEmpty(contactNumber) ? "Contact Number Not Found." : contactNumber,
                            Section = string.IsNullOrEmpty(section) ? "Section Not Found." : section,
                            PNumber = strPNumber,
                            DateOfBirth = strDOB
                        };

                        PermanentChForm permanentForm = new PermanentChForm(permanentCh);
                        

                        permanentForm.Show();
                    }
                    else
                    {
                        string companyName = cardHolderInfo.Company == null ? "" : cardHolderInfo.Company.CompanyName;
                        string CNICNumber = cardHolderInfo.CNICNumber;
                        string department = cardHolderInfo.Department == null ? "" : cardHolderInfo.Department.DepartmentName;
                        string designation = cardHolderInfo.Designation == null ? "" : cardHolderInfo.Designation.Designation;
                        string emergancyContactNumber = cardHolderInfo.EmergancyContactNumber;
                        string section = cardHolderInfo.Section == null ? "" : cardHolderInfo.Section.SectionName;
                        string wONumber = cardHolderInfo.WONumber;

                        ContractorCardHolder contratorCh = new ContractorCardHolder()
                        {
                            FirstName = cardHolderInfo.FirstName,
                            LastName = cardHolderInfo.LastName,
                            CompanyName = string.IsNullOrEmpty(companyName) ? "Company Name Not Found." : companyName,
                            Cadre = cadre,
                            CardNumber = cardHolderInfo.LastName,
                            CNICNumber = string.IsNullOrEmpty(CNICNumber) ? "CINC Not Found." : CNICNumber,
                            Department = string.IsNullOrEmpty(department) ? "Department Not Found." : department,
                            Designation = string.IsNullOrEmpty(designation) ? "Designation Not Found." : designation,
                            EmergancyContactNumber = string.IsNullOrEmpty(emergancyContactNumber) ? "Contact Number Not Found." : emergancyContactNumber,
                            Section = string.IsNullOrEmpty(section) ? "Section Not Found." : section,
                            WONumber = string.IsNullOrEmpty(wONumber) ? "WO No. Not Found." : wONumber
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
