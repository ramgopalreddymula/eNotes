using System;
using System.Collections.Generic;
//using Plugin.AudioRecorder;
using Xamarin.Forms;

namespace eNote
{
    public partial class AudioRecordPage : ContentPage
    {
        public AudioRecordPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, true);
            //if (Device.RuntimePlatform == Device.iOS)
            //    this.Padding = new Thickness(0, 28, 0, 0);
            //recorder = new AudioRecorderService
            //{
            //    StopRecordingAfterTimeout = true,
            //    TotalAudioTimeout = TimeSpan.FromSeconds(15),
            //    AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            //};

            //player = new AudioPlayer();
            //player.FinishedPlaying += Finish_Playing;
        }
        //AudioRecorderService recorder;
       // AudioPlayer player;
        bool isTimerRunning = false;
        int seconds = 0, minutes = 0;
        public void Invert_Clicked(object sender, EventArgs e)
        {

            var btnInvert = (Image)sender;
           
        }
        void Finish_Playing(object sender, EventArgs e)
        {
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";
        }

        async void Record_Clicked(object sender, EventArgs e)
        {

            //if (!recorder.IsRecording)
            //{
            //    seconds = 0;
            //    minutes = 0;
            //    isTimerRunning = true;
            //    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
            //        seconds++;

            //        if (seconds.ToString().Length == 1)
            //        {
            //            lblSeconds.Text = "0" + seconds.ToString();
            //        }
            //        else
            //        {
            //            lblSeconds.Text = seconds.ToString();
            //        }
            //        if (seconds == 60)
            //        {
            //            minutes++;
            //            seconds = 0;

            //            if (minutes.ToString().Length == 1)
            //            {
            //                lblMinutes.Text = "0" + minutes.ToString();
            //            }
            //            else
            //            {
            //                lblMinutes.Text = minutes.ToString();
            //            }

            //            lblSeconds.Text = "00";
            //        }
            //        return isTimerRunning;
            //    });

            //    //
            //    recorder.StopRecordingOnSilence = IsSilence.IsToggled;
            //    var audioRecordTask = await recorder.StartRecording();

            //    bntRecord.IsEnabled = false;
            //    bntRecord.BackgroundColor = Color.Silver;
            //    bntPlay.IsEnabled = false;
            //    bntPlay.BackgroundColor = Color.Silver;
            //    bntStop.IsEnabled = true;
            //    bntStop.BackgroundColor = Color.FromHex("#7cbb45");

            //    await audioRecordTask;
            //}
        }

        async void Stop_Clicked(object sender, EventArgs e)
        {
            StopRecording();
          //  await recorder.StopRecording();
        }

        void StopRecording()
        {
            isTimerRunning = false;
            bntRecord.IsEnabled = true;
            bntRecord.BackgroundColor = Color.FromHex("#7cbb45");
            bntPlay.IsEnabled = true;
            bntPlay.BackgroundColor = Color.FromHex("#7cbb45");
            bntStop.IsEnabled = false;
            bntStop.BackgroundColor = Color.Silver;
            lblSeconds.Text = "00";
            lblMinutes.Text = "00";
        }
        void Play_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var filePath = recorder.FilePath;

            //    if (filePath != null)
            //    {
            //        StopRecording();
            //       // player.Play(filePath);
            //        DependencyService.Get<IToast>().StartPlayer(filePath);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}
