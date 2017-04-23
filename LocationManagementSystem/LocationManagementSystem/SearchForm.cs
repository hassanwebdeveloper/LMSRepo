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
        public static bool mIsPlant = false;

        public SearchForm(bool isPlant)
        {
            mIsPlant = isPlant;
            InitializeComponent();

            this.maskedTextBox1.Select();
            if (isPlant)
            {
                this.lblLocation.Text = "Plant";
            }
            else
            {
                this.lblLocation.Text = "Colony";
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = this.maskedTextBox1.Text;

            bool isNicNumber = this.maskedTextBox1.Mask == "00000-0000000-0";
            CCFTCentral ccftCentral = EFERTDbUtility.mCCFTCentral;
            Cardholder cardHolder = null;
            CardHolderInfo cardHolderInfo = null;
            VisitorCardHolder visitor = null;
            DailyCardHolder dailyCardHolder = null;
            bool updatedCardExist = true;

            if (isNicNumber)
            {
                if (this.maskedTextBox1.MaskCompleted)
                {
                    Task<Cardholder> cardHolderByNicTask = new Task<Cardholder>(() =>
                    {
                        Cardholder cardHolderByNic = (from pds in ccftCentral.PersonalDataStrings
                                                      where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == searchString
                                                      select pds.Cardholder).FirstOrDefault();

                        return cardHolderByNic;
                    });

                    cardHolderByNicTask.Start();

                    cardHolderInfo = (from card in EFERTDbUtility.mEFERTDb.CardHolders
                                      where card != null && card.CNICNumber == searchString
                                      select card).FirstOrDefault();

                    if (cardHolderInfo == null)
                    {
                        cardHolder = cardHolderByNicTask.Result;

                        if (cardHolder == null)
                        {
                            visitor = (from visit in EFERTDbUtility.mEFERTDb.Visitors
                                       where visit != null && visit.CNICNumber == searchString
                                       select visit).FirstOrDefault();

                            if (visitor == null)
                            {
                                dailyCardHolder = (from daily in EFERTDbUtility.mEFERTDb.DailyCardHolders
                                                   where daily != null && daily.CNICNumber == searchString
                                                   select daily).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {
                        if (cardHolderInfo.IsTemp)
                        {
                            cardHolder = cardHolderByNicTask.Result;

                            if (cardHolder == null)
                            {
                                updatedCardExist = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Please enter some valid CNIC number.");
                    return;
                }
            }
            else
            {

                Task<Cardholder> cardHolderByCardNumberTask = new Task<Cardholder>(() =>
                {
                    Cardholder cardHolderByCardNumber = (from c in ccftCentral.Cardholders
                                                        where c != null && c.LastName == searchString
                                                        select c).FirstOrDefault();

                    return cardHolderByCardNumber;
                });

                cardHolderByCardNumberTask.Start();

                cardHolderInfo = (from card in EFERTDbUtility.mEFERTDb.CardHolders
                                  where card != null && card.CardNumber == searchString
                                  select card).FirstOrDefault();

                if (cardHolderInfo == null)
                {
                    CheckInAndOutInfo cardIssued = (from checkIn in EFERTDbUtility.mEFERTDb.CheckedInInfos
                                                    where checkIn != null && checkIn.CheckedIn && checkIn.CardNumber == searchString
                                                    select checkIn).FirstOrDefault();

                    if (cardIssued != null)
                    {
                        visitor = cardIssued.Visitors;

                        if (visitor == null)
                        {
                            dailyCardHolder = cardIssued.DailyCardHolders;

                            if (dailyCardHolder == null)
                            {
                                cardHolderInfo = cardIssued.CardHolderInfos;

                                if (cardHolderInfo != null && cardHolderInfo.IsTemp)
                                {
                                    cardHolder = (from pds in ccftCentral.PersonalDataStrings
                                                where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == cardIssued.CNICNumber
                                                  select pds.Cardholder).FirstOrDefault();

                                    if (cardHolder != null)
                                    {
                                        updatedCardExist = true;
                                    }
                                    else
                                    {
                                        updatedCardExist = false;
                                    }
                                }
                            }
                        }
                    }
                    

                    if (visitor == null && dailyCardHolder == null && cardHolderInfo == null)
                    {
                        cardHolder = cardHolderByCardNumberTask.Result;
                    }

                    if (visitor == null && dailyCardHolder == null && cardHolder == null && cardHolderInfo == null)
                    {
                        MessageBox.Show(this, "Cardholder with " + searchString + " card number is not found.");
                        return;
                    }

                }
                //else
                //{
                //    if (!cardHolderInfo.GallagherCardHolder)
                //    {
                //        cardHolder = cardHolderByNicTask.Result;

                //        if (cardHolder == null)
                //        {
                //            updatedCardExist = false;
                //        }
                //    }
                //}
                //bool isDigitOnly = this.IsDigitsOnly(searchString);

                //if (isDigitOnly)
                //{
                //cardHolder = (from c in ccftCentral.Cardholders
                //              where c != null && c.LastName == searchString
                //              select c).FirstOrDefault();
                //}
                //else
                //{
                //    cardHolder = (from c in ccftCentral.Cardholders
                //                  where c != null && c.FirstName == searchString
                //                  select c).FirstOrDefault();
                //}
            }

            if (cardHolder == null && cardHolderInfo == null && visitor == null && dailyCardHolder == null)
            {
                if (mIsPlant)
                {
                    NewPlantChForm npchf = new NewPlantChForm(searchString);

                    npchf.ShowDialog(this);
                }
                else
                {
                    NewColonyChForm ncchf = new NewColonyChForm(searchString);

                    ncchf.ShowDialog(this);


                }
            }
            else
            {
                if (cardHolderInfo != null && !cardHolderInfo.IsTemp)
                {
                    string cadre = cardHolderInfo.Cadre == null ? "" : cardHolderInfo.Cadre.CadreName;

                    bool isPermanent = cadre.ToLower() == "nmpt" || cadre.ToLower() == "mpt";

                    if (isPermanent)
                    {                        
                        PermanentChForm permanentForm = new PermanentChForm(cardHolderInfo);
                        permanentForm.Show();
                    }
                    else
                    {
                        ContractorChForm contractorForm = new ContractorChForm(cardHolderInfo);
                        contractorForm.Show();
                    }
                }
                else if (cardHolder != null)
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
                            int cardHolderId = cardHolder.FTItemID;

                            CadreInfo cadreInfo = (from c in EFERTDbUtility.mEFERTDb.Cadres
                                                   where c != null && c.CadreName == cadre
                                                   select c).FirstOrDefault() ?? new CadreInfo() { CadreName = cadre };

                            CrewInfo crewInfo = string.IsNullOrEmpty(crew) ? null :
                                                 ((from c in EFERTDbUtility.mEFERTDb.Crews
                                                   where c != null && c.CrewName == crew
                                                   select c).FirstOrDefault() ?? new CrewInfo() { CrewName = crew });

                            DepartmentInfo departmentInfo = string.IsNullOrEmpty(department) ? null :
                                                            ((from c in EFERTDbUtility.mEFERTDb.Departments
                                                              where c != null && c.DepartmentName == department
                                                              select c).FirstOrDefault() ?? new DepartmentInfo() { DepartmentName = department });


                            DesignationInfo designationInfo = string.IsNullOrEmpty(designation) ? null :
                                                                ((from c in EFERTDbUtility.mEFERTDb.Designations
                                                                  where c != null && c.Designation == designation
                                                                  select c).FirstOrDefault() ?? new DesignationInfo() { Designation = designation });

                            SectionInfo sectionInfo = string.IsNullOrEmpty(section) ? null :
                                                    ((from c in EFERTDbUtility.mEFERTDb.Sections
                                                      where c != null && c.SectionName == section
                                                      select c).FirstOrDefault() ?? new SectionInfo() { SectionName = section });



                            if (cardHolderInfo != null && cardHolderInfo.IsTemp)
                            {
                                cardHolderInfo.FTItemId = cardHolderId;
                                cardHolderInfo.FirstName = cardHolder.FirstName;
                                cardHolderInfo.LastName = cardHolder.LastName;
                                cardHolderInfo.BloodGroup = string.IsNullOrEmpty(bloodGroup) ? null : bloodGroup;
                                cardHolderInfo.Cadre = cadreInfo;
                                cardHolderInfo.CardNumber = cardHolder.LastName;
                                cardHolderInfo.CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : CNICNumber;
                                cardHolderInfo.Crew = crewInfo;
                                cardHolderInfo.Department = departmentInfo;
                                cardHolderInfo.Designation = designationInfo;
                                cardHolderInfo.EmergancyContactNumber = string.IsNullOrEmpty(contactNumber) ? null : contactNumber;
                                cardHolderInfo.Section = sectionInfo;
                                cardHolderInfo.PNumber = pNumber == null ? null : pNumber.ToString();
                                cardHolderInfo.DateOfBirth = dateOfBirth == null ? null : dateOfBirth.ToString();
                                cardHolderInfo.IsTemp = false;

                                EFERTDbUtility.mEFERTDb.Entry(cardHolderInfo).State = System.Data.Entity.EntityState.Modified;
                            }
                            else
                            {
                                cardHolderInfo = new CardHolderInfo()
                                {
                                    FTItemId = cardHolderId,
                                    FirstName = cardHolder.FirstName,
                                    LastName = cardHolder.LastName,
                                    BloodGroup = string.IsNullOrEmpty(bloodGroup) ? null : bloodGroup,
                                    Cadre = cadreInfo,
                                    CardNumber = cardHolder.LastName,
                                    CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : CNICNumber,
                                    Crew = crewInfo,
                                    Department = departmentInfo,
                                    Designation = designationInfo,
                                    EmergancyContactNumber = string.IsNullOrEmpty(contactNumber) ? null : contactNumber,
                                    Section = sectionInfo,
                                    PNumber = pNumber == null ? null : pNumber.ToString(),
                                    DateOfBirth = dateOfBirth == null ? null : dateOfBirth.ToString(),
                                    IsTemp = false
                                };

                                EFERTDbUtility.mEFERTDb.CardHolders.Add(cardHolderInfo);

                            }

                            EFERTDbUtility.mEFERTDb.SaveChanges();

                            PermanentChForm permanentForm = new PermanentChForm(cardHolderInfo);
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
                            int cardHolderId = cardHolder.FTItemID;

                            CadreInfo cadreInfo = (from c in EFERTDbUtility.mEFERTDb.Cadres
                                                   where c != null && c.CadreName == cadre
                                                   select c).FirstOrDefault() ?? new CadreInfo() { CadreName = cadre };


                            CrewInfo crewInfo = null;

                            DepartmentInfo departmentInfo = string.IsNullOrEmpty(department) ? null :
                                                            ((from c in EFERTDbUtility.mEFERTDb.Departments
                                                              where c != null && c.DepartmentName == department
                                                              select c).FirstOrDefault() ?? new DepartmentInfo() { DepartmentName = department });


                            DesignationInfo designationInfo = string.IsNullOrEmpty(designation) ? null :
                                                            ((from c in EFERTDbUtility.mEFERTDb.Designations
                                                              where c != null && c.Designation == designation
                                                              select c).FirstOrDefault() ?? new DesignationInfo() { Designation = designation });

                            SectionInfo sectionInfo = string.IsNullOrEmpty(section) ? null :
                                                    ((from c in EFERTDbUtility.mEFERTDb.Sections
                                                      where c != null && c.SectionName == section
                                                      select c).FirstOrDefault() ?? new SectionInfo() { SectionName = section });

                            CompanyInfo companyInfo = string.IsNullOrEmpty(companyName) ? null :
                                                    ((from c in EFERTDbUtility.mEFERTDb.Companies
                                                      where c != null && c.CompanyName == companyName
                                                      select c).FirstOrDefault() ?? new CompanyInfo() { CompanyName = companyName });



                            if (cardHolderInfo != null && cardHolderInfo.IsTemp)
                            {
                                cardHolderInfo.FTItemId = cardHolderId;
                                cardHolderInfo.FirstName = cardHolder.FirstName;
                                cardHolderInfo.LastName = cardHolder.LastName;
                                cardHolderInfo.Company = companyInfo;
                                cardHolderInfo.Cadre = cadreInfo;
                                cardHolderInfo.CardNumber = cardHolder.LastName;
                                cardHolderInfo.CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : CNICNumber;
                                cardHolderInfo.Crew = crewInfo;
                                cardHolderInfo.Department = departmentInfo;
                                cardHolderInfo.Designation = designationInfo;
                                cardHolderInfo.EmergancyContactNumber = string.IsNullOrEmpty(emergancyContactNumber) ? null : emergancyContactNumber;
                                cardHolderInfo.Section = sectionInfo;
                                cardHolderInfo.WONumber = string.IsNullOrEmpty(wONumber) ? null : wONumber;
                                cardHolderInfo.IsTemp = false;

                                EFERTDbUtility.mEFERTDb.Entry(cardHolderInfo).State = System.Data.Entity.EntityState.Modified;
                            }
                            else
                            {

                                cardHolderInfo = new CardHolderInfo()
                                {
                                    FTItemId = cardHolderId,
                                    FirstName = cardHolder.FirstName,
                                    LastName = cardHolder.LastName,
                                    Company = companyInfo,
                                    Cadre = cadreInfo,
                                    CardNumber = cardHolder.LastName,
                                    CNICNumber = string.IsNullOrEmpty(CNICNumber) ? null : CNICNumber,
                                    Crew = crewInfo,
                                    Department = departmentInfo,
                                    Designation = designationInfo,
                                    EmergancyContactNumber = string.IsNullOrEmpty(emergancyContactNumber) ? null : emergancyContactNumber,
                                    Section = sectionInfo,
                                    WONumber = string.IsNullOrEmpty(wONumber) ? null : wONumber,
                                    IsTemp = false
                                };

                                EFERTDbUtility.mEFERTDb.CardHolders.Add(cardHolderInfo);
                            }

                            EFERTDbUtility.mEFERTDb.SaveChanges();


                            ContractorChForm contractorForm = new ContractorChForm(cardHolderInfo);
                            contractorForm.Show();
                        }
                    }
                }
                else if (!updatedCardExist)
                {
                    ContractorChForm contractorForm = new ContractorChForm(cardHolderInfo, true);
                    contractorForm.Show();
                }
                else if (visitor != null)
                {
                    VisitorForm vistorForm = new VisitorForm(visitor);

                    vistorForm.Show();
                }
                else if (dailyCardHolder != null)
                {
                    ContractorChForm contractorForm = new ContractorChForm(dailyCardHolder);

                    contractorForm.Show();
                }
            }
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form1.mMainForm.Close();
        }

        private void rbtCnicNumber_CheckedChanged(object sender, EventArgs e)
        {
            //this.rbtCardNumber.Checked = false;
            this.maskedTextBox1.Text = string.Empty;
            this.maskedTextBox1.Mask = "00000-0000000-0";

        }

        private void rbtCardNumber_CheckedChanged(object sender, EventArgs e)
        {
            //this.rbtCnicNumber.Checked = false;
            this.maskedTextBox1.Text = string.Empty;
            this.maskedTextBox1.Mask = "0000000000";
        }
    }
}
