using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace eNote
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            LoginPageModel.eventEnotesAction -= LoginPageModel_EventEnotesAction;
            LoginPageModel.eventEnotesAction += LoginPageModel_EventEnotesAction;
            LineAnimation();

        }
        #region Animations
        private async void LineAnimation()
        {
            do
            {
                await lblSignUpLine.TranslateTo(200, 0, 2000);
                await Task.Delay(1000);
                await lblSignUpLine.TranslateTo(-200, 0, 2000);
                await lblSignUpLine.TranslateTo(0, 0, 2000);
            } while (true);
        }
        async void LoginPageModel_EventEnotesAction(ActionType action)
        {
            switch(action)
            {
                case ActionType.Login:

                    await btnLogin.TranslateTo(0,200, 1200,Easing.BounceIn);
                   
                    await btnLogin.ScaleTo(2, 100,Easing.CubicIn);
                    await btnLogin.ScaleTo(1, 190, Easing.CubicOut);
                    await btnLogin.TranslateTo(0, 0, 1200, Easing.BounceOut);
                    break;
                case ActionType.SingUp:

                    lblSignup.Opacity = 0;

                     await lblSignup.FadeTo(1);
                    break;
                default:
                    break;
            }
        }
        #endregion

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;

        }//end onBackPressed()
    }
}
