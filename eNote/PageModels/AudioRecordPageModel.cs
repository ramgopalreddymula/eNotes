using System;
using System.Collections.ObjectModel;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class AudioRecordPageModel : FreshBasePageModel
    {
        public AudioRecordPageModel()
        {
            NotesList = new ObservableCollection<AudioFiles>();
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg",AudioFileName="Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio2", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio3", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio4", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio5", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio6", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio7", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio8", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });
            NotesList.Add(new AudioFiles() { AudioPath = "gdjfhgfjdfgdjhfg", AudioFileName = "Audio1", Image = "Small_Microphone", UserName = "ram" });

            // resp = App.database.AddOrUpdateNotesDetails(items);
        }
        public ObservableCollection<AudioFiles> NotesList { get; set; }

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
        public Command<AudioFiles> ListItemSelected
        {
            get
            {
                return new Command<AudioFiles>(async (users) => {
                    _selectedItem = null;
                    await CoreMethods.PushPageModel<NotesDetailPageModel>(users);
                });
            }
        }
        #region OverrideMethods
        public override void Init(object initData)
        {
            if (initData != null)
            {

            }

        }

        #endregion

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

        }
    }
}
