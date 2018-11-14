using System;
using System.Collections.Generic;

namespace eNote
{
    public static class Global
    {
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
