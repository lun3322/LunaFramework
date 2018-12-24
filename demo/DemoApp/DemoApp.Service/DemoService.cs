using Luna.Application;

namespace DemoApp.Service
{
    public class DemoService : LunaService, IDemoService
    {
        public string GetMessage()
        {
            Logger.Info("GetMessage");
            return "message aaaaaa";
        }
    }
}