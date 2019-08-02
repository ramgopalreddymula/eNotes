using System;
using System.Threading.Tasks;

namespace eNote
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
