using LocationManagementSystem.CCFTCentralDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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
                List<string> lstInvalidTextboxes = (from text in lstTextBoxes
                 where text != null && string.IsNullOrEmpty(text.Text)
                 select text.Name).ToList();

                validated = lstInvalidTextboxes.Count == 0;

                foreach (TextBox textbox in lstTextBoxes)
                {
                    if (textbox.ReadOnly)
                    {
                        continue;
                    }
                    if (lstInvalidTextboxes.Contains(textbox.Name))
                    {
                        textbox.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        textbox.BackColor = System.Drawing.Color.White;
                    }

                }
            }

            return validated;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            byte[] array = null;

            if (imageIn != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    array = ms.ToArray();
                }
            }

            return array;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;

            if (byteArrayIn != null)
            {
                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                {
                    returnImage = Image.FromStream(ms);
                }
            }
            
            return returnImage;
        }

        public static void AllowNumericOnly(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
    }

    public enum LimitStatus
    {
        Allowed,
        LimitReached,
        CurrentlyCheckIn
    }
}
