using System;
using System.IO;
using System.Threading.Tasks;

namespace eNote.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
