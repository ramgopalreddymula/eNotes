using System;
using System.Collections.Generic;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class NotesDetailPageModel : FreshBasePageModel
    {
        #region Properties  
        public string NotesNavTitle { get; set; }
        public string NotesTitle { get; set; }
        public string NotesDescription { get; set; }
        public string UserName { get; set; }
        public string ErrorResponce { get; set; }
        public Notes notes;
        private bool isVisibleDelete = false;
        public bool IsVisibleDelete
        {
            get { return isVisibleDelete; }
            set { isVisibleDelete = value; RaisePropertyChanged("IsVisibleDelete"); }
        }
        #endregion

        #region Constructor
        public NotesDetailPageModel()
        {
            NotesNavTitle = "Create Notes";

        }
        #endregion

        #region Default Override functions  
        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData != null)
            {
                var item = (Notes)initData;
                notes = item;
                NotesTitle = item.NotesTitle;
                NotesDescription = item.NotesDescription;
                if(notes.Id !=0)
                {
                    IsVisibleDelete = true;
                    NotesNavTitle = "Edit Notes";
                }
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData != null)
            {
                var temp = (string)returnedData;
                CoreMethods.DisplayAlert("Response", temp, "Ok");
            }

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            SaveData(true);

        }
        #endregion

        #region Commands  
        public Command SaveCommand
        {
            get
            {
                return new Command(() =>
                {
                    SaveData();

                });
            }
        }
        private async void SaveData(bool isback=false)
        {
            if (!string.IsNullOrEmpty(NotesDescription) || !string.IsNullOrEmpty(NotesTitle))
            {
                bool isUpdate = false;
                if (notes == null)
                {
                    notes = new Notes();
                    notes.CreateDate = DateTime.Now.ToUniversalTime();

                }
                else
                {
                    isUpdate = true;
                    notes.ModifiedDate = DateTime.Now.ToUniversalTime();
                }
                notes.NotesTitle = NotesTitle;
                notes.NotesDescription = NotesDescription;
                notes.UserName = StringValues.UserName;
                var resp = SaveNotesDetails(notes);
                if (resp)
                {
                    ClearValues();
                    if(!isback)
                        CoreMethods.PopPageModel(false, true);
                    
                    string message = "Saved Note Sucessfully";
                    if (isUpdate)
                    {
                        message = "Updated Note Sucessfully";
                    }
                    DependencyService.Get<IToast>().Show(message);

                }

                else
                    DependencyService.Get<IToast>().Show(ErrorStrings.NotesSaveFail);
                //await CoreMethods.DisplayAlert("Error", ErrorStrings.NotesSaveFail, "Ok");

            }
            else
            {
              //  DependencyService.Get<IToast>().Show(ErrorStrings.NotesDescriptionMan);
                //await CoreMethods.DisplayAlert("Error", ErrorStrings.NotesDescriptionMan, "Ok");
            }
           
        }
        public Command CancelCommand
        {
            get
            {
                return new Command(() =>
                {
                    CoreMethods.PopToRoot(false);
                });
            }
        }


        #endregion

        #region Private Methods
         private bool SaveNotesDetails(Notes items)
        {
            var resp = App.database.AddOrUpdateNotesDetails(items);
            return resp;
        }
        private void ClearValues()
        {
            NotesTitle = String.Empty;
            NotesDescription = String.Empty;
            //PasswordText = String.Empty;
            //ConfPasswordText = String.Empty;
        }
        public Command DeleteNotesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ClearValues();
                    List<Notes> noteItem = new List<Notes>();
                    noteItem.Add(notes);
                    var resp=App.database.DeleteNotes(noteItem);
                    if(resp)
                        await CoreMethods.PopPageModel(false, true);
                });
            }
        }

        #endregion
    }
}
