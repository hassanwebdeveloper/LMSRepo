using LocationManagementSystem.CCFTCentralDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationManagementSystem
{
    public partial class PermanentChForm : Form
    {
        public string mCNICNumber = string.Empty;
        public List<CheckInAndOutInfo> mCheckIns = new List<CheckInAndOutInfo>();
        public List<BlockedPersonInfo> mBlocks = new List<BlockedPersonInfo>();
        public CardHolderInfo mCardHolderInfo = null;

        public PermanentChForm(CardHolderInfo cardHolderInfo)
        {
            PermamentCardHolder permanentCh = null;

            if (cardHolderInfo != null)
            {
                this.mCardHolderInfo = cardHolderInfo;
                string strPNumber = string.IsNullOrEmpty(cardHolderInfo.PNumber) ? "P-Number not found." : cardHolderInfo.PNumber;

                string strDOB = string.IsNullOrEmpty(cardHolderInfo.DateOfBirth) ? "Date of birth not found." : cardHolderInfo.DateOfBirth;
                string bloodGroup = cardHolderInfo.BloodGroup;
                string CNICNumber = cardHolderInfo.CNICNumber;
                string crew = cardHolderInfo.Crew == null ? "" : cardHolderInfo.Crew.CrewName;
                string cadre = cardHolderInfo.Cadre == null ? "" : cardHolderInfo.Cadre.CadreName;
                string department = cardHolderInfo.Department == null ? "" : cardHolderInfo.Department.DepartmentName;
                string designation = cardHolderInfo.Designation == null ? "" : cardHolderInfo.Designation.Designation;
                string contactNumber = cardHolderInfo.EmergancyContactNumber;
                string section = cardHolderInfo.Section == null ? "" : cardHolderInfo.Section.SectionName;

                permanentCh = new PermamentCardHolder()
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
                    DateOfBirth = strDOB,
                    CheckInInfos = cardHolderInfo.CheckInInfos ?? new List<CheckInAndOutInfo>(),
                    BlockedInfos = cardHolderInfo.BlockingInfos ?? new List<BlockedPersonInfo>()
                };
            }

            InitializeComponent();

            if (permanentCh != null)
            {
                PersonalDataImageID pdii = (from pdi in EFERTDbUtility.mCCFTCentral.PersonalDataImageIDs
                                             where pdi != null && pdi.CardholderID == cardHolderInfo.FTItemId
                                             select pdi).FirstOrDefault();

                

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
                this.mCheckIns = permanentCh.CheckInInfos;
                this.mBlocks = permanentCh.BlockedInfos;
                this.mCNICNumber = permanentCh.CNICNumber;
            }

            this.UpdateStatus(this.mCNICNumber);
        }

        //public PermanentChForm(string cnicNumber)
        //{
        //    InitializeComponent();

        //    this.tbxCardNumber.ReadOnly = false;
        //    this.tbxFirstName.ReadOnly = false;
        //    this.tbxBloodGroup.ReadOnly = false;
        //    this.tbxCadre.ReadOnly = false;
        //    this.tbxCrew.ReadOnly = false;
        //    this.tbxDob.ReadOnly = false;
        //    this.tbxDepartment.ReadOnly = false;
        //    this.tbxDesignation.ReadOnly = false;
        //    this.tbxContactNumber.ReadOnly = false;
        //    this.tbxCNICNumber.Text = cnicNumber;
        //    this.tbxPNumber.ReadOnly = false;
        //    this.tbxSection.ReadOnly = false;
        //    this.tbxLastName.ReadOnly = false;
        //    //this.tbxCardNumber.Text = permanentCh.CardNumber;


        //    this.UpdateStatus(cnicNumber);
        //}

        private void UpdateStatus(string cnicNumber)
        {
            if (string.IsNullOrEmpty(cnicNumber))
            {
                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.tbxBlockedBy.ReadOnly = true;
                this.tbxBlockedReason.ReadOnly = true;
                this.tbxBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxBlockedReason.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.lblVisitorStatus.Text = "Invalid User";
                this.lblVisitorStatus.BackColor = Color.Red;
                this.btnBlock.Enabled = false;
                this.btnUnBlock.Enabled = false;
                return;
            }

            if (Form1.mLoggedInUser.IsAdmin)
            {
                this.btnBlock.Visible = true;
                this.btnUnBlock.Visible = true;
                this.tbxBlockedBy.ReadOnly = false;
                this.tbxBlockedReason.ReadOnly = false;
                this.tbxBlockedBy.BackColor = System.Drawing.Color.White;
                this.tbxBlockedReason.BackColor = System.Drawing.Color.White;
            }
            else
            {
                this.btnBlock.Visible = false;
                this.btnUnBlock.Visible = false;
                this.tbxBlockedBy.ReadOnly = true;
                this.tbxBlockedReason.ReadOnly = true;
                this.tbxBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxBlockedReason.BackColor = System.Drawing.SystemColors.ButtonFace;
            }

            this.mCNICNumber = cnicNumber;

            BlockedPersonInfo blockedPerson = null;
            bool blockedUser = false;

            if (this.mBlocks.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {
                blockedUser = true;
                blockedPerson = this.mBlocks.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);

                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.tbxBlockedBy.Text = blockedPerson.BlockedBy;
                this.tbxBlockedReason.Text = blockedPerson.BlockedReason;
                this.tbxBlockedTime.Text = blockedPerson.BlockedTime.ToString();
                this.lblVisitorStatus.Text = "Blocked";
                this.lblVisitorStatus.BackColor = Color.Red;
                this.btnBlock.Enabled = false;

                this.tbxBlockedBy.ReadOnly = true;
                this.tbxBlockedReason.ReadOnly = true;

                this.tbxBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxBlockedReason.BackColor = System.Drawing.SystemColors.ButtonFace;

                this.tbxUnBlockedBy.ReadOnly = false;
                this.tbxUnblockReason.ReadOnly = false;

                this.tbxUnBlockedBy.BackColor = System.Drawing.Color.White;
                this.tbxUnblockReason.BackColor = System.Drawing.Color.White;
            }
            else
            {
                this.btnUnBlock.Enabled = false;
                this.tbxBlockedBy.Text = string.Empty;
                this.tbxBlockedReason.Text = string.Empty;
                this.lblVisitorStatus.Text = "Allowed";
                this.lblVisitorStatus.BackColor = Color.Green;

                this.tbxBlockedBy.ReadOnly = false;
                this.tbxBlockedReason.ReadOnly = false;

                this.tbxBlockedBy.BackColor = System.Drawing.Color.White;
                this.tbxBlockedReason.BackColor = System.Drawing.Color.White;

                this.tbxUnBlockedBy.ReadOnly = true;
                this.tbxUnblockReason.ReadOnly = true;

                this.tbxUnBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxUnblockReason.BackColor = System.Drawing.SystemColors.ButtonFace;


                if (this.mBlocks.Count > 0)
                {
                    BlockedPersonInfo lastBlockedInfo = this.mBlocks.Last();

                    this.tbxUnBlockedBy.Text = lastBlockedInfo.UnBlockedBy;
                    this.tbxUnBlockTime.Text = lastBlockedInfo.UnBlockTime.ToString();
                    this.tbxUnblockReason.Text = lastBlockedInfo.UnBlockedReason;
                }
            }

            if (this.mCheckIns.Exists(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber))
            {
                CheckInAndOutInfo checkedInInfo = this.mCheckIns.Find(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber);
                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = true && !blockedUser;

                this.tbxCheckInCardNumber.Text = checkedInInfo.CardNumber;
                this.tbxCheckInVehicleNumber.Text = checkedInInfo.VehicleNmuber;
                this.tbxCheckInDateTimeIn.Text = checkedInInfo.DateTimeIn.ToString();
                this.tbxCheckInDateTimeOut.Text = DateTime.Now.ToString();

                this.tbxCheckInCardNumber.ReadOnly = true;
                this.tbxCheckInVehicleNumber.ReadOnly = true;
                this.tbxCheckInCardNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxCheckInVehicleNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            }
            else
            {
                LimitStatus limitStatus = EFERTDbUtility.CheckIfUserCheckedInLimitReached(this.mCheckIns);

                if (limitStatus == LimitStatus.LimitReached)
                {
                    blockedUser = true;
                    this.btnCheckIn.Enabled = false;
                    this.btnCheckOut.Enabled = false;
                    this.tbxBlockedBy.Text = "Admin";
                    this.tbxBlockedReason.Text = "You have reached maximum limit of temporary check in.";
                    this.lblVisitorStatus.Text = "Blocked";
                    this.lblVisitorStatus.BackColor = Color.Red;
                    this.btnBlock.Enabled = false;

                    this.tbxBlockedBy.ReadOnly = true;
                    this.tbxBlockedReason.ReadOnly = true;

                    this.tbxBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                    this.tbxBlockedReason.BackColor = System.Drawing.SystemColors.ButtonFace;
                }
                else
                {

                    this.btnCheckIn.Enabled = true && !blockedUser;
                    this.btnCheckOut.Enabled = false;
                    this.tbxCheckInDateTimeIn.Text = DateTime.Now.ToString();
                }
            }

            if (blockedUser)
            {
                this.tbxCheckInCardNumber.ReadOnly = true;
                this.tbxCheckInVehicleNumber.ReadOnly = true;
                this.tbxCheckInCardNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxCheckInVehicleNumber.BackColor = System.Drawing.SystemColors.ButtonFace;

                BlockedPersonNotificationForm blockedForm = null;
                if (blockedPerson == null)
                {
                    blockedForm = new BlockedPersonNotificationForm(this.mCardHolderInfo.FirstName, this.mCNICNumber);
                }
                else
                {
                    blockedForm = new BlockedPersonNotificationForm(blockedPerson);
                }


                blockedForm.ShowDialog(this);
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            CardHolderInfo cardHolderInfo = this.mCardHolderInfo;

            bool validtated = EFERTDbUtility.ValidateInputs(new List<TextBox>() { this.tbxFirstName, this.tbxCNICNumber, this.tbxBlockedBy, this.tbxBlockedReason });
            if (!validtated)
            {
                MessageBox.Show(this, "Please fill mandatory fields first.");
                return;
            }

            DialogResult result = MessageBox.Show(this, "Are you sure you want to block this person?", "Confirmation Dialog", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            if (cardHolderInfo == null)
            {
                MessageBox.Show(this, "Unable to Block user. Some error occured in getting cardholder information.");
                return;
            }

            BlockedPersonInfo blockedPerson = new BlockedPersonInfo()
            {
                Blocked = true,
                BlockedBy = this.tbxBlockedBy.Text,
                BlockedReason = this.tbxBlockedReason.Text,
                CNICNumber = this.mCNICNumber,
                BlockedTime = DateTime.Now,
                UnBlockTime = DateTime.MaxValue,
                CardHolderInfos = cardHolderInfo
            };

            blockedPerson.BlockedInPlant = SearchForm.mIsPlant;
            blockedPerson.BlockedInColony = !SearchForm.mIsPlant;

            try
            {
                EFERTDbUtility.mEFERTDb.BlockedPersons.Add(blockedPerson);
                EFERTDbUtility.mEFERTDb.SaveChanges();
            }
            catch (Exception ex)
            {
                EFERTDbUtility.RollBack();

                MessageBox.Show(this, "Some error occurred in blocking cardholder.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
                return;
            }

            this.mBlocks = cardHolderInfo.BlockingInfos;

            this.btnCheckIn.Enabled = false;
            this.btnCheckOut.Enabled = false;
            this.lblVisitorStatus.Text = "Blocked";
            this.lblVisitorStatus.BackColor = Color.Red;
            this.tbxBlockedTime.Text = blockedPerson.BlockedTime.ToString();
            this.btnBlock.Enabled = false;
            this.btnUnBlock.Enabled = true;

            this.tbxBlockedBy.ReadOnly = true;
            this.tbxBlockedReason.ReadOnly = true;

            this.tbxBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbxBlockedReason.BackColor = System.Drawing.SystemColors.ButtonFace;

            this.tbxUnBlockedBy.ReadOnly = false;
            this.tbxUnblockReason.ReadOnly = false;

            this.tbxUnBlockedBy.BackColor = System.Drawing.Color.White;
            this.tbxUnblockReason.BackColor = System.Drawing.Color.White;
        }

        private void btnUnBlock_Click(object sender, EventArgs e)
        {
            bool validtated = EFERTDbUtility.ValidateInputs(new List<TextBox>() { this.tbxFirstName, this.tbxCNICNumber, this.tbxUnBlockedBy, this.tbxUnblockReason });
            if (!validtated)
            {
                MessageBox.Show(this, "Please fill mandatory fields first.");
                return;
            }


            DialogResult result = MessageBox.Show(this, "Are you sure you want to unblock this person?", "Confirmation Dialog", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            if (this.mBlocks.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {                
                BlockedPersonInfo blockedPerson = this.mBlocks.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);
                blockedPerson.Blocked = false;
                blockedPerson.UnBlockTime = DateTime.Now;
                blockedPerson.UnBlockedBy = this.tbxUnBlockedBy.Text;
                blockedPerson.UnBlockedReason = this.tbxUnblockReason.Text;

                try
                {
                    EFERTDbUtility.mEFERTDb.Entry(blockedPerson).State = System.Data.Entity.EntityState.Modified;
                    EFERTDbUtility.mEFERTDb.SaveChanges();
                }
                catch (Exception ex)
                {
                    EFERTDbUtility.RollBack();

                    MessageBox.Show(this, "Some error occurred in unblocking cardholder.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
                    return;
                }

                if (this.mCheckIns.Exists(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber))
                {
                    this.btnCheckIn.Enabled = false;
                    this.btnCheckOut.Enabled = true;
                }
                else
                {
                    this.btnCheckIn.Enabled = true;
                    this.btnCheckOut.Enabled = false;
                }

                this.mBlocks = this.mCardHolderInfo.BlockingInfos;

                this.tbxBlockedBy.Text = string.Empty;
                this.tbxBlockedReason.Text = string.Empty;
                this.lblVisitorStatus.Text = "Allowed";
                this.lblVisitorStatus.BackColor = Color.Green;
                this.tbxUnBlockTime.Text = blockedPerson.UnBlockTime.ToString();
                this.btnBlock.Enabled = true;
                this.btnUnBlock.Enabled = false;

                this.tbxBlockedBy.ReadOnly = false;
                this.tbxBlockedReason.ReadOnly = false;

                this.tbxBlockedBy.BackColor = System.Drawing.Color.White;
                this.tbxBlockedReason.BackColor = System.Drawing.Color.White; 

                this.tbxUnBlockedBy.ReadOnly = true;
                this.tbxUnblockReason.ReadOnly = true;

                this.tbxUnBlockedBy.BackColor = System.Drawing.SystemColors.ButtonFace;
                this.tbxUnblockReason.BackColor = System.Drawing.SystemColors.ButtonFace;
            }
            else
            {
                MessageBox.Show(this, "This user is not blocked.");
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string cardNumber = this.tbxCheckInCardNumber.Text;

            if (string.IsNullOrEmpty(cardNumber))
            {
                MessageBox.Show(this, "Card number can not be empty.");
                return;
            }

            bool isCardNotReturned = this.mCheckIns.Any(checkInInfo => checkInInfo.CheckedIn && checkInInfo.CardNumber == cardNumber);

            bool cardExist = false;
            CCFTCentralDb.CCFTCentral ccftCentralDb = new CCFTCentralDb.CCFTCentral();
            cardExist = ccftCentralDb.Cardholders.Any(card => card.LastName == cardNumber);
            CardHolderInfo cardHolderInfo = this.mCardHolderInfo;

            if (cardExist && !isCardNotReturned)
            {
                var cardAlreadyIssued =(from checkin in EFERTDbUtility.mEFERTDb.CheckedInInfos
                 where checkin != null && checkin.CheckedIn && checkin.CardNumber == cardNumber
                 select new
                 {
                     checkin.CheckedIn,
                     checkin.CNICNumber
                 }).FirstOrDefault();

                if (cardAlreadyIssued != null && cardAlreadyIssued.CheckedIn)
                {
                    MessageBox.Show(this, "This card is already issue to the person with CNIC number: " + cardAlreadyIssued.CNICNumber);
                    return;
                }
                

                if (cardHolderInfo == null)
                {
                    MessageBox.Show(this, "Unable to Issue Card. Some error occured in getting cardholder information.");
                    return;
                }

                CheckInAndOutInfo checkedInInfo = new CheckInAndOutInfo();

                checkedInInfo.CheckInToPlant = SearchForm.mIsPlant;
                checkedInInfo.CheckInToPlant = !SearchForm.mIsPlant;
                checkedInInfo.FirstName = cardHolderInfo.FirstName;
                checkedInInfo.CardHolderInfos = cardHolderInfo;
                checkedInInfo.CNICNumber = this.mCNICNumber;
                checkedInInfo.CardNumber = cardNumber;
                checkedInInfo.VehicleNmuber = this.tbxCheckInVehicleNumber.Text;
                checkedInInfo.DateTimeIn = Convert.ToDateTime(this.tbxCheckInDateTimeIn.Text);
                checkedInInfo.DateTimeOut = DateTime.MaxValue;
                checkedInInfo.CheckedIn = true;                               

                try
                {
                    EFERTDbUtility.mEFERTDb.CheckedInInfos.Add(checkedInInfo);
                    EFERTDbUtility.mEFERTDb.SaveChanges();
                }
                catch (Exception ex)
                {
                    EFERTDbUtility.RollBack();

                    MessageBox.Show(this, "Some error occurred in issuing card.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
                    return;
                }

                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = true;

                if (NewColonyChForm.mNewColonyChForm != null)
                {
                    NewColonyChForm.mNewColonyChForm.Close();
                }

                if (NewPlantChForm.mNewPlantChForm != null)
                {
                    NewPlantChForm.mNewPlantChForm.Close();
                }

                this.Close();
            }
            else
            {
                if (!cardExist)
                {
                    MessageBox.Show(this, "Please enter valid card number.");
                }
                else if (isCardNotReturned)
                {
                    MessageBox.Show(this, "Card is already issued to some one else.");
                }

            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (this.mCheckIns.Exists(checkedOut => checkedOut.CheckedIn && checkedOut.CNICNumber == this.mCNICNumber))
            {
                CheckInAndOutInfo checkedOutInfo = this.mCheckIns.Find(checkedOut => checkedOut.CheckedIn && checkedOut.CNICNumber == this.mCNICNumber);
                checkedOutInfo.CheckedIn = false;
                checkedOutInfo.DateTimeOut = Convert.ToDateTime(this.tbxCheckInDateTimeOut.Text);

                try
                {
                    EFERTDbUtility.mEFERTDb.Entry(checkedOutInfo).State = System.Data.Entity.EntityState.Modified;
                    EFERTDbUtility.mEFERTDb.SaveChanges();
                }
                catch (Exception ex)
                {
                    EFERTDbUtility.RollBack();

                    MessageBox.Show(this, "Some error occurred in returning card.\n\n" + EFERTDbUtility.GetInnerExceptionMessage(ex));
                    return;
                }

                this.btnCheckIn.Enabled = true;
                this.btnCheckOut.Enabled = false;

                if (NewColonyChForm.mNewColonyChForm != null)
                {
                    NewColonyChForm.mNewColonyChForm.Close();
                }

                if (NewPlantChForm.mNewPlantChForm != null)
                {
                    NewPlantChForm.mNewPlantChForm.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "This user is not checked in.");
            }
        }

        private void tbxCheckInCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            EFERTDbUtility.AllowNumericOnly(e);
        }

        private void tbxCheckInCardNumber_TextChanged(object sender, EventArgs e)
        {
            EFERTDbUtility.ValidateInputs(new List<TextBox>() { this.tbxCheckInCardNumber });
        }
    }
}
