using System;
using System.Collections.ObjectModel;
using eNote.Models;
using FreshMvvm;
using PropertyChanged;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsPageModel : FreshBasePageModel
    {
        public SettingsPageModel()
        {
             NaviColorsList = new ObservableCollection<NaviColors>();
            BackgroundColorsList = new ObservableCollection<BackgroundColors>();
        }
        public ObservableCollection<NaviColors> NaviColorsList { get; set; }
        public ObservableCollection<BackgroundColors> BackgroundColorsList { get; set; }

        private NaviColors _selectedNavColor;
        public NaviColors SelectedNavColor
        {
            get => _selectedNavColor;
            set
            {
                _selectedNavColor = value;

            }
        }
    }
}
