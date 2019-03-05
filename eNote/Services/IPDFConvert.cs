using System;
namespace eNote.Services
{
    public interface IPDFConvert
    {
        string SafeHTMLToPDF(string html, string filename);

    }
}
