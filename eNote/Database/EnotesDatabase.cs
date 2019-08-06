using System;
using System.Collections.Generic;
using PropertyChanged;
using SQLite;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class EnotesDatabase
    {
        private SQLiteConnection db;
        public EnotesDatabase()
        {
            try
            {
                db = DependencyService.Get<IDatabase>().GetConnection();
                if (db != null)
                {
                    db.CreateTable<Notes>();

                    db.CreateTable<Users>();
                    db.CreateTable<UserPins>();
                    db.CreateTable<AudioFiles>();
                    db.CreateTable<Purchase>();
                    db.CreateTable<Expenses>();
                }
            }
            catch (Exception ex)
            {

            }
           

        }
        #region _____ Add Or Update Items _______
        public bool AddOrUpdateUserDetails(Users users)
        {
            try
            {
                var userItems = db.Query<Users>("SELECT * FROM Users WHERE UserName = ?", users.UserName);
                if (userItems == null || userItems.Count == 0)
                {
                    db.Insert(users);
                    return true;
                }
                else
                {
                    // Update required Fileds
                    userItems[0].IsPinEnable = users.IsPinEnable;
                    userItems[0].Passcode = users.Passcode;
                    userItems[0].ModifiedDate = users.ModifiedDate;
                    db.Update(userItems[0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool AddOrUpdatePINDetails(UserPins users)
        {
            try
            {
                var userItems = db.Query<UserPins>("SELECT * FROM UserPins WHERE UserName = ?", users.UserName);
                if (userItems == null || userItems.Count == 0)
                {
                    db.Insert(userItems);
                    return true;
                }
                else
                {
                    // Update required Fileds
                    userItems[0].IsPinEnable = users.IsPinEnable;
                    userItems[0].Password = users.Password;
                    userItems[0].ModifiedDate = users.ModifiedDate;
                    db.Update(userItems[0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool AddOrUpdateNotesDetails(Notes notes)
        {
            try
            {
                var notesItems = db.Query<Notes>("SELECT * FROM Notes WHERE Id = ?", notes.Id);
                if (notesItems == null || notesItems.Count == 0)
                {
                    db.Insert(notes);
                    GetAllNotesList(StringValues.UserName);
                    return true;
                }
                else
                {
                    // Update required Fileds
                    notesItems[0].Image = notes.Image;
                    notesItems[0].NotesTitle = notes.NotesTitle;
                    notesItems[0].NotesDescription = notes.NotesDescription;
                    notesItems[0].PIN = notes.PIN;
                    notesItems[0].ModifiedDate = notes.ModifiedDate;
                    db.Update(notesItems[0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool AddOrUpdateAudioDetails(AudioFiles users)
        {
            try
            {


                    db.Insert(users);
                    return true;


            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool AddOrUpdatePurchaseDetails(Purchase purchase)
        {
            try
            {
                var purchaseItems = db.Query<Purchase>("SELECT * FROM Purchase WHERE Id = ?", purchase.Id);
                if (purchaseItems == null || purchaseItems.Count == 0)
                {
                    db.Insert(purchase);

                    return true;
                }
                else
                {
                    // Update required Fileds
                    purchaseItems[0].AutoMessage = purchase.AutoMessage;
                    purchaseItems[0].ExpectedAmount = purchase.ExpectedAmount;
                    purchaseItems[0].ExpectedPurchaseDate = purchase.ExpectedPurchaseDate;
                    purchaseItems[0].ItemName = purchase.ItemName;
                    purchaseItems[0].Reminder = purchase.Reminder;
                    purchaseItems[0].AutoMessage = purchase.AutoMessage;
                    purchaseItems[0].RemindMe = purchase.RemindMe;
                    purchaseItems[0].PhoneNumber = purchase.PhoneNumber;
                    purchaseItems[0].ReasonForPurchasing = purchase.ReasonForPurchasing;
                    purchaseItems[0].ModifiedDate = purchase.ModifiedDate;
                    db.Update(purchaseItems[0]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        #endregion

        #region _____ Get Items _______
        public List<Notes> GetAllNotesList(string userName)
        {
            var userItems = db.Query<Notes>("SELECT * FROM Notes WHERE UserName = ?", userName);
            return userItems;
        }
        public List<Users> GetAllUserDetails()
        {
            var userItems = db.Query<Users>("SELECT * FROM Users");
            return userItems;
        }
        public List<Users> GetAllUserNames(string userName)
        {
            var userItems = db.Query<Users>("SELECT UserName FROM Users WHERE UserName != ?",userName);
            return userItems;
        }
        public Users GetSelectedUser(string userName)
        {
            var resp = db.Query<Users>("SELECT * FROM Users WHERE UserName = ?", userName);
            if (resp != null && resp.Count == 1)
            {
                return resp[0];
            }
            else
                return null;

        }
        public List<Purchase> GetAllPurchaseList(string userName)
        {
            var purchaseItems = db.Query<Purchase>("SELECT * FROM Purchase WHERE UserName = ?", userName);
            return purchaseItems;
        }
#if DEBUG
        public List<Notes> GetAllNotesListTest()
        {
            var userItems = db.Query<Notes>("SELECT * FROM Notes");
            return userItems;
        }
#endif
#endregion

        #region _____ Check Items _______
        public bool IsValidUser(string userName, string password)
        {
            var resp = db.Query<Users>("SELECT * FROM Users WHERE UserName = ? and Password = ?", userName, password);
            if (resp != null && resp.Count > 0)
                return true;
            else
                return false;

        }
        public bool IsUserExist(string userName)
        {
            var resp = db.Query<Users>("SELECT * FROM Users WHERE UserName = ?", userName);
            if (resp != null && resp.Count > 0)
                return true;
            else
                return false;

        }
        #endregion

        #region _____ Delete Items _______
        public bool DeleteNotes(List<Notes> notes)
        {
            try
            {
                foreach (var items in notes)
                {
                    var resp = from res in db.Table<Notes>()
                               where res.UserName == items.UserName && res.Id == items.Id
                               select res;
                    if (resp != null && resp.Count() > 0)
                    {
                        db.Delete(resp.FirstOrDefault());

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteUser(string userName)
        {
            //var resp = from res in db.Table<Users>()
                       //where res.UserName == userName
                       //select res;
            var resp= db.Query<Users>("SELECT * FROM Users WHERE UserName = ?", userName);
            if (resp != null && resp.Count > 0)
            {
               // var userItems = db.Query<Notes>("DELETE FROM Notes WHERE UserName = ?", userName);
                var userItems = from res in db.Table<Notes>()
                           where res.UserName == userName
                           select res;
                if (userItems != null && userItems.Count() > 0)
                {
                    foreach (var items in userItems)
                    {
                        db.Delete(items);
                    }

                }
                db.Delete<Users>(resp[0].Id);
                return true;
            }
            else
                return false;

        }

        public bool DeletePurchase(string  userName,int Id)
        {
            try
            {

                    var resp = from res in db.Table<Purchase>()
                               where res.UserName == userName && res.Id == Id
                               select res;
                    if (resp != null && resp.Count() > 0)
                    {
                        db.Delete(resp.FirstOrDefault());
                        return true;

                     }
                    else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
