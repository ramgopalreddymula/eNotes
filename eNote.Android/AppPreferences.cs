using System;
using Android.Content;
using Android.Preferences;

namespace eNote.Droid
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String PREFERENCE_ACCESS_KEY = "userName";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveAccessKey(string key)
        {
            mPrefsEditor.PutString(PREFERENCE_ACCESS_KEY, key);
            mPrefsEditor.Apply();
        }

        public string getAccessKey()
        {
            return mSharedPrefs.GetString(PREFERENCE_ACCESS_KEY, "");
        }
    }
}
