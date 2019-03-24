using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace eNote
{
    public class ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            Dictionary<string, string> dicColor = new Dictionary<string, string>();
            dicColor.Add("DeepSea", "#117864");
            dicColor.Add("SkyBlue", "#85C1E9");
            dicColor.Add("Maroon", "#6E2C00");
            dicColor.Add("Blue", "#1F618D");
            dicColor.Add("ShadesBlue", "#212F3C");
            dicColor.Add("Gray", "#B3B6B7");
            dicColor.Add("Peach", "#DAF7A6");
            dicColor.Add("Cyan", "#7E5109");
            dicColor.Add("aquamarine2", "#76eec6");
            dicColor.Add("eNotes", "#00C59B");
            dicColor.Add("White", "#FFFFFF");

            switch (value)
            {
                case EnotesColors.DeepSea:
                case "DeepSea":
                    return Color.FromHex(dicColor["DeepSea"]);
                case EnotesColors.SkyBlue:
                case "SkyBlue":
                    return Color.FromHex(dicColor["SkyBlue"]);
                case EnotesColors.Maroon:
                case "Maroon":
                    return Color.FromHex(dicColor["Maroon"]);
                case EnotesColors.Blue:
                case "Blue":
                    return Color.FromHex(dicColor["Blue"]);
                case EnotesColors.ShadesBlue:
                case "ShadesBlue":
                    return Color.FromHex(dicColor["ShadesBlue"]);
                case EnotesColors.Gray:
                case "Gray":
                    return Color.FromHex(dicColor["Gray"]);
                case EnotesColors.Peach:
                case "Peach":
                    return Color.FromHex(dicColor["Peach"]);
                case EnotesColors.Cyan:
                case "Cyan":
                    return Color.FromHex(dicColor["Cyan"]);
                case EnotesColors.aquamarine2:
                case "aquamarine2":
                    return Color.FromHex(dicColor["aquamarine2"]);
                case EnotesColors.eNotes:
                case "eNotes":
                    return Color.FromHex(dicColor["eNotes"]);
                case EnotesColors.white:
                case "White":
                    return Color.FromHex(dicColor["White"]);
                case "ExpectedDate":
                    return Color.Green;
                 
                case "ReminderDate":
                    return Color.YellowGreen;
                   
                case "ReminderExpDate":
                    return Color.Red;
                    

                default:
                    return Color.FromHex(dicColor["eNotes"]);

            }


        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public enum EnotesColors
    {
        DeepSea,
        SkyBlue,
        Maroon,
        Blue,
        ShadesBlue,
        Gray,
        Peach,
        Cyan,
        eNotes,
        aquamarine2,
            white



    }
    
}
