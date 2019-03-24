using System;
using FreshMvvm;
using PropertyChanged;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class AboutPageModel : FreshBasePageModel
    {
        public string NvColor1 { get; set; }
        public string BgColor1 { get; set; }
        public AboutPageModel()
        {
        }
    }
}
