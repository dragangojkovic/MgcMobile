using MBoxMobile.Interfaces;
using System.Diagnostics;

namespace MBoxMobile.iOS.Helpers
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApplicationHandler()
        {
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
            //Thread.CurrentThread.Abort();
        }
    }
}
