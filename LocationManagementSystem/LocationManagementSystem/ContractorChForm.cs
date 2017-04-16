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
        public DailyCardHolder mDailyCardHolder = null;
        public bool mIsDailyCardHolder = false;
        private bool mClubStaff = false;

        public ContractorChForm(ContractorCardHolder contractor)
        {
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
                this.label2.Location = this.label1.Location;
                this.tbxFirstName.Location = this.tbxCardNumber.Location;
                this.label1.Visible = false;
                this.tbxCardNumber.Visible = false;
                this.groupBox1.Size = new Size(708, 155);
                this.groupBox2.Location = new Point(12, 173);
                this.groupBox3.Location = new Point(406, 173);
                this.Size = new Size(754, 370);
            }
            else
            {
                this.tbxCardNumber.Visible = false;
                this.tbxFirstName.ReadOnly = false;
                this.tbxFirstName.Location = new Point(86, 25);
                this.label2.Location = this.label1.Location;
                this.tbxCompanyName.Visible = false;
                this.tbxCadre.Visible = false;
                this.tbxDepartment.Visible = false;
                this.tbxDesignation.Visible = false;
                this.tbxContactNumber.Visible = false;
                this.tbxCNICNumber.Text = this.mCNICNumber;
                this.label10.Location = this.label4.Location;
                this.tbxCNICNumber.Location = this.tbxCadre.Location;
                this.label1.Visible = false;
                this.label4.Visible = false;
                this.tbxCadre.Visible = false;
                this.tbxCardNumber.Visible = false;
                this.tbxWONumber.Visible = false;
                this.tbxSection.Visible = false;
                this.tbxLastName.Visible = false;
                //this.tbxCardNumber.Text = permanentCh.CardNumber;

                this.groupBox1.Size = new Size(708, 51);
                this.groupBox2.Location = new Point(12, 70);
                this.groupBox3.Location = new Point(406, 70);
                this.Size = new Size(754, 270);
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

            if (SearchForm.mBlockedList.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {
                blockedUser = true;
                BlockedPersonInfo blockedPerson = SearchForm.mBlockedList.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);

                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = false;
                this.tbxBlockedBy.Text = blockedPerson.BlockedBy;
                this.tbxBlockedReason.Text = blockedPerson.Reason;
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

            if (SearchForm.mCheckedInList.Exists(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber))
            {
                CheckInAndOutInfo checkedInInfo = SearchForm.mCheckedInList.Find(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber);
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
                int checkedInCount = SearchForm.mCheckedInList.Count(checkedInInfo => !checkedInInfo.CheckedIn && checkedInInfo.CNICNumber == this.mCNICNumber);

                if (checkedInCount == 3)
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
            BlockedPersonInfo blockedPerson = new BlockedPersonInfo()
            {
                Blocked = true,
                BlockedBy = this.tbxBlockedBy.Text,
                Reason = this.tbxBlockedReason.Text,
                CNICNumber = this.mCNICNumber
            };
            SearchForm.mBlockedList.Add(blockedPerson);
            this.btnCheckIn.Enabled = false;
            this.btnCheckOut.Enabled = false;
            this.lblVisitorStatus.Text = "Blocked";
            this.lblVisitorStatus.BackColor = Color.Red;
            this.btnBlock.Enabled = false;
            this.btnUnBlock.Enabled = true;
        }

        private void btnUnBlock_Click(object sender, EventArgs e)
        {
            if (SearchForm.mBlockedList.Exists(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber))
            {
                if (SearchForm.mCheckedInList.Exists(checkedIn => checkedIn.CheckedIn && checkedIn.CNICNumber == this.mCNICNumber))
                {
                    this.btnCheckIn.Enabled = false;
                    this.btnCheckOut.Enabled = true;
                }
                else
                {
                    this.btnCheckIn.Enabled = true;
                    this.btnCheckOut.Enabled = false;
                }
                BlockedPersonInfo blockedPerson = SearchForm.mBlockedList.Find(blocked => blocked.Blocked && blocked.CNICNumber == this.mCNICNumber);
                blockedPerson.Blocked = false;

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
            bool isCardNotReturned = SearchForm.mCheckedInList.Any(checkInInfo => checkInInfo.CheckedIn && checkInInfo.CardNumber == cardNumber);
            CCFTCentralDb.CCFTCentral ccftCentralDb = new CCFTCentralDb.CCFTCentral();
            bool cardExist = ccftCentralDb.Cardholders.Any(card => card.LastName == cardNumber);

            if (cardExist && !isCardNotReturned)
            {
                if (this.mContractorCardHolder == null && this.mDailyCardHolder == null)
                {
                    EFERTDbContext efertDb = new EFERTDbContext();

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

                        SearchForm.mDailyCardHolders.Add(dailyCardHolder);
                    }
                    else
                    {
                        CardHolderInfo cardHolderInfo = new CardHolderInfo()
                        {
                            FirstName = this.tbxFirstName.Text,
                            CNICNumber = string.IsNullOrEmpty(this.mCNICNumber) ? null : this.mCNICNumber,
                            IsTemp = true
                        };

                        efertDb.CardHolders.Add(cardHolderInfo);
                        efertDb.SaveChanges();
                    }

                    

                    //VisitorCardHolder visitor = new VisitorCardHolder();

                    //visitor.CNICNumber = this.tbxCnicNumber.Text;
                    //visitor.Gender = this.cbxGender.SelectedItem == null ? string.Empty : this.cbxGender.SelectedItem as String;
                    //visitor.FirstName = this.tbxFirstName.Text;
                    //visitor.LastName = this.tbxLastName.Text;
                    //visitor.PostCode = this.tbxAddress.Text;
                    //visitor.City = this.tbxCity.Text;
                    //visitor.State = this.tbxState.Text;
                    //visitor.CompanyName = this.tbxCompanyName.Text;
                    //visitor.ContactNo = this.tbxPhoneNumber.Text;
                    //visitor.EmergencyContantPerson = this.tbxEmergencyContact.Text;
                    //visitor.EmergencyContantPersonNumber = this.tbxEmergencyContactNumber.Text;

                    //SearchForm.mVisitorsList.Add(visitor);
                }

                CheckInAndOutInfo checkedInInfo = new CheckInAndOutInfo();

                checkedInInfo.CNICNumber = this.mCNICNumber;
                checkedInInfo.CardNumber = this.tbxCheckInCardNumber.Text;
                checkedInInfo.VehicleNmuber = this.tbxCheckInVehicleNumber.Text;
                checkedInInfo.DateTimeIn = Convert.ToDateTime(this.tbxCheckInDateTimeIn.Text);
                checkedInInfo.CheckedIn = true;

                this.btnCheckIn.Enabled = false;
                this.btnCheckOut.Enabled = true;

                SearchForm.mCheckedInList.Add(checkedInInfo);

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
            if (SearchForm.mCheckedInList.Exists(checkedOut => checkedOut.CheckedIn && checkedOut.CNICNumber == this.mCNICNumber))
            {
                CheckInAndOutInfo checkedOutInfo = SearchForm.mCheckedInList.Find(checkedOut => checkedOut.CheckedIn && checkedOut.CNICNumber == this.mCNICNumber);
                checkedOutInfo.CheckedIn = false;
                checkedOutInfo.DateTimeOut = Convert.ToDateTime(this.tbxCheckInDateTimeOut.Text);

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
