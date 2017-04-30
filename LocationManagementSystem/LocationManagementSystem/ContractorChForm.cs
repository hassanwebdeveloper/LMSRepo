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
        public string mCNICNumber = string.Empty;
        public ContractorCardHolder mContractorCardHolder = null;
        public CardHolderInfo mCardHolderInfo = null;
        public DailyCardHolder mDailyCardHolder = null;
        public List<CheckInAndOutInfo> mCheckIns = new List<CheckInAndOutInfo>();
        public List<BlockedPersonInfo> mBlocks = new List<BlockedPersonInfo>();
        public bool mIsDailyCardHolder = false;
        private bool mClubStaff = false;

        public ContractorChForm(CardHolderInfo cardHolderInfo, bool isTempCard = false)
        {
            ContractorCardHolder contractor = null;

            if (cardHolderInfo != null)
            {
                this.mCardHolderInfo = cardHolderInfo;

                if (isTempCard)
                {
                    contractor = new ContractorCardHolder()
                    {
                        FirstName = cardHolderInfo.FirstName,
                        CNICNumber = string.IsNullOrEmpty(cardHolderInfo.CNICNumber) ? "CINC Not Found." : cardHolderInfo.CNICNumber,
                        GallagherCardHolder = false,
                        CheckInInfos = cardHolderInfo.CheckInInfos ?? new List<CheckInAndOutInfo>(),
                        BlockedPersonsInfos = cardHolderInfo.BlockingInfos ?? new List<BlockedPersonInfo>()
                    };
                }
                else
                {
                    string cadre = cardHolderInfo.Cadre == null ? "" : cardHolderInfo.Cadre.CadreName;
                    string companyName = cardHolderInfo.Company == null ? "" : cardHolderInfo.Company.CompanyName;
                    string CNICNumber = cardHolderInfo.CNICNumber;
                    string department = cardHolderInfo.Department == null ? "" : cardHolderInfo.Department.DepartmentName;
                    string designation = cardHolderInfo.Designation == null ? "" : cardHolderInfo.Designation.Designation;
                    string emergancyContactNumber = cardHolderInfo.EmergancyContactNumber;
                    string section = cardHolderInfo.Section == null ? "" : cardHolderInfo.Section.SectionName;
                    string wONumber = cardHolderInfo.WONumber;

                    contractor = new ContractorCardHolder()
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
                        WONumber = string.IsNullOrEmpty(wONumber) ? "WO No. Not Found." : wONumber,
                        CheckInInfos = cardHolderInfo.CheckInInfos ?? new List<CheckInAndOutInfo>(),
                        BlockedPersonsInfos = cardHolderInfo.BlockingInfos ?? new List<BlockedPersonInfo>()
                    };
                }
               

            }

            InitializeComponent();

            if (contractor != null)
            {
                this.mIsDailyCardHolder = false;
                this.mContractorCardHolder = contractor;
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
                this.mCheckIns = contractor.CheckInInfos;
                this.mBlocks = contractor.BlockedPersonsInfos;
                this.mCNICNumber = contractor.CNICNumber;

            }

            //this.UpdateLayout(this.mIsDailyCardHolder);
            this.UpdateStatus(this.mCNICNumber);
        }

        public ContractorChForm(DailyCardHolder dailyCardHolder)
        {
            InitializeComponent();

            if (dailyCardHolder != null)
            {
                this.mIsDailyCardHolder = true;
                this.mDailyCardHolder = dailyCardHolder;
                this.tbxFirstName.Text = dailyCardHolder.FirstName;
                this.tbxCompanyName.Text = dailyCardHolder.CompanyName;
                this.tbxCadre.Text = dailyCardHolder.Cadre;
                this.tbxDepartment.Text = dailyCardHolder.Department;
                this.tbxDesignation.Text = dailyCardHolder.Designation;
                this.tbxContactNumber.Text = dailyCardHolder.EmergancyContactNumber;
                this.tbxCNICNumber.Text = dailyCardHolder.CNICNumber;
                this.tbxWONumber.Text = dailyCardHolder.WONumber;
                this.tbxLastName.Text = dailyCardHolder.LastName;
                //this.tbxCardNumber.Text = permanentCh.CardNumber;
                this.mCheckIns = dailyCardHolder.CheckInInfos ?? new List<CheckInAndOutInfo>();
                this.mBlocks = dailyCardHolder.BlockingInfos ??  new List<BlockedPersonInfo>();
                this.mCNICNumber = dailyCardHolder.CNICNumber;

            }
            this.UpdateLayout(this.mIsDailyCardHolder);

            this.UpdateStatus(this.mCNICNumber);
        }

        public ContractorChForm(string cnicNumber, bool dailyCardHolder = false, string title = null, string gpTitle = null, bool clubStaff = false)
        {
            InitializeComponent();
            
            if (!string.IsNullOrEmpty(title))
            {
                this.Text = title;
            }

            if (!string.IsNullOrEmpty(gpTitle))
            {
                this.groupBox1.Text = gpTitle;
            }

            this.mClubStaff = clubStaff;

            if (clubStaff)
            {
                this.lblClubType.Visible = true;
                this.cbxClubType.Visible = true;
            }
            else
            {
                this.lblClubType.Visible = false;
                this.cbxClubType.Visible = false;
            }

            this.tbxFirstName.ReadOnly = false;
            this.tbxCompanyName.ReadOnly = false;
            this.tbxCadre.ReadOnly = false;
            this.tbxDepartment.ReadOnly = false;
            this.tbxDesignation.ReadOnly = false;
            this.tbxContactNumber.ReadOnly = false;
            this.tbxCNICNumber.Text = cnicNumber;
            this.tbxWONumber.ReadOnly = false;
            this.tbxLastName.ReadOnly = false;

            this.mIsDailyCardHolder = dailyCardHolder;
            this.mCNICNumber = cnicNumber;
            this.UpdateLayout(dailyCardHolder);

            this.UpdateStatus(cnicNumber);
        }

        private void UpdateLayout(bool dailyCardHolder)
        {
            if (dailyCardHolder)
            {
                if (this.mClubStaff)
                {
                    this.lblClubType.Location = this.label11.Location;
                    this.cbxClubType.Location = this.tbxWONumber.Location;
                }
                this.label11.Location = this.label2.Location;
                this.tbxWONumber.Location = this.tbxFirstName.Location;
                this.tbxWONumber.Size = this.tbxFirstName.Size;
                this.label2.Location = this.label1.Location;
                this.tbxFirstName.Location = this.tbxCardNumber.Location;
                this.label1.Visible = false;
                this.tbxCardNumber.Visible = false;
                this.groupBox1.Size = new Size(this.groupBox1.Size.Width, 250);
                this.groupBox2.Location = new Point(this.groupBox2.Location.X, 264);
                this.groupBox3.Location = new Point(this.groupBox3.Location.X, 264);
                this.Size = new Size(this.Size.Width, 540);
            }
            else
            {
                this.tbxCardNumber.Visible = false;
                this.tbxFirstName.ReadOnly = false;
                this.tbxFirstName.Location = new Point(this.tbxFirstName.Location.X, this.tbxCardNumber.Location.Y);
                this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                this.tbxFirstName.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                this.label2.Location = new Point(this.label2.Location.X, this.label1.Location.Y);
                this.tbxCompanyName.Visible = false;
                this.tbxCadre.Visible = false;
                this.tbxDepartment.Visible = false;
                this.tbxDesignation.Visible = false;
                this.tbxContactNumber.Visible = false;
                this.tbxCNICNumber.Text = this.mCNICNumber;
                this.label10.Location = new Point(this.label10.Location.X, this.label4.Location.Y);
                this.tbxCNICNumber.Location = new Point(this.tbxCNICNumber.Location.X, this.tbxCadre.Location.Y);
                this.label1.Visible = false;
                this.label4.Visible = false;
                this.tbxCadre.Visible = false;
                this.tbxCardNumber.Visible = false;
                this.tbxWONumber.Visible = false;
                this.tbxSection.Visible = false;
                this.tbxLastName.Visible = false;
                //this.tbxCardNumber.Text = permanentCh.CardNumber;

                this.groupBox1.Size = new Size(this.groupBox1.Size.Width, 60);
                this.groupBox2.Location = new Point(this.groupBox2.Location.X, 79);
                this.groupBox3.Location = new Point(this.groupBox3.Location.X, 79);
                this.Size = new Size(this.Size.Width, 350);
            }
        }

        private void UpdateStatus(string cnicNumber)
        {
            if (string.IsNullOrEmpty(cnicNumber))
            {
                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.tbxBlockedBy.ReadOnly = true;
                this.tbxBlockedReason.ReadOnly = true;
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
            }
            else
            {
                this.btnBlock.Visible = false;
                this.btnUnBlock.Visible = false;
                this.tbxBlockedBy.ReadOnly = true;
                this.tbxBlockedReason.ReadOnly = true;
            }

            


            bool blockedUser = false;

            if (this.mBlocks.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {
                blockedUser = true;
                BlockedPersonInfo blockedPerson = this.mBlocks.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);

                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.tbxBlockedBy.Text = blockedPerson.BlockedBy;
                this.tbxBlockedReason.Text = blockedPerson.Reason;
                this.tbxBlockedTime.Text = blockedPerson.BlockedTime.ToString();
                this.lblVisitorStatus.Text = "Blocked";
                this.lblVisitorStatus.BackColor = Color.Red;
                this.btnBlock.Enabled = false;
            }
            else
            {
                this.btnUnBlock.Enabled = false;
                this.tbxBlockedBy.Text = string.Empty;
                this.tbxBlockedReason.Text = string.Empty;
                this.lblVisitorStatus.Text = "Allowed";
                this.lblVisitorStatus.BackColor = Color.Green;
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
            }
            else
            {
                LimitStatus limitStatus = EFERTDbUtility.CheckIfUserCheckedInLimitReached(this.mCheckIns);

                if (limitStatus == LimitStatus.LimitReached)
                {
                    this.btnCheckIn.Enabled = false;
                    this.btnCheckOut.Enabled = false;
                    this.tbxBlockedBy.Text = "Admin";
                    this.tbxBlockedReason.Text = "You have reached maximum limit of temporary check in.";
                    this.lblVisitorStatus.Text = "Blocked";
                    this.lblVisitorStatus.BackColor = Color.Red;
                    this.btnBlock.Enabled = false;
                }
                else
                {

                    this.btnCheckIn.Enabled = true && !blockedUser;
                    this.btnCheckOut.Enabled = false;
                    this.tbxCheckInDateTimeIn.Text = DateTime.Now.ToString();
                }
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            CardHolderInfo cardHolderInfo = this.mCardHolderInfo;

            if (this.mContractorCardHolder == null && this.mDailyCardHolder == null)
            {
                bool validtated = EFERTDbUtility.ValidateInputs(new List<TextBox>() { this.tbxFirstName, this.tbxCNICNumber });
                if (!validtated)
                {
                    MessageBox.Show(this, "Please fill mandatory fields first.");
                    return;
                }
                if (this.mIsDailyCardHolder)
                {
                    DailyCardHolder dailyCardHolder = new DailyCardHolder()
                    {
                        FirstName = this.tbxFirstName.Text,
                        LastName = this.tbxLastName.Text,
                        Department = this.tbxDepartment.Text,
                        Cadre = this.tbxCadre.Text,
                        CNICNumber = this.tbxCNICNumber.Text,
                        CompanyName = this.tbxCompanyName.Text,
                        Designation = this.tbxDesignation.Text,
                        EmergancyContactNumber = this.tbxContactNumber.Text,
                        WONumber = this.tbxWONumber.Text
                    };

                    EFERTDbUtility.mEFERTDb.DailyCardHolders.Add(dailyCardHolder);
                    EFERTDbUtility.mEFERTDb.SaveChanges();

                    this.mDailyCardHolder = dailyCardHolder;

                }
                else
                {
                    cardHolderInfo = new CardHolderInfo()
                    {
                        FirstName = this.tbxFirstName.Text,
                        CNICNumber = string.IsNullOrEmpty(this.mCNICNumber) ? null : this.mCNICNumber,
                        IsTemp = true
                    };

                    EFERTDbUtility.mEFERTDb.CardHolders.Add(cardHolderInfo);
                    EFERTDbUtility.mEFERTDb.SaveChanges();

                    this.mCardHolderInfo = cardHolderInfo;

                }

            }

            BlockedPersonInfo blockedPerson = new BlockedPersonInfo()
            {
                Blocked = true,
                BlockedBy = this.tbxBlockedBy.Text,
                Reason = this.tbxBlockedReason.Text,
                CNICNumber = this.mCNICNumber,                
                BlockedTime = DateTime.Now,
                UnBlockTime = DateTime.MaxValue
            };

            if (SearchForm.mIsPlant)
            {
                blockedPerson.BlockedInPlant = true;
            }
            else
            {
                blockedPerson.BlockedInColony = true;
            }

            if (this.mIsDailyCardHolder)
            {
                blockedPerson.DailyCardHolders = this.mDailyCardHolder;
            }
            else
            {
                if (cardHolderInfo == null)
                {
                    MessageBox.Show(this, "Unable to Issue Card. Some error occured in getting cardholder information.");
                    return;
                }
                else
                {
                    blockedPerson.CardHolderInfos = cardHolderInfo;
                }
            }

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

            if (this.mIsDailyCardHolder)
            {
                this.mBlocks = this.mDailyCardHolder.BlockingInfos;
            }
            else
            {
                this.mBlocks = cardHolderInfo.BlockingInfos;
            }

            this.btnCheckIn.Enabled = false;
            this.btnCheckOut.Enabled = false;
            this.lblVisitorStatus.Text = "Blocked";
            this.lblVisitorStatus.BackColor = Color.Red;
            this.btnBlock.Enabled = false;
            this.btnUnBlock.Enabled = true;
        }

        private void btnUnBlock_Click(object sender, EventArgs e)
        {
            if (this.mBlocks.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {
                BlockedPersonInfo blockedPerson = this.mBlocks.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);
                blockedPerson.Blocked = false;
                blockedPerson.UnBlockTime = DateTime.Now;

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

                if (this.mIsDailyCardHolder)
                {
                    this.mBlocks = this.mDailyCardHolder.BlockingInfos;
                }
                else
                {
                    this.mBlocks = this.mCardHolderInfo.BlockingInfos;
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

                this.tbxBlockedBy.Text = string.Empty;
                this.tbxBlockedReason.Text = string.Empty;
                this.lblVisitorStatus.Text = "Allowed";
                this.lblVisitorStatus.BackColor = Color.Green;
                this.btnBlock.Enabled = true;
                this.btnUnBlock.Enabled = false;
            }
            else
            {
                MessageBox.Show(this, "This user is not blocked.");
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string cardNumber = this.tbxCheckInCardNumber.Text;

            bool validtated = EFERTDbUtility.ValidateInputs(new List<TextBox>() { this.tbxFirstName, this.tbxCNICNumber, this.tbxCheckInCardNumber });
            if (!validtated)
            {
                MessageBox.Show(this, "Please fill mandatory fields first.");
                return;
            }

            bool isCardNotReturned = this.mCheckIns.Any(checkInInfo => checkInInfo.CheckedIn && checkInInfo.CardNumber == cardNumber);
            CCFTCentralDb.CCFTCentral ccftCentralDb = new CCFTCentralDb.CCFTCentral();

            bool cardExist = ccftCentralDb.Cardholders.Any(card => card.LastName == cardNumber);

            CardHolderInfo cardHolderInfo = this.mCardHolderInfo;

            if (cardExist && !isCardNotReturned)
            {
                var cardAlreadyIssued = (from checkin in EFERTDbUtility.mEFERTDb.CheckedInInfos
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
                if (this.mContractorCardHolder == null && this.mDailyCardHolder == null)
                {
                    

                    if (this.mIsDailyCardHolder)
                    {
                        DailyCardHolder dailyCardHolder = new DailyCardHolder()
                        {
                            FirstName = this.tbxFirstName.Text,
                            LastName = this.tbxLastName.Text,
                            Department = this.tbxDepartment.Text,
                            Cadre = this.tbxCadre.Text,
                            CNICNumber = this.tbxCNICNumber.Text,
                            CompanyName = this.tbxCompanyName.Text,
                            Designation = this.tbxDesignation.Text,
                            EmergancyContactNumber = this.tbxContactNumber.Text,
                            WONumber = this.tbxWONumber.Text
                        };

                        EFERTDbUtility.mEFERTDb.DailyCardHolders.Add(dailyCardHolder);
                        //EFERTDbUtility.mEFERTDb.SaveChanges();

                        this.mDailyCardHolder = dailyCardHolder;

                    }
                    else
                    {
                        cardHolderInfo = new CardHolderInfo()
                        {
                            FirstName = this.tbxFirstName.Text,
                            CNICNumber = string.IsNullOrEmpty(this.mCNICNumber) ? null : this.mCNICNumber,
                            IsTemp = true                          
                        };

                        EFERTDbUtility.mEFERTDb.CardHolders.Add(cardHolderInfo);
                        //EFERTDbUtility.mEFERTDb.SaveChanges();

                        this.mCardHolderInfo = cardHolderInfo;
                        
                    }
                    
                }

                CheckInAndOutInfo checkedInInfo = new CheckInAndOutInfo();

                if (this.mIsDailyCardHolder)
                {
                    checkedInInfo.DailyCardHolders = this.mDailyCardHolder;
                }
                else
                {
                    if (cardHolderInfo == null)
                    {
                        MessageBox.Show(this, "Unable to Issue Card. Some error occured in getting cardholder information.");
                        return;
                    }
                    else
                    {
                        checkedInInfo.CardHolderInfos = cardHolderInfo;
                    }
                    


                }

                checkedInInfo.CNICNumber = this.mCNICNumber;
                checkedInInfo.CardNumber = this.tbxCheckInCardNumber.Text;
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
    }
}
