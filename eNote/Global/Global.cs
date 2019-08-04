using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eNote
{

    public static class Global
    {
        public static Dictionary<string, string> dicColor = new Dictionary<string, string>();
        

        //public static bool isLoginThrough = false;
        public static IDictionary<string, object> Properties { get; set; }
        public static bool IsLogin
        {
            set{
                if(Properties.ContainsKey("IsLogin"))
                {
                    Properties["IsLogin"] = value;
                }
                else
                {
                    Properties.Add("IsLogin", value);
                }
            }
            get{
                if (Properties.ContainsKey("IsLogin"))
                {
                    return (bool)Properties["IsLogin"];
                }
                else
                {
                    return false;
                }
            }
        }
       
        private static string ENotesNavBarColor= "DeepSea";
        public static string eNotesNavBarColor
        {
            set
            {
                ENotesNavBarColor = value;
            }
            get
            {

                return ENotesNavBarColor; 

            }
        }
        private static string ENotesBackgroundColor = "White";
        public static string eNotesBackgroundColor
        {
            set
            {
                ENotesBackgroundColor = value;
            }
            get
            {

                return ENotesBackgroundColor;

            }
        }
        public static string UserName
        {
            set
            {
                if (Properties.ContainsKey("UserName"))
                {
                    Properties["UserName"] = value;
                }
                else
                {
                    Properties.Add("UserName", value);
                }
            }
            get
            {
                if (Properties != null)
                {
                    if (Properties.ContainsKey("UserName"))
                    {
                        return (string)Properties["UserName"];
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
    }
    public class Emoji
    {
        readonly int[] codes;

        public Emoji(int[] codes)
        {
            this.codes = codes;
        }

        public Emoji(int code)
        {
            codes = new int[] { code };
        }

        public override string ToString()
        {
            if (codes == null)
                return string.Empty;

            var sb = new StringBuilder(codes.Length);

            foreach (var code in codes)
                sb.Append(Char.ConvertFromUtf32(code));

            return sb.ToString();
        }
    }
}
