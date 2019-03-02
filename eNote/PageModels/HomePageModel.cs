using System;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class HomePageModel : FreshBasePageModel
    {
        public string UserName { get; set; }
        public HomePageModel()
        {
            UserName = StringValues.UserName.ToUpper();
        }
        public Command NotesCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<NotesListPageModel>();
                });
            }
        }

        public Command CreateNotesCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<NotesDetailPageModel>();
                });
            }
        }
        public Command SettingsCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<SettingsPageModel>();
                });
            }
        }
        public Command LogOutCommand
        {
            get
            {
                return new Command( () => {

                });
            }
        }
    }
}
