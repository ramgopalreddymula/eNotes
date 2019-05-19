using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eNote
{
    public partial class NotesDetailPage : ContentPage
    {
        public NotesDetailPage()
        {
            InitializeComponent();
            NotesDetailPageModel.eventEnotesAction -= LoginPageModel_EventEnotesAction;
            NotesDetailPageModel.eventEnotesAction += LoginPageModel_EventEnotesAction;
            BindingContext = new NotesDetailPageModel();
            ScrollAnimation();
        }
        private async void ScrollAnimation()
        {
            do
            {
                await lblScroll.TranslateTo(300, 0, 2000);
                await Task.Delay(1000);
                await lblScroll.TranslateTo(-300, 0, 2000);
                await lblScroll.TranslateTo(0, 0, 2000);
            } while (true);
        }
        async void LoginPageModel_EventEnotesAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.DeleteNotes:

                    await imgDelete.TranslateTo(100, 0, 2000);
                    await imgDelete.TranslateTo(0, 0, 2000);

                    break;
                case ActionType.Save:
                    imgSave.Opacity = 0;
                    await imgSave.FadeTo(1, 1500);

                    break;
                default:
                    break;
            }
        }
    }
}
