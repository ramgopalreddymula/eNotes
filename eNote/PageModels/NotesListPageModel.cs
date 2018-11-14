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
            NotesNavTitle = "Notes" + "\n" + "( " + StringValues.UserName + " )";
        }
       
        public ObservableCollection<Notes> NotesList { get; set; }

        public override void Init(object initData)
        {
            NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));
        }
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
           
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            NotesList = new ObservableCollection<Notes>(App.database.GetAllNotesList(StringValues.UserName));

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
        public Command DeleteNotesCommand
        {
            get
            {
                return new Command(async () =>
                {
                   
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
                   

                    var action = await CoreMethods.DisplayActionSheet("Confirmation!\n Are you sure you want to logout?", "No", "Yes");
                    switch (action)
                    {
                        case "Yes":
                            try
                            {
                                Application.Current.Properties.Remove("userName");
                                App.Current.Properties.Remove("userName");
                                await App.Current.SavePropertiesAsync();
                                await CoreMethods.PushPageModel<LoginPageModel>();
                            }
                            catch (Exception ex)
                            {
                                DependencyService.Get<IToast>().Show("Failed, Please try again");
                            }

                            break;
                        case "No":
                           
                             break;
                        default:
                            break;
            }
                });
            }
        }

    }
}
