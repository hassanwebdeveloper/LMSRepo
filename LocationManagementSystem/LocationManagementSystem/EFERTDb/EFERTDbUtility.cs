using LocationManagementSystem.CCFTCentralDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationManagementSystem
{
    public static class EFERTDbUtility
    {
        public static CCFTCentral mCCFTCentral = null;
        public static EFERTDbContext mEFERTDb = null;
        public static List<VisitingLocations> mVisitingLocations = null;

        public static void InitializeDatabases()
        {
            mEFERTDb = new EFERTDbContext();
            mCCFTCentral = new CCFTCentral();
            try
            {
                Cardholder cardHolderByNic = (from pds in mCCFTCentral.PersonalDataStrings
                                              where pds != null && pds.PersonalDataFieldID == 5051 && pds.Value != null && pds.Value == "12345-1234567-1"
                                              select pds.Cardholder).FirstOrDefault();
            }
            catch (Exception e)
            {
                
            }

            try
            {
                mVisitingLocations = (from visitLocation in mEFERTDb.VisitingLocations
                                      where visitLocation != null
                                      select visitLocation).ToList();
            }
            catch (Exception e)
            {
                
            }

            //try
            //{
            //    Cardholder cardHolderByNic = (from c in mCCFTCentral.Cardholders
            //                                  where c != null && c.LastName == "123456789"
            //                                  select c).FirstOrDefault();                
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    CadreInfo cadreInfo = (from c in mEFERTDb.Cadres
            //                           where c != null && c.CadreName == "mpt"
            //                           select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    CrewInfo crewInfo = (from c in mEFERTDb.Crews
            //                         where c != null && c.CrewName == "C"
            //                         select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    DepartmentInfo departmentInfo = (from c in mEFERTDb.Departments
            //                                     where c != null && c.DepartmentName == "Admin"
            //                                     select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    DesignationInfo designationInfo = (from c in mEFERTDb.Designations
            //                                       where c != null && c.Designation == "ABC"
            //                                       select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    SectionInfo sectionInfo = (from c in mEFERTDb.Sections
            //                               where c != null && c.SectionName == "XYZ"
            //                               select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    CompanyInfo companyInfo = (from c in mEFERTDb.Companies
            //                               where c != null && c.CompanyName == "ABC"
            //                               select c).FirstOrDefault();
            //}
            //catch (Exception)
            //{

            //}
            
        }

        public static void RollBack()
        {
            var context = mEFERTDb;
            var changedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public static string GetInnerExceptionMessage(Exception ex)
        {
            string message = ex.Message;
            Exception innerException = ex.InnerException;

            while (innerException != null)
            {
                message += "\n" + innerException.Message;
                innerException = innerException.InnerException;
            }

            return message;
        }

        public static LimitStatus CheckIfUserCheckedInLimitReached(List<CheckInAndOutInfo> checkIns)
        {
            LimitStatus limitStatus = LimitStatus.Allowed;

            if (checkIns.Count > 0)
            {
                CheckInAndOutInfo last = checkIns.Last();

                if (last.CheckedIn)
                {
                    limitStatus = LimitStatus.CurrentlyCheckIn;
                }
                else
                {
                    CheckInAndOutInfo previousCheckIn = checkIns[0];

                    if (previousCheckIn != null)
                    {
                        int count = 0;
                        DateTime previousDateTimeIn = previousCheckIn.DateTimeIn;

                        for (int i = 1; i < checkIns.Count; i++)
                        {
                            CheckInAndOutInfo CurrentCheckIn = checkIns[i];

                            DateTime currDateTimeIn = CurrentCheckIn.DateTimeIn;

                            TimeSpan timeDiff = currDateTimeIn - previousDateTimeIn;
                            
                            bool isContinous = timeDiff.Days == 1;

                            if (isContinous)
                            {
                                count++;
                            }
                            else
                            {
                                count = 0;
                            }

                            previousDateTimeIn = currDateTimeIn;
                        }

                        if (count == 70)
                        {
                            limitStatus = LimitStatus.LimitReached;
                        }
                        else
                        {
                            limitStatus = LimitStatus.Allowed;
                        }
                    }
                }
            }

            return limitStatus;
        }

        public static bool ValidateInputs(List<TextBox> lstTextBoxes)
        {
            bool validated = false;

            if (lstTextBoxes != null)
            {
                List<TextBox> lstInvalidTextboxes = (from text in lstTextBoxes
                 where text != null && string.IsNullOrEmpty(text.Text)
                 select text).ToList();

                validated = lstInvalidTextboxes.Count == 0;

                foreach (TextBox textbox in lstInvalidTextboxes)
                {
                    textbox.BackColor = System.Drawing.Color.Yellow;
                }
            }

            return validated;
        }
    }

    public enum LimitStatus
    {
        Allowed,
        LimitReached,
        CurrentlyCheckIn
    }
}
