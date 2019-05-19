using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace eNote
{
    public partial class CustomSlideView : ContentView
    {
       
        public ICommand CloseTappedCommand { get { return new Command((obj) => OnCloseMenu()); } }

        public CustomSlideView()
        {
            InitializeComponent();
            var gestureRecognizer = new TapGestureRecognizer();
            gestureRecognizer.Command = CloseTappedCommand;
            CloseStack.GestureRecognizers.Add(gestureRecognizer);
        }

        public static readonly BindableProperty IsSlideOpenProperty =
            BindableProperty.Create("IsSlideOpen",
                                    typeof(bool),
                                    typeof(CustomSlideView),
                                    false,
                                    BindingMode.TwoWay,
                                    propertyChanged: SlideOpenClose);

        public static readonly BindableProperty DefaultHeightProperty =
            BindableProperty.Create("DefaultHeight",
                                    typeof(double),
                                    typeof(CustomSlideView),
                                    0.0D,
                                    BindingMode.TwoWay, null,
                                    propertyChanged: DefaultHeightChanged, propertyChanging: null, coerceValue: null, defaultValueCreator: null);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                                    typeof(StackLayout),
                                    typeof(CustomSlideView),
                                    null,
                                    BindingMode.TwoWay, null,
                                    propertyChanged: StackLayoutAdded, propertyChanging: null, coerceValue: null, defaultValueCreator: null);

        public bool IsSlideOpen
        {
            get { return (bool)GetValue(IsSlideOpenProperty); }
            set { SetValue(IsSlideOpenProperty, value); }
        }

        public StackLayout ItemTemplate
        {
            get { return (StackLayout)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public double DefaultHeight
        {
            get { return (double)GetValue(DefaultHeightProperty); }
            set { SetValue(DefaultHeightProperty, value); }
        }


        private static async void SlideOpenClose(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                (bindable as CustomSlideView).IsVisible = true;
                await (bindable as CustomSlideView).TranslateTo(0, 0, 250, Easing.SinInOut);
                newValue = false;
            }
            else
            {
                await (bindable as CustomSlideView).TranslateTo(0, App.Current.MainPage.Height, 250, Easing.SinInOut);
                (bindable as CustomSlideView).IsVisible = false;
                newValue = true;
            }
        }

        private static void StackLayoutAdded(BindableObject bindable, object oldValue, object newValue)
        {
            if ((StackLayout)newValue != null)
            {
                (bindable as CustomSlideView).ParentStackLayout.Children.Add((StackLayout)newValue);
            }
        }

        private static void DefaultHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomSlideView).IsVisible = false;
            (bindable as CustomSlideView).TranslationY = (double)newValue;
        }

        private void OnCloseMenu()
        {
            IsSlideOpen = false;
        }
    }
}
