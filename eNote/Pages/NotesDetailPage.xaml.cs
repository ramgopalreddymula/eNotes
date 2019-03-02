using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eNote
{
    public partial class NotesDetailPage : ContentPage
    {
        public NotesDetailPage()
        {
            InitializeComponent();
            BindingContext = new NotesDetailPageModel();
        }
    }
}
