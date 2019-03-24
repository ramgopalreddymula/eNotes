using System;
namespace eNote
{
    public interface IToast
    {
        void Show(string message);
        void StartPlayer(String filePath);
    }


}
