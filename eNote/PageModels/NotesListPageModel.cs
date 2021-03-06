﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class NotesListPageModel : FreshBasePageModel
    {
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        private bool isLoginVisible = false;
        public bool IsLoginVisible
        {
            get { return isLoginVisible; }
            set { isLoginVisible = value; RaisePropertyChanged("IsLoginVisible"); }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NotesNavTitle { get; set; }
        public NotesListPageModel()
        {
            NvColor1 = Global.eNotesNavBarColor;
            BgColor1 = Global.eNotesBackgroundColor;
            NotesNavTitle = "Notes" + "\n" + "( " + StringValues.UserName + " )";
        }
       
        public ObservableCollection<Notes> NotesList { get; set; }

        public override void Init(object initData)
        {
            try
            {
                NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
            }
            catch (Exception ex)
            {
                NotesList = new ObservableCollection<Notes>();
            }

        }
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
           
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
           // NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
            try
            {
                NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
            }
            catch (Exception ex)
            {
                NotesList = new ObservableCollection<Notes>();
            }

        }
        Notes _selectedItem;

        public Notes SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (value != null)
                    ListItemSelected.Execute(value);
            }
        }
        public Command<Notes> ListItemSelected
        {
            get
            {
                return new Command<Notes>(async (users) => {
                    _selectedItem = null;
                    await CoreMethods.PushPageModel<NotesDetailPageModel>(users);
                });
            }
        }
        public Command AddNotesCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await CoreMethods.PushPageModel<NotesDetailPageModel>();
                });
            }
        }
        public Command<Notes> DeleteNotesCommand
        {
            get
            {
                return new Command<Notes>(async (users) =>
                {
                    bool action = await CoreMethods.DisplayAlert("Delete Notes Confirmation!", "Are you sure you want to delete?", "Yes", "No");
                    switch (action)
                    {
                        case true:
                            
                            List<Notes> noteItem = new List<Notes>();
                            noteItem.Add(users);
                            var resp = App.database.DeleteNotes(noteItem);
                            if (resp)
                            {
                                try
                                {

                                    NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
                                }
                                catch (Exception ex)
                                {
                                    NotesList = new ObservableCollection<Notes>();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                bool action = await CoreMethods.DisplayAlert("Confirmation!","Are you sure you want to delete A/c? a/c data will be lost", "Yes", "No");
                    switch (action)
                    {
                        case true:
                            if (string.IsNullOrEmpty(UserName))
                            {
                                try
                                {
                                    var response=App.database.DeleteUser(StringValues.UserName);
                                    if (response)
                                    {
                                        string navColor = "NavBarColor" + StringValues.UserName;
                                        string bgColor = "BgColor" + StringValues.UserName;
                                        Application.Current.Properties.Remove("userName");
                                        App.Current.Properties.Remove("userName");
                                        Application.Current.Properties.Remove(navColor);
                                        Application.Current.Properties.Remove(bgColor);
                                        await App.Current.SavePropertiesAsync();
                                        await CoreMethods.PushPageModel<LoginPageModel>();
                                        UserName = string.Empty;
                                        Password = string.Empty;
                                        IsLoginVisible = false;
                                        DependencyService.Get<IToast>().Show(ErrorStrings.AccountDeletion);
                                    }
                                    else
                                    {
                                        DependencyService.Get<IToast>().Show("Failed, Please try again");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    DependencyService.Get<IToast>().Show("Failed, Please try again");
                                }


                            }
                            else
                            {
                                var response = App.database.DeleteUser(UserName);
                                if (response)
                                {

                                    UserName = string.Empty;
                                    Password = string.Empty;
                                    IsLoginVisible = false;
                                    DependencyService.Get<IToast>().Show(ErrorStrings.AccountDeletion);
                                }
                                else
                                {
                                    DependencyService.Get<IToast>().Show("Failed, Please try again");
                                }

                            }
                            break;
                        default:
                                break;
                    }
                });
            }
        }
        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    UserName = string.Empty;
                    Password = string.Empty;
                    IsLoginVisible = false;
                });
            }
        }//CancelCommand
        public Command SwitchUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var resp = App.database.GetAllUserNames(StringValues.UserName);
                    if (resp != null && resp.Count > 0)
                    {
                        List<string> list = new List<string>();
                        foreach (var item in resp)
                        {
                            list.Add(item.UserName);
                        }
                        string[] resut = list.ToArray();
                        var action = await CoreMethods.DisplayActionSheet("Switch User!\n Please Select User?", "Cancel", "", resut);
                        switch (action)
                        {
                            case "Cancel":
                            case "":

                                break;
                            default:
                                if (!String.IsNullOrEmpty(action))
                                {
                                    IsLoginVisible = true;
                                    UserName = action;
                                }
                                break;
                        }
                    }
                    else{
                        DependencyService.Get<IToast>().Show("Sorry!,You have only one account!");
                    }
                });
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (App.database.IsValidUser(UserName.ToLower(), Password))
                    {
                        StringValues.UserName = UserName.ToLower();
                        NotesNavTitle = "Notes" + "\n" +"( "+ StringValues.UserName+" )";
                        try
                        {
                            Application.Current.Properties.Remove("userName");
                            App.Current.Properties.Remove("userName");
                            await App.Current.SavePropertiesAsync();
                            App.Current.Properties.Add("userName", StringValues.UserName);
                            await App.Current.SavePropertiesAsync();
                        }
                        catch (Exception ex)
                        {

                        }
                        NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
                        IsLoginVisible = false;
                        UserName = "";
                        Password = "";
                        DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginSwitched);
                    }
                    else
                    {
                        DependencyService.Get<IToast>().Show(ErrorStrings.UserLoginCredtionalsFail);
                        //CoreMethods.DisplayAlert("Error", ErrorStrings.UserLoginCredtionalsFail, "Ok");
                    }
                });
            }
        }
        public Command LogOutCommand
        {
            get
            {
                return new Command(async () =>
                {
                   

                    var action = await CoreMethods.DisplayAlert("Confirmation!","Are you sure you want to logout?", "Yes","No");
                    switch (action)
                    {
                        case true:
                            try
                            {
                                Application.Current.Properties.Remove("userName");
                                App.Current.Properties.Remove("userName");
                                await App.Current.SavePropertiesAsync();
                                Global.eNotesNavBarColor = "NavBarColor,#117864";
                                Global.eNotesBackgroundColor = "BgColor,#FFFFFF";
                                await CoreMethods.PushPageModel<LoginPageModel>();
                            }
                            catch (Exception ex)
                            {
                                DependencyService.Get<IToast>().Show("Failed, Please try again");
                            }

                            break;
                        case false:
                           
                             break;
                        default:
                            break;
            }
                });
            }
        }

    }
}
