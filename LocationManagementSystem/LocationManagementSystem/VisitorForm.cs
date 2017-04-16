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
    public partial class VisitorForm : Form
    {
        public string mCNICNumber = string.Empty;
        public VisitorCardHolder mVisitor = null;

        public VisitorForm(VisitorCardHolder visitorCardHolder)
        {
            InitializeComponent();
            if (SearchForm.mIsPlant)
            {
                this.cbxVisitorType.Items.AddRange(new object[] {
                                                    "Official",
                                                    "Private"
                                                                        });
            }
            else
            {
                this.cbxVisitorType.Items.AddRange(new object[] {
                                                    "Clinic / Snake bite",
                                                    "Contractor / Supervisor",
                                                    "Work Order Staff",
                                                    "Market",
                                                    "School",
                                                    "Masjid",
                                                    "Club"});
            }

            if (visitorCardHolder != null)
            {
                this.mVisitor = visitorCardHolder;

                this.mCNICNumber = this.mVisitor.CNICNumber;

                this.tbxCnicNumber.Text = this.mVisitor.CNICNumber;
                this.cbxGender.SelectedItem = this.mVisitor.Gender;
                this.tbxFirstName.Text = this.mVisitor.FirstName;
                this.tbxLastName.Text = this.mVisitor.LastName;
                this.tbxAddress.Text = this.mVisitor.PostCode;
                this.tbxCity.Text = this.mVisitor.City;
                this.tbxState.Text = this.mVisitor.State;
                this.tbxCompanyName.Text = this.mVisitor.CompanyName;
                this.tbxPhoneNumber.Text = this.mVisitor.ContactNo;
                this.tbxEmergencyContact.Text = this.mVisitor.EmergencyContantPerson;
                this.tbxEmergencyContactNumber.Text = this.mVisitor.EmergencyContantPersonNumber;
                this.cbxVisitorType.SelectedItem = this.mVisitor.VisitorType;

                this.tbxCnicNumber.ReadOnly = true;
                this.cbxGender.Enabled = false;
                this.tbxFirstName.ReadOnly = true;
                this.tbxLastName.ReadOnly = true;
                this.tbxAddress.ReadOnly = true;
                this.tbxCity.ReadOnly = true;
                this.tbxState.ReadOnly = true;
                this.tbxCompanyName.ReadOnly = true;
                this.tbxPhoneNumber.ReadOnly = true;
                this.tbxEmergencyContact.ReadOnly = true;
                this.tbxEmergencyContactNumber.ReadOnly = true;
            }
            
            this.UpdateStatus(this.mCNICNumber);

        }


        public VisitorForm(string cnicNumber, string title = null, string gpTitle = null, bool schoolStaff = false)
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

            if (schoolStaff)
            {
                this.lblSchoolCollege.Visible = true;
                this.cbxSchoolCollege.Visible = true;
            }
            else
            {
                this.lblSchoolCollege.Visible = false;
                this.cbxSchoolCollege.Visible = false;
            }

            if (SearchForm.mIsPlant)
            {
                this.cbxVisitorType.Items.AddRange(new object[] {
                                                    "Official",
                                                    "Private"
                                                                        });
            }
            else
            {
                this.cbxVisitorType.Items.AddRange(new object[] {
                                                    "Clinic / Snake bite",
                                                    "Contractor / Supervisor",
                                                    "Work Order Staff",
                                                    "Market",
                                                    "School",
                                                    "Masjid",
                                                    "Club"});
            }

            this.tbxCnicNumber.Text = cnicNumber;
            this.UpdateStatus(cnicNumber);
            
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

            this.mCNICNumber = cnicNumber;

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
                this.nuNoOfMaleGuest.Value = checkedInInfo.NoOfMaleGuest;
                this.nuNoOfFemaleGuest.Value = checkedInInfo.NoOfFemaleGuest;
                this.numCheckInDurationOfStay.Value = checkedInInfo.DurationOfStay;
                this.nuCheckInNoOfChildren.Value = checkedInInfo.NoOfChildren;
                this.tbxCheckInAreaOfVisit.Text = checkedInInfo.AreaOfVisit;
                this.tbxCheckInHostName.Text = checkedInInfo.HostName;
                this.tbxCheckInDateTimeIn.Text = checkedInInfo.DateTimeIn.ToString();
                this.tbxCheckInDateTimeOut.Text = DateTime.Now.ToString();

                this.tbxCheckInCardNumber.ReadOnly = true;
                this.tbxCheckInVehicleNumber.ReadOnly = true;
                this.nuNoOfMaleGuest.ReadOnly = true;
                this.nuNoOfFemaleGuest.ReadOnly = true;
                this.numCheckInDurationOfStay.ReadOnly = true;
                this.nuCheckInNoOfChildren.ReadOnly = true;
                this.tbxCheckInAreaOfVisit.ReadOnly = true;
                this.tbxCheckInHostName.ReadOnly = true;
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

            bool isCardReturned = !SearchForm.mCheckedInList.Any(checkInInfo => checkInInfo.CardNumber == cardNumber);

            CCFTCentralDb.CCFTCentral ccftCentralDb = new CCFTCentralDb.CCFTCentral();
            bool cardExist = ccftCentralDb.Cardholders.Any(card => card.LastName == cardNumber);

            if (cardExist && isCardReturned)
            {

                if (!SearchForm.mVisitorsList.Exists(vistor => vistor.CNICNumber == this.mCNICNumber))
                {
                    VisitorCardHolder visitor = new VisitorCardHolder();

                    visitor.CNICNumber = this.tbxCnicNumber.Text;
                    visitor.Gender = this.cbxGender.SelectedItem == null ? string.Empty : this.cbxGender.SelectedItem as String;
                    visitor.FirstName = this.tbxFirstName.Text;
                    visitor.LastName = this.tbxLastName.Text;
                    visitor.PostCode = this.tbxAddress.Text;
                    visitor.City = this.tbxCity.Text;
                    visitor.State = this.tbxState.Text;
                    visitor.CompanyName = this.tbxCompanyName.Text;
                    visitor.ContactNo = this.tbxPhoneNumber.Text;
                    visitor.EmergencyContantPerson = this.tbxEmergencyContact.Text;
                    visitor.EmergencyContantPersonNumber = this.tbxEmergencyContactNumber.Text;
                    visitor.VisitorType = this.cbxVisitorType.SelectedItem == null ? string.Empty : this.cbxVisitorType.SelectedItem as String;

                    SearchForm.mVisitorsList.Add(visitor);
                }

                CheckInAndOutInfo checkedInInfo = new CheckInAndOutInfo();

                checkedInInfo.CNICNumber = this.mCNICNumber;
                checkedInInfo.CardNumber = this.tbxCheckInCardNumber.Text;
                checkedInInfo.VehicleNmuber = this.tbxCheckInVehicleNumber.Text;
                checkedInInfo.NoOfMaleGuest = this.nuNoOfMaleGuest.Value;
                checkedInInfo.NoOfFemaleGuest = this.nuNoOfFemaleGuest.Value;
                checkedInInfo.DurationOfStay = this.numCheckInDurationOfStay.Value;
                checkedInInfo.NoOfChildren = this.nuCheckInNoOfChildren.Value;
                checkedInInfo.AreaOfVisit = this.tbxCheckInAreaOfVisit.Text;
                checkedInInfo.HostName = this.tbxCheckInHostName.Text;
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
                if (cardExist)
                {
                    MessageBox.Show(this, "Please enter valid card number.");
                }
                else if (isCardReturned)
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
