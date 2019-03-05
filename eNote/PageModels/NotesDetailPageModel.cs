using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eNote.Services;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Essentials;
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
            ClearValues();
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
            if(cts!=null)
                CancelSpeech();
        }
        #endregion
        #region Share 
        public Command ShareEmailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SendEmail(NotesTitle, NotesDescription, null);
                });
            }
        }
        public Command PDFCommand
        {
            get
            {
                return new Command(() =>
                {
                    /* var htmlSource = new HtmlWebViewSource();
                     htmlSource.Html = @"<html><body>
   <h1>Xamarin.Forms</h1>
   <p>NotesDescription</p>
   </body></html>";*/
                    // DependencyService.Get<IPDFConvert>().SafeHTMLToPDF(NotesDescription, "Invoice");
                    DependencyService.Get<IToast>().Show("Coming soon");
                });
            }
        }

        public Command DocumentCommand
        {
            get
            {
                return new Command(() =>
                {
                    DependencyService.Get<IToast>().Show("Coming soon");
                });
            }
        }
        public Command TextToSpeechCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SpeakNow();
                });
            }
        }
        public Command SendSMSCommand
        {
            get
            {
                return new Command(async () =>
                {
                    bool action = await CoreMethods.DisplayAlert("Please Enter Notes Title As Phone Number!", "Is it Phone Number Notes Title?", "Yes", "No");
                    if(action)
                        await SendSms(NotesDescription, NotesTitle);
                });
            }
        }

        public Command ShareTextCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ShareText("Subject:"+NotesTitle+"\n"+ NotesDescription);
                });
            }
        }
        CancellationTokenSource cts;
        public async Task SpeakNow()
        {
            cts = new CancellationTokenSource();
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            var locale = locales.FirstOrDefault();

            var settings = new SpeechOptions()
            {
                Volume = (float)0.75,
                Pitch = (float)1.0,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(NotesDescription, settings, cancelToken: cts.Token);
        }
        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? false)
                return;

            cts.Cancel();
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "eNotes Text"
            });
        }
        #endregion
        #region Commands  
        public Command SaveCommand
        {
            get
            {
                return new Command(async() =>
                {
                    SaveData();
                    //await SendEmail(NotesTitle, NotesDescription, new List<string>(){ "ramgopal9988@gmail.com" });
                });
            }
        }
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                bool action = await CoreMethods.DisplayAlert("Email support is not there!", "Please include email?", "Yes", "No");

            }
            catch (Exception ex)
            {
                // Some other exception occurred
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
                bool action = await CoreMethods.DisplayAlert("Delete Notes Confirmation!", "Are you sure you want to delete?", "Yes", "No");
                    switch (action)
                    {
                        case true:
                            ClearValues();
                            List<Notes> noteItem = new List<Notes>();
                            noteItem.Add(notes);
                            var resp = App.database.DeleteNotes(noteItem);
                            if (resp)
                                await CoreMethods.PopPageModel(false, true);
                            break;
                        default:
                            break;
                    }
                });
            }
        }

        #endregion
    }
}
