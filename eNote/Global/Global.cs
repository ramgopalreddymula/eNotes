using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace eNote
{

    public static class Global
    {
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
       
        private static string ENotesNavBarColor= "NavBarColor,#117864";
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
        private static string ENotesBackgroundColor = "BgColor,#FFFFFF";
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
}
